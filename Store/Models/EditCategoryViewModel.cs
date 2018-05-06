using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Store.Models
{
    public class EditCategoryViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage ="Название не может быть пустым")]
        [Display(Name ="Название категории")]      
        public string Name { get; set; }
    }
}