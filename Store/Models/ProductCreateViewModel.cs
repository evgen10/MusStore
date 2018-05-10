using StoreModel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Store.Models
{
    public class ProductCreateViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name ="Подкатегория")]
        public int SubCategoryId { get; set; }

        [Required]
        [Display(Name = "Категория")]
        public int MainCategoryId { get; set; }
 
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Бренд")]
        public int BrandId { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 20, ErrorMessage = "Длина строки должна быть от 20 до 500 символов")]
        [Display(Name = "Описание")]
        public string Description { get; set; }

                
        public int Count { get; set; }

        [Required]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Display(Name = "Изображение")]
        public byte[] Image { get; set; }


    }
}