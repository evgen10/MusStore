using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreModel.Models
{
    public class Product
    {

        public Product()
        {
            CreationDate = DateTime.Now;
            Orders = new List<Order>();

        }

        public int Id { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }




        public DateTime CreationDate { get; private set; }

        public string Description { get; set; }           

        public int OrderCount { get; set; }

        public int SubCategoryId { get; set; }

        public int BrandId { get; set; }

        public byte[] Image { get; set; }

        public virtual SubCategory SubCategory { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        


    }
}
