using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StoreModel.Models;
using StoreData.Repositories.Interfaces;
using StoreData.Infrastructure;

using System.Threading.Tasks;

namespace StoreData.Repositories
{
    public class OrderRepository: Repository<Order>, IOrderRepository
    {
        public OrderRepository(StoreContext context):base(context)
        {

        }
    }
}
