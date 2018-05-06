using StoreData.Infrastructure.Interfaces;
using StoreData.Repositories;
using StoreData.Repositories.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreData.Identity;
using StoreModel.Models;

namespace StoreData.Infrastructure
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly StoreContext context;

        public UnitOfWork(StoreContext context)
        {
            this.context = context;

            Roles = new ApplicationRoleRepository(this.context);
            Users = new ApplicationUserRepository(this.context);
            Brands = new BrandRepository(this.context);
            Cities = new CityRepository(this.context);
            MainCategories = new MainCategoryRepository(this.context);
            SubCategories = new SubCategoryRepository(this.context);
            Orders = new OrderRepository(this.context);
            Products = new ProductRepository(this.context);            
            UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(this.context));
            RoleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(this.context));

        }        

        public IApplicationRoleRepository Roles { get; private set; }

        public IApplicationUserRepository Users { get; private set; }

        public IBrandRepository Brands { get; private set; }

        public ICityRepository Cities { get; private set; }

        public IMainCategoryRepository MainCategories { get; private set; }

        public IOrderRepository Orders { get; private set; }

        public IProductRepository Products { get; private set; }        

        public ISubCategoryRepository SubCategories { get; private set; }

        public  ApplicationUserManager UserManager { get; private set; }

        public ApplicationRoleManager RoleManager { get; private set; }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {

        }


    }
}
