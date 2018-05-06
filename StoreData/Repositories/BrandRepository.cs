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
    public class BrandRepository: Repository<Brand>, IBrandRepository
    {
        public BrandRepository(StoreContext context):base(context)
        {

        }


    }
}
