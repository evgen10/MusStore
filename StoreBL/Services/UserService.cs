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

        private readonly IUnitOfWork db;

        public UserService(IUnitOfWork unitOfWork)
        {
            db = unitOfWork;
        }

        public async Task<ClaimsIdentity> Authenticate(string email, string password)
        {
            try
            {
                ClaimsIdentity claim = null;
                // находим пользователя
                ApplicationUser user = await db.UserManager.FindAsync(email, password);
                // авторизуем его и возвращаем объект ClaimsIdentity
                if (user != null)
                {
                    claim = await db.UserManager.CreateIdentityAsync(user,
                                                DefaultAuthenticationTypes.ApplicationCookie);
                }

                return claim;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<OperationDetails> Create(ApplicationUser usr, string password)
        {
            try
            {
                ApplicationUser user = await db.UserManager.FindByEmailAsync(usr.Email);

                if (user == null)
                {
                    user = new ApplicationUser
                    {
                        FirstName = usr.FirstName,
                        LastName = usr.LastName,
                        Email = usr.Email,
                        Address = usr.Address,
                        CityId = usr.CityId,
                        UserName = usr.Email,
                        PhoneNumber = usr.PhoneNumber
                    };

                    var result = await db.UserManager.CreateAsync(user, password);

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
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }


        public void Dispose()
        {
            db.Dispose();
        }



        public IEnumerable<ApplicationUser> GetUsers()
        {
            try
            {
                return db.Users.GetAll();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public IEnumerable<ApplicationUser> GetUsers(OrderStatus status)
        {
            try
            {

                var users = from u in db.Users.GetAll()
                            from o in u.Orders
                            where o.OrderStatus == OrderStatus.IsOrdered
                            select u;

                return users.Distinct();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }


        public ApplicationUser  GetUserById(string userId)
        {
            try
            {
                return db.UserManager.FindById(userId);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }
       


    }


}
