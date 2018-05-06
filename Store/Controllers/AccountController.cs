using Store.Models;
using StoreBL.Services.Interfaces;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Security.Claims;
using StoreModel.Models;
using StoreBL.Utils;
using StoreBL.Services;
using AutoMapper;

namespace Store.Controllers
{
    public class AccountController : Controller
    {
        IUserService userService;
        ICityService cityService;

        public AccountController(IUserService userService, ICityService cityService)
        {
            this.userService = userService;
            this.cityService = cityService;
        }


        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel login, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ClaimsIdentity claim = await userService.Authenticate(login.Email, login.Password);

                if (claim == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);

                     return RedirectToLocal(returnUrl);
                }


            }

            return View(login);

        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            var cities = cityService.GetAllCities();
            SelectList city = new SelectList(cities, "Id", "Name");
            ViewBag.City = city;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            ApplicationUser user = Mapper.Map<RegisterViewModel, ApplicationUser>(model);
            //  await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                //ApplicationUser user = new ApplicationUser
                //{
                //    FirstName = model.FirstName,
                //    LastName = model.LastName,

                //    Email = model.Email,                    
                //    Address = model.Address,


                //};
                               

                OperationDetails operationDetails = await userService.Create(user,model.Password);

                if (operationDetails.Succedeed)
                {
                    ClaimsIdentity claim = await userService.Authenticate(model.Email, model.Password);

                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
                    
                }
            }

            var cities = cityService.GetAllCities();
            SelectList city = new SelectList(cities, "Id", "Name", user.CityId);
            ViewBag.CityId = city;



            return View(model);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        //private async Task SetInitialDataAsync()
        //{
        //    await UserService.SetInitialData(new UserDTO
        //    {
        //        Email = "somemail@mail.ru",
        //        UserName = "somemail@mail.ru",
        //        Password = "ad46D_ewr3",
        //        Name = "Семен Семенович Горбунков",
        //        Address = "ул. Спортивная, д.30, кв.75",
        //        Role = "admin",
        //    }, new List<string> { "user", "admin" });
        //}




        //private IUserService UserService
        //{
        //    get
        //    {
        //        return HttpContext.GetOwinContext().GetUserManager<IUserService>();
        //    }
        //}

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }



    }
}