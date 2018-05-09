using StoreModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBL.Services.Interfaces
{
    public interface IOrderService : IDisposable
    {
        void AddToCart(Product product, string userId);
        void RemoveOrder(Order order);
        void Checkout(IEnumerable<Order> orders);
        void Confirm(IEnumerable<Order> orders);

        IEnumerable<Order> GetProductsInCartByUserId(string userId);
        IEnumerable<Order> GetOrdersByUserId(string userId);
        IEnumerable<Order> GetConfirmedOrders();

        Order GetOrderById(int id);

    }
}
