using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using ProyectoFinalL2.Models;

[assembly: OwinStartupAttribute(typeof(ProyectoFinalL2.Startup))]
namespace ProyectoFinalL2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CrearRolesUsuarios();
        }

        private void CrearRolesUsuarios()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "admin@udla.com";
                user.Email = "admin@udla.com";
                string userPWD = "Cesar2020*";
                var chkUser = userManager.Create(user, userPWD);

                userManager.AddToRole(user.Id, "Admin");
            }
        }
    }
}
