using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using StoreModel;
using StoreData.Identity;
using Microsoft.AspNet.Identity;
using StoreData;
using StoreBL.Services.Interfaces;
using StoreBL.Services;
using StoreData.Infrastructure.Interfaces;
using StoreData.Infrastructure;

[assembly: OwinStartup(typeof(Store.App_Start.Startup))]

namespace Store.App_Start
{
    public class Startup
    {
        IUserService userService;        

      

        public void Configuration(IAppBuilder app)
        {
            // настраиваем контекст и менеджер
            app.CreatePerOwinContext<IUserService>(CreateUserService);
            //app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            //app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }


        private IUserService CreateUserService()
        {
            return new UserService(new UnitOfWork(StoreContext.Create()));
        }

    }
}