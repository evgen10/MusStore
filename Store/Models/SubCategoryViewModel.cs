using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Store.Models
{
    public class SubCategoryViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int MainCategoryId { get; set; }

        [Required]
        [Display(Name = "Название категории")]
        public string CategoryName { get; set; }
    }
}