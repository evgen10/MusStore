using StoreBL.Services.Interfaces;
using StoreModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Store.Controllers
{

    public enum OrderSorts
    {
        ByCount,
        ByUser,
        ByCost,
        ByConfirmDate,
        ByOrderDate,
        ByProduct,
    }

    [Authorize(Roles = "Administrator")]
    public class ManageController : Controller
    {
        private readonly IOrderService orderService;
        private readonly IUserService userService;

        public ManageController(IOrderService orderService, IUserService userService)
        {
            this.orderService = orderService;
            this.userService = userService;
        }

       
        public ActionResult Index()
        {            
            var users = userService.GetUsers(OrderStatus.IsOrdered);

            if (users!=null)
            {
                ViewBag.Users = users;

                return View();
            }
            else
            {
                return View("Error", new string[] { "Покупатели не найдены" });
            }          

        }

        public ActionResult GetConfirmedOrders(OrderSorts orderSort = OrderSorts.ByCount)
        {
            IEnumerable<Order> orders;

            switch (orderSort)
            {
                case OrderSorts.ByCount:
                    {
                        orders = orderService.GetConfirmedOrders().OrderByDescending(o => o.Count);
                        break;
                    }
                case OrderSorts.ByUser:
                    {
                        orders = orderService.GetConfirmedOrders().OrderByDescending(o => o.ApplicationUser.FirstName);
                        break;
                    }
                case OrderSorts.ByCost:
                    {
                        orders = orderService.GetConfirmedOrders().OrderByDescending(o => o.Cost);
                        break;
                    }
                case OrderSorts.ByConfirmDate:
                    {
                        orders = orderService.GetConfirmedOrders().OrderByDescending(o => o.ConfirmDate);
                        break;

                    }
                case OrderSorts.ByOrderDate:
                    {
                        orders = orderService.GetConfirmedOrders().OrderByDescending(o => o.OrderDate);
                        break;
                    }
                case OrderSorts.ByProduct:
                    {
                        orders = orderService.GetConfirmedOrders().OrderByDescending(o => o.ProductId);
                        break;
                    }
                default:
                    {
                        orders = orderService.GetConfirmedOrders().OrderByDescending(o => o.Count);
                        break;
                    }
            }

            return View(orders);

        }

        //выводит заказы по определенному покупателю
        public ActionResult Orders(string userId)
        {
            var orders = orderService.GetOrdersByUserId(userId);

            ViewBag.TotalCost = orders.Sum(o => o.Cost);

            return PartialView(orders);
        }

        //Подтверждает заказы покупателя
        public ActionResult Confirm(string userId)
        {
            var orders = orderService.GetOrdersByUserId(userId);

            if (orders != null)
            {
                orderService.Confirm(orders);
                return RedirectToAction("Index");
            }
            else
            {
                return View("Error", new string[] { "Покупатель не найден" });
            }
            

        }

        public ActionResult Delete(int id)
        {
            var order = orderService.GetOrderById(id);

            if (order != null)
            {
                orderService.RemoveOrder(order);
            }
            else
            {
                return View("Error", new string[] { "Заказ не найден" });
            }

            return RedirectToAction("GetConfirmedOrders");

        }

        public ActionResult UserInfo(string id)
        {
            var user = userService.GetUserById(id);

            if (user != null)
            {
                return View(user);
            }
            else
            {
                return View("Error", new string[] { "Покупатель на найден" });
            }
            

        }


    }
}