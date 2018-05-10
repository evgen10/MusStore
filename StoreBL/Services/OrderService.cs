using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreData.Infrastructure.Interfaces;
using StoreBL.Services.Interfaces;
using StoreModel.Models;

namespace StoreBL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork db;

        public OrderService(IUnitOfWork unitOfWork)
        {
            db = unitOfWork;



        }


        public void AddToCart(Product product, string userId)
        {
            try
            {
                Order order = new Order
                {
                    ApplicationUserId = userId,
                    Product = product,
                    OrderStatus = OrderStatus.IsInCart,
                    Count = 1,
                    Cost = product.Price


                };

                var ord = db.Orders.Get(o => o.ApplicationUserId == order.ApplicationUserId && o.Product.Id == order.Product.Id && o.OrderStatus == OrderStatus.IsInCart);

                if (ord == null)
                {
                    db.Orders.Create(order);
                }
                else
                {
                    ord.Count++;
                    ord.Cost = ord.Count * product.Price;
                    db.Orders.Update(ord);

                }

                db.Save();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public void RemoveOrder(Order order)
        {
            try
            {
                db.Orders.Delete(order);
                db.Save();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public IEnumerable<Order> GetProductsInCartByUserId(string userId)
        {
            try
            {
                return db.Orders.GetMany(o => o.ApplicationUserId == userId && o.OrderStatus == OrderStatus.IsInCart);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public Order GetOrderById(int id)
        {
            try
            {
                return db.Orders.Get(id);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }

        public void Checkout(IEnumerable<Order> orders)
        {

            try
            {
                foreach (var item in orders)
                {
                    var order = db.Orders.Get(item.Id);

                    order.OrderStatus = OrderStatus.IsOrdered;
                    order.OrderDate = DateTime.Now;
                    order.Product.OrderCount += order.Count;

                    db.Orders.Update(order);
                }

                db.Save();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public IEnumerable<Order> GetOrdersByUserId(string userId)
        {
            try
            {
                return db.Orders.GetMany(o => o.OrderStatus == OrderStatus.IsOrdered && o.ApplicationUserId == userId);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }

        public IEnumerable<Order> GetConfirmedOrders()
        {
            try
            {
                return db.Orders.GetMany(o => o.OrderStatus == OrderStatus.IsConfirmed);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }

        public void Confirm(IEnumerable<Order> orders)
        {
            try
            {
                foreach (var item in orders)
                {
                    var order = db.Orders.Get(item.Id);

                    order.OrderStatus = OrderStatus.IsConfirmed;
                    order.ConfirmDate = DateTime.Now;


                    db.Orders.Update(order);
                }

                db.Save();
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

    }
}
