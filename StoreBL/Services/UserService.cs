using StoreBL.Services.Interfaces;
using StoreData.Infrastructure.Interfaces;
using StoreModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using StoreBL.Utils;
using System.Security.Claims;

namespace StoreBL.Services
{
    public class UserService: IUserService
    {

        private IUnitOfWork db;

        public UserService(IUnitOfWork unitOfWork)
        {
            db = unitOfWork;
        }






        public async Task<ClaimsIdentity> Authenticate(string email, string password)
        {
            ClaimsIdentity claim = null;
            // находим пользователя
            ApplicationUser user = await db.UserManager.FindAsync(email, password);
            // авторизуем его и возвращаем объект ClaimsIdentity
            if (user != null)
                claim = await db.UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        public async Task<OperationDetails> Create(ApplicationUser usr, string password)
        {
            ApplicationUser user = await db.UserManager.FindByEmailAsync(usr.Email);
           // var role = db.Roles.Get(r => r.Name == "User");

            if (user==null)
            {
                user = new ApplicationUser
                {
                    FirstName = usr.FirstName,
                    LastName = usr.LastName,
                    Email = usr.Email,
                    Address = usr.Address,
                    CityId = usr.CityId,                   
                    UserName = usr.Email      
                    



                };

                var  result = await db.UserManager.CreateAsync(user, password);

                if (!result.Succeeded)
                {
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                }

                db.UserManager.AddToRole(user.Id, "User");


                db.Save();

             

                return new OperationDetails(true, "Регистрация успешно пройдена", "");

            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
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
        // ~UserService() {
        //   // Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
        //   Dispose(false);
        // }

        // Этот код добавлен для правильной реализации шаблона высвобождаемого класса.
        public void Dispose()
        {
            // Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
            Dispose(true);
            // TODO: раскомментировать следующую строку, если метод завершения переопределен выше.
            // GC.SuppressFinalize(this);
        }
        #endregion



        public IEnumerable<ApplicationUser> GetUsers()
        {

            return db.Users.GetAll();

        }

        public IEnumerable<ApplicationUser> GetUsers(OrderStatus status)
        {

            var users = from u in db.Users.GetAll()
                        from o in u.Orders
                        where o.OrderStatus == OrderStatus.IsOrdered
                        select u;

            return users.Distinct();

        }


        public ApplicationUser  GetUserById(string userId)
        {
            return db.UserManager.FindById(userId);

        }
       

    }


}
