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

        #region IDisposable Support
        private bool disposedValue = false; // Для определения избыточных вызовов

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: освободить управляемое состояние (управляемые объекты).
                }

                // TODO: освободить неуправляемые ресурсы (неуправляемые объекты) и переопределить ниже метод завершения.
                // TODO: задать большим полям значение NULL.

                disposedValue = true;
            }
        }

        // TODO: переопределить метод завершения, только если Dispose(bool disposing) выше включает код для освобождения неуправляемых ресурсов.
        // ~UnitOfWork() {
        //   // Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
        //   Dispose(false);
        // }

        // Этот код добавлен для правильной реализации шаблона высвобождаемого класса.
        public void Dispose()
        {
            // Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
            Dispose(true);
            GC.SuppressFinalize(this);
            // TODO: раскомментировать следующую строку, если метод завершения переопределен выше.
            // GC.SuppressFinalize(this);
        }
        #endregion




    }
}
