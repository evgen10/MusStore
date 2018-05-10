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
using System;

namespace Store.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService userService;
        private readonly ICityService cityService;

        public AccountController(IUserService userService, ICityService cityService)
        {
            this.userService = userService;
            this.cityService = cityService;
        }

        public ActionResult Login(string returnUrl)
        {
            try
            {
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }
            catch (Exception e)
            {
                return View("Error", new string[] { e.Message });

            }


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel login, string returnUrl)
        {
            try
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
            catch (Exception e)
            {
                return View("Error", new string[] { e.Message });

            }

        }

        public ActionResult Logout()
        {
            try
            {
                AuthenticationManager.SignOut();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                return View("Error", new string[] { e.Message });

            }
        }

        public ActionResult Register()
        {
            try
            {
                var cities = cityService.GetAllCities();

                SelectList city = new SelectList(cities, "Id", "Name");
                ViewBag.City = city;

                return View();
            }
            catch (Exception e)
            {
                return View("Error", new string[] { e.Message });

            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            try
            {
                ApplicationUser user = Mapper.Map<RegisterViewModel, ApplicationUser>(model);


             

                var phone  = user.PhoneNumber;

                int result;

                if (!int.TryParse(phone,out result))
                {
                    ModelState.AddModelError("Phone","Не телефон");
                }          

               

                


                if (ModelState.IsValid)
                {

                    OperationDetails operationDetails = await userService.Create(user, model.Password);

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
            catch (Exception e)
            {
                return View("Error", new string[] { e.Message });

            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            try
            {
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                return View("Error", new string[] { e.Message });

            }
        }


        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

    }
}