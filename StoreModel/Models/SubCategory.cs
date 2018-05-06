using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreModel.Models
{
    public class SubCategory
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public int MainCategoryId { get; set; }

        public MainCategory MainCategory { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        //public byte[] Image { get; set; }      

    }
}
