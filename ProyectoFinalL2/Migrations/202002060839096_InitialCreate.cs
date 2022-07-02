namespace ProyectoFinalL2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CitaMedicas",
                c => new
                    {
                        CitaMedicaID = c.Int(nullable: false, identity: true),
                        VeterinarioID = c.Int(nullable: false),
                        MascotasID = c.Int(nullable: false),
                        FechaReserva = c.DateTime(nullable: false),
                        Hora = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CitaMedicaID)
                .ForeignKey("dbo.Mascotas", t => t.MascotasID, cascadeDelete: true)
                .ForeignKey("dbo.Veterinarios", t => t.VeterinarioID, cascadeDelete: true)
                .Index(t => t.VeterinarioID)
                .Index(t => t.MascotasID);
            
            CreateTable(
                "dbo.Mascotas",
                c => new
                    {
                        MascotasID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Edad = c.Int(nullable: false),
                        Sexo = c.String(nullable: false),
                        Vacunas = c.String(nullable: false),
                        Pedigri = c.String(),
                    })
                .PrimaryKey(t => t.MascotasID);
            
            CreateTable(
                "dbo.Citas",
                c => new
                    {
                        CitasID = c.Int(nullable: false, identity: true),
                        ClienteID = c.Int(nullable: false),
                        MascotasID = c.Int(nullable: false),
                        FechaReserva = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CitasID)
                .ForeignKey("dbo.Clientes", t => t.ClienteID, cascadeDelete: true)
                .ForeignKey("dbo.Mascotas", t => t.MascotasID, cascadeDelete: true)
                .Index(t => t.ClienteID)
                .Index(t => t.MascotasID);
            
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        ClienteID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Edad = c.Int(nullable: false),
                        Cedula = c.Int(nullable: false),
                        Telefono = c.Int(nullable: false),
                        Direccion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ClienteID);
            
            CreateTable(
                "dbo.Veterinarios",
                c => new
                    {
                        VeterinarioID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Edad = c.Int(nullable: false),
                        Cedula = c.Int(nullable: false),
                        Telefono = c.Int(nullable: false),
                        Veterinario_VeterinarioID = c.Int(),
                    })
                .PrimaryKey(t => t.VeterinarioID)
                .ForeignKey("dbo.Veterinarios", t => t.Veterinario_VeterinarioID)
                .Index(t => t.Veterinario_VeterinarioID);
            
            CreateTable(
                "dbo.Voluntarios",
                c => new
                    {
                        VoluntarioID = c.Int(nullable: false, identity: true),
                        NombreV = c.String(nullable: false),
                        CedulaV = c.Int(nullable: false),
                        Correo = c.String(nullable: false),
                        MascotasID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VoluntarioID)
                .ForeignKey("dbo.Mascotas", t => t.MascotasID, cascadeDelete: true)
                .Index(t => t.MascotasID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.VeterinarioMascotas",
                c => new
                    {
                        Veterinario_VeterinarioID = c.Int(nullable: false),
                        Mascotas_MascotasID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Veterinario_VeterinarioID, t.Mascotas_MascotasID })
                .ForeignKey("dbo.Veterinarios", t => t.Veterinario_VeterinarioID, cascadeDelete: true)
                .ForeignKey("dbo.Mascotas", t => t.Mascotas_MascotasID, cascadeDelete: true)
                .Index(t => t.Veterinario_VeterinarioID)
                .Index(t => t.Mascotas_MascotasID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.CitaMedicas", "VeterinarioID", "dbo.Veterinarios");
            DropForeignKey("dbo.CitaMedicas", "MascotasID", "dbo.Mascotas");
            DropForeignKey("dbo.Voluntarios", "MascotasID", "dbo.Mascotas");
            DropForeignKey("dbo.Veterinarios", "Veterinario_VeterinarioID", "dbo.Veterinarios");
            DropForeignKey("dbo.VeterinarioMascotas", "Mascotas_MascotasID", "dbo.Mascotas");
            DropForeignKey("dbo.VeterinarioMascotas", "Veterinario_VeterinarioID", "dbo.Veterinarios");
            DropForeignKey("dbo.Citas", "MascotasID", "dbo.Mascotas");
            DropForeignKey("dbo.Citas", "ClienteID", "dbo.Clientes");
            DropIndex("dbo.VeterinarioMascotas", new[] { "Mascotas_MascotasID" });
            DropIndex("dbo.VeterinarioMascotas", new[] { "Veterinario_VeterinarioID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Voluntarios", new[] { "MascotasID" });
            DropIndex("dbo.Veterinarios", new[] { "Veterinario_VeterinarioID" });
            DropIndex("dbo.Citas", new[] { "MascotasID" });
            DropIndex("dbo.Citas", new[] { "ClienteID" });
            DropIndex("dbo.CitaMedicas", new[] { "MascotasID" });
            DropIndex("dbo.CitaMedicas", new[] { "VeterinarioID" });
            DropTable("dbo.VeterinarioMascotas");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Voluntarios");
            DropTable("dbo.Veterinarios");
            DropTable("dbo.Clientes");
            DropTable("dbo.Citas");
            DropTable("dbo.Mascotas");
            DropTable("dbo.CitaMedicas");
        }
    }
}
