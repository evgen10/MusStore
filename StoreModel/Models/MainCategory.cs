using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreModel.Models
{
    public class MainCategory
    {

        public MainCategory()
        {
            SubCategories = new List<SubCategory>();
        }
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public virtual ICollection<SubCategory> SubCategories { get; set; }      

        //public byte[] Image { get; set; }




    }
}
