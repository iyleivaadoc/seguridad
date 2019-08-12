namespace web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tablapersona : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Personas",
                c => new
                    {
                        idPersona = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 50),
                        apellido = c.String(nullable: false, maxLength: 50),
                        fechaNacimiento = c.DateTime(),
                    })
                .PrimaryKey(t => t.idPersona);
            
            AddColumn("dbo.AspNetUsers", "persona_idPersona", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "persona_idPersona");
            AddForeignKey("dbo.AspNetUsers", "persona_idPersona", "dbo.Personas", "idPersona");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "persona_idPersona", "dbo.Personas");
            DropIndex("dbo.AspNetUsers", new[] { "persona_idPersona" });
            DropColumn("dbo.AspNetUsers", "persona_idPersona");
            DropTable("dbo.Personas");
        }
    }
}
