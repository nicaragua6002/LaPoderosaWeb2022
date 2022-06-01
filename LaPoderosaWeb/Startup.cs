using LaPoderosaWeb.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LaPoderosaWeb.Startup))]
namespace LaPoderosaWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndUsers();
        }
        private void CreateRolesAndUsers()
        {
            //accedemos al modelo de la seguridad integrada
            ApplicationDbContext context = new ApplicationDbContext();
            //definimos las variables manejadoras de roles y usuarios
            var ManejadorRol = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var ManejadorUsuario = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            //Verificamos la excistencia de los roles por defecto
            if (!ManejadorRol.RoleExists("Admin"))
            {
                //sino existe, se crea el rol y se asigna un nuevo usuario con ese rol
                var rol = new IdentityRole();
                rol.Name = "Admin";
                ManejadorRol.Create(rol);
                //creamos un primer usuario para ese rol
                var user = new ApplicationUser();
                user.UserName = "Admin@lapoderosa.com";
                user.Email = "Admin@lapoderosa.com";
                string PWD = "12345678";
                var chkUser = ManejadorUsuario.Create(user, PWD);
                //si se creo con exito
                if (chkUser.Succeeded)
                {
                    ManejadorUsuario.AddToRole(user.Id, "Admin");
                }

            }

            //Verificamos la excistencia de los roles por defecto

            //Gerencia
            if (!ManejadorRol.RoleExists("Cliente"))
            {
                //sino existe, se crea el rol y se asigna un nuevo usuario con ese rol

                var rol = new IdentityRole();
                rol.Name = "Cliente";
                ManejadorRol.Create(rol);

                //creamos un primer usuario para ese rol
                var user = new ApplicationUser();
                user.UserName = "cliente@lapoderosa.com";
                user.Email = "cliente@lapoderosa.com";
                string PWD = "12345678";

                var chkUser = ManejadorUsuario.Create(user, PWD);
                if (chkUser.Succeeded)
                {
                    ManejadorUsuario.AddToRole(user.Id, rol.Name);
                }
            }

           
        }
    }
}