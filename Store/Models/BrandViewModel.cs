using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Store.Models
{
    public class BrandViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name="Название")]
        public string Name { get; set; }
    }
}