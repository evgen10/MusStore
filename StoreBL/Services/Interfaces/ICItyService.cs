using StoreModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBL.Services.Interfaces
{
    public interface ICityService : IDisposable
    {
        IEnumerable<City> GetAllCities();


    }
}
