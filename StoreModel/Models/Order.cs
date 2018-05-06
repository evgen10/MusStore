using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreModel.Models
{
    public enum OrderStatus
    {
        IsInCart = 1,
        IsOrdered,
        IsConfirmed
    }


    public class Order
    {

        public Order()
        {
            CreateDate = DateTime.Now;

        }

        public int Id { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public int Count { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual Product Product { get; set; }

        public int ProductId { get; set; }

        public DateTime CreateDate { get; private set; }

        public DateTime? OrderDate { get; set; }

        public DateTime? ConfirmDate { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public decimal Cost { get; set; }

    }
}
