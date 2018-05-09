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

        public void RemoveOrder(Order order)
        {
            db.Orders.Delete(order);
            db.Save();

        }

        public IEnumerable<Order> GetProductsInCartByUserId(string userId)
        {
            return db.Orders.GetMany(o => o.ApplicationUserId == userId && o.OrderStatus == OrderStatus.IsInCart);

        }

        public Order GetOrderById(int id)
        {
            return db.Orders.Get(id);
        }

        public void Checkout(IEnumerable<Order> orders)
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

        public IEnumerable<Order> GetOrdersByUserId(string userId)
        {
            return db.Orders.GetMany(o => o.OrderStatus == OrderStatus.IsOrdered && o.ApplicationUserId ==userId);

        }

        public IEnumerable<Order> GetConfirmedOrders()
        {
            return db.Orders.GetMany(o => o.OrderStatus == OrderStatus.IsConfirmed);
        }

        public void Confirm(IEnumerable<Order> orders)
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


        public void Dispose()
        {
            db.Dispose();
        }

    }
}
