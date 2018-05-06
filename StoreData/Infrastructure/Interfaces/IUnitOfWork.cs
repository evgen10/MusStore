using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreData.Repositories;
using StoreData.Repositories.Interfaces;
using StoreData.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace StoreData.Infrastructure.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationUserManager UserManager { get; }
        ApplicationRoleManager RoleManager { get; }
        IApplicationRoleRepository Roles { get; }
        IApplicationUserRepository Users { get; }
        IBrandRepository Brands { get; }
        ICityRepository Cities { get; }
        IMainCategoryRepository MainCategories { get; }
        IOrderRepository Orders { get; }
        IProductRepository Products { get; }        
        ISubCategoryRepository SubCategories { get; }

        void Save();

    }
}
