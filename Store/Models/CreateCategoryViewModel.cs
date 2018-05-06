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
        public string MainCategoryName { get; set; }
        
        [Required]
        public string SubCategoryName { get; set; }

    }
}