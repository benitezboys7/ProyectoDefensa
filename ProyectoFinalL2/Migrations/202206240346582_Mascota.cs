namespace ProyectoFinalL2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mascota : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mascotas", "Tamano", c => c.String(nullable: false));
            AddColumn("dbo.Mascotas", "Area", c => c.String());
            AddColumn("dbo.Clientes", "EdadMascota", c => c.Int(nullable: false));
            AddColumn("dbo.Clientes", "Tamano", c => c.String());
            AddColumn("dbo.Clientes", "Area", c => c.String());
            DropColumn("dbo.Mascotas", "Vacunas");
            DropColumn("dbo.Mascotas", "Pedigri");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Mascotas", "Pedigri", c => c.String());
            AddColumn("dbo.Mascotas", "Vacunas", c => c.String(nullable: false));
            DropColumn("dbo.Clientes", "Area");
            DropColumn("dbo.Clientes", "Tamano");
            DropColumn("dbo.Clientes", "EdadMascota");
            DropColumn("dbo.Mascotas", "Area");
            DropColumn("dbo.Mascotas", "Tamano");
        }
    }
}
