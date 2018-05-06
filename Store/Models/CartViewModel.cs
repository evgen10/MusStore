using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Store.Models
{
    public class CartViewModel
    {
    
        public int Id { get; set; }

     
        [Display(Name ="Название")]
        public string ProductName { get; set; }        


        [Display(Name ="Бренд")]
        public string Brand { get; set; }

       
        [Display(Name ="Дата заказа")]
        public DateTime CreateDate { get; set; }

        [Display(Name ="Количество")]
        public int Count { get; set; }

        [Display(Name ="Цена")]
        public decimal Price { get; set; }

        [Display(Name ="Стоймость")]
        public decimal Cost { get; set; }


    }
}