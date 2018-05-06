using StoreModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubCategoryName{ get; set; }
        public decimal Price { get; set; }
        public string BrandName { get; set; }
        public byte[] Image { get; set; }
        public string Description { get; set; }

       

    }
}