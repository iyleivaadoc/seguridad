using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Models
{
    public class Accesos
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_acceso { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        [StringLength(50)]
        public String Nombre { get; set; }

        [Required]
        [Display(Name = "Control")]
        [StringLength(50)]
        public String Control { get; set; }

        [Required]
        [Display(Name = "Metodo")]
        [StringLength(50)]
        public String Metodo { get; set; }

        [Required]
        [Display(Name = "Tipo")]
        public Boolean Tipo { get; set; }

        //[Required]
        [Display(Name = "AccesoPredecesor")]
        [StringLength(50)]
        public String AccesoPredecesor { get; set; }

        //[Required]
        public virtual ICollection<Permisos> Permisos { get; set; }

        //public virtual ICollection<Accesos> AccesosList { get; set; }


    }
}