using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Store.Models;
using StoreBL.Services.Interfaces;
using StoreModel.Models;

namespace Store.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IProductService productService;
        private readonly IOrderService orderService;


        public CartController(IProductService productService, IOrderService orderService)
        {
            this.orderService = orderService;
            this.productService = productService;
        }

        // GET: Cart
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            var productsInCart = orderService.GetProductsInCartByUserId(userId);
            var productInCartModel = Mapper.Map<IEnumerable<Order>, IEnumerable<CartViewModel>>(productsInCart);

            decimal totalCost = productInCartModel.Sum(t => t.Cost);

            ViewBag.TotalCost = totalCost;
            
            return View(productInCartModel);                  
            
        }
        
        public ActionResult AddProductToCart(int productId)
        {
            var product = productService.GetProductById(productId);
            string userId = User.Identity.GetUserId();

            if (product != null)
            {
                orderService.AddToCart(product, userId);
                return RedirectToAction("Index");
                    
            }

            return HttpNotFound();
        }

        public ActionResult DeleteOrder(int id)
        {
            var order = orderService.GetOrderById(id);            

            if (order!=null)
            {
                orderService.RemoveOrder(order);

                return RedirectToAction("Index");

            }

            return RedirectToAction("Index");

        }

        public ActionResult Checkout()
        {
            string userId = User.Identity.GetUserId();
            var productsInCart = orderService.GetProductsInCartByUserId(userId);
            
            if (productsInCart!=null)
            {
                orderService.Checkout(productsInCart);
                return View("OrderSucceed");
            }

            return RedirectToAction("Index");

        }
        
    }
}