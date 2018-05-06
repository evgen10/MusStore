using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreModel.Models;
using StoreData.Repositories.Interfaces;
using StoreData.Infrastructure;


namespace StoreData.Repositories
{
    public class SubCategoryRepository: Repository<SubCategory>, ISubCategoryRepository
    {
        public SubCategoryRepository(StoreContext context):base(context)
        {

        }

    }
}
