using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Store.Models
{
    public class CreateCategoryViewModel
    {
        [Required]
        [Display(Name ="Название категории")]
        public string MainCategoryName { get; set; }
        
        [Required]
        [Display(Name = "Название подкатегории")]
        public string SubCategoryName { get; set; }

    }
}