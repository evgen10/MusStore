using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreModel.Models;
using StoreData.Infrastructure.Interfaces;

namespace StoreData.Repositories.Interfaces
{
    public interface IProductRepository: IRepository<Product>
    {
    }
}
