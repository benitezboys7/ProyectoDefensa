using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ProyectoFinalL2.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("MascotasDB", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<ProyectoFinalL2.Models.Cliente> Clientes { get; set; }

        public System.Data.Entity.DbSet<ProyectoFinalL2.Models.Mascotas> Mascotas { get; set; }

        public System.Data.Entity.DbSet<ProyectoFinalL2.Models.Citas> Citas { get; set; }

        public System.Data.Entity.DbSet<ProyectoFinalL2.Models.Voluntario> Voluntarios { get; set; }

        public System.Data.Entity.DbSet<ProyectoFinalL2.Models.Veterinario> Veterinarios { get; set; }

        public System.Data.Entity.DbSet<ProyectoFinalL2.Models.CitaMedica> CitaMedicas { get; set; }

    }
}