using StoreModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Store.Models
{
    public class ProductViewModel
    {

        public int Id { get; set; }

        [Display(Name ="Название")]
        public string Title { get; set; }

        [Display(Name = "Категория")]
        public string SubCategoryName{ get; set; }

        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Display(Name = "Бренд")]
        public string BrandName { get; set; }

        [Display(Name = "Изображение")]
        public byte[] Image { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

       

    }
}