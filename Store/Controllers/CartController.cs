﻿using System;
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
            try
            {
                //получаем  id пользователя использующего приложение 
                string userId = User.Identity.GetUserId();

                var productsInCart = orderService.GetProductsInCartByUserId(userId);
                var productInCartModel = Mapper.Map<IEnumerable<Order>, IEnumerable<CartViewModel>>(productsInCart);

                ///общая сумма в корзине
                decimal totalCost = productInCartModel.Sum(t => t.Cost);

                ViewBag.TotalCost = totalCost;

                return View(productInCartModel);
            }
            catch (Exception e)
            {
                return View("Error", new string[] { e.Message });

            }

        }

        public ActionResult AddProductToCart(int productId)
        {
            try
            {
                string userId = User.Identity.GetUserId();

                var product = productService.GetProductById(productId);

                if (product != null)
                {
                    orderService.AddToCart(product, userId);
                    return RedirectToAction("Index");

                }
                else
                {
                    return View("Error", new string[] { "Товар не найден" });
                }
            }
            catch (Exception e)
            {
                return View("Error", new string[] { e.Message });

            }

        }

        public ActionResult DeleteOrder(int id)
        {
            try
            {
                var order = orderService.GetOrderById(id);

                if (order != null)
                {
                    orderService.RemoveOrder(order);

                    return RedirectToAction("Index");

                }
                else
                {
                    return View("Error", new string[] { "Товар не найден" });

                }
            }
            catch (Exception e)
            {
                return View("Error", new string[] { e.Message });

            }
        }

        public ActionResult Checkout()
        {
            try
            {
                string userId = User.Identity.GetUserId();

                var productsInCart = orderService.GetProductsInCartByUserId(userId);

                if (productsInCart != null)
                {
                    orderService.Checkout(productsInCart);
                    return View("OrderSucceed");
                }
                else
                {
                    return View("Error", new string[] { "Товар не найден" });

                }
            }
            catch (Exception e)
            {
                return View("Error", new string[] { e.Message });

            }


        }

    }
}