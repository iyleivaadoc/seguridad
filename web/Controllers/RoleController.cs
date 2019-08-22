using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using web.Models;

namespace web.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private ApplicationRoleManager _roleManager;

        public RoleController()
        {
        }

        public RoleController(ApplicationRoleManager roleManager)
        {
            RoleManager = roleManager;
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        // GET: Role
        public ActionResult Index()
        {
            List<RoleViewModel> list = new List<RoleViewModel>();
            foreach (var role in RoleManager.Roles.Where(x => x.Eliminado == false))
                list.Add(new RoleViewModel(role));
            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(RoleViewModel model)
        {
            var role = new ApplicationRole() { Name = model.Name };
            role.Descripcion = model.Descripcion;
            role.UsuarioCrea = this.GetUserId(User);
            role.UsuarioModifica = null;
            role.FechaCrea = DateTime.Now;
            role.FechaModifica = role.FechaCrea;
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var x = await RoleManager.CreateAsync(role);
            if (!x.Succeeded)
            {
                foreach (var error in x.Errors)
                {
                    if (error.EndsWith("is already taken."))
                        ModelState.AddModelError("", "El Rol ya existe.");
                    else ModelState.AddModelError("", error);
                }
                return View();
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            return View(new RoleViewModel(role));
        }

        [HttpPost]
        public async Task<ActionResult> Edit(RoleViewModel model)
        {
            var role = await RoleManager.FindByIdAsync(model.Id);
            if (role.Name == "Administrador")
            {
                ModelState.AddModelError("", "El Rol Administrador no puede ser editado.");
                return View();
            }
            else
            {
                role.Name = model.Name;
                role.Descripcion = model.Descripcion;
                role.FechaModifica = DateTime.Now;
                role.UsuarioModifica = this.GetUserId(User);
                var x = await RoleManager.UpdateAsync(role);
                if (!x.Succeeded)
                {
                    foreach (var error in x.Errors)
                    {
                        if (error.EndsWith("is already taken."))
                            ModelState.AddModelError("", "El Rol ingresado ya existe, por lo que no se puede actualizar a este.");
                        else ModelState.AddModelError("", error);
                    }
                    return View();
                }
                return RedirectToAction("Index");
            }
        }

        public async Task<ActionResult> Details(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            return View(new RoleViewModel(role));
        }
        public async Task<ActionResult> Delete(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            return View(new RoleViewModel(role));
        }

        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            if (role.Name=="Administrador")
            {
                ModelState.AddModelError("", "El Rol Administrador no puede ser eliminado.");
                return View("Delete", new RoleViewModel(role));
            }
            else
            {
                role.Eliminado = true;
                role.Name = role.Name + "_deleted_" + DateTime.Now;
                role.FechaModifica = DateTime.Now;
                role.UsuarioModifica = this.GetUserId(User);
                await RoleManager.UpdateAsync(role);
                return RedirectToAction("Index");
            }
        }

        public string GetUserId(IPrincipal principal)
        {
            var claimsIdentity = (ClaimsIdentity)principal.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            return claim.Value;
        }

    }
}