using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using StoreBL.Services;
using StoreBL.Services.Interfaces;

namespace Store.App_Start
{
    public class Dependency : NinjectModule
    {
        public override void Load()
        {
            Bind<ICategoryService>().To<CategoryService>();
            Bind<IBrandService>().To<BrandService>();
            Bind<ICityService>().To<CityService>();
            Bind<IOrderService>().To<OrderService>();
            Bind<IProductService>().To<ProductService>();
            Bind<IUserService>().To<UserService>();
        }
    }
}