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
        public int SubCategoryId { get; set; }

        public int MainCategoryId { get; set; }
 
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        public string Title { get; set; }

        [Required]
        public int BrandId { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 20, ErrorMessage = "Длина строки должна быть от 20 до 500 символов")]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
        

        public byte[] Image { get; set; }


    }
}