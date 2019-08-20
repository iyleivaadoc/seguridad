namespace web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class roless : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetRoles", "Descripcion", c => c.String());
            AddColumn("dbo.AspNetRoles", "Eliminado", c => c.Boolean(defaultValue:false));
            AddColumn("dbo.AspNetRoles", "UsuarioCrea", c => c.String());
            AddColumn("dbo.AspNetRoles", "UsuarioModifica", c => c.String());
            AddColumn("dbo.AspNetRoles", "FechaCrea", c => c.DateTime());
            AddColumn("dbo.AspNetRoles", "FechaModifica", c => c.DateTime(nullable:true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetRoles", "FechaModifica");
            DropColumn("dbo.AspNetRoles", "FechaCrea");
            DropColumn("dbo.AspNetRoles", "UsuarioModifica");
            DropColumn("dbo.AspNetRoles", "UsuarioCrea");
            DropColumn("dbo.AspNetRoles", "Eliminado");
            DropColumn("dbo.AspNetRoles", "Descripcion");
        }
    }
}
