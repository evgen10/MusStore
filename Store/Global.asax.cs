using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using Store.App_Start;
using StoreBL;
using StoreData;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Store.Mapping;
using System.Web.Mvc;
using System.Web.Routing;


namespace Store
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            Database.SetInitializer(new StoreDbInitializer());
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AutoMapperConfig.Configure();

            NinjectModule moduleBL = new DependencyBL(new StoreContext());
            NinjectModule moduleP = new Dependency();
            var kernel = new StandardKernel(moduleBL, moduleP);
            kernel.Unbind<ModelValidatorProvider>();
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}
