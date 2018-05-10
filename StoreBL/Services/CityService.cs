using StoreBL.Services.Interfaces;
using StoreData.Infrastructure.Interfaces;
using StoreModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBL.Services
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWork db;


        public CityService(IUnitOfWork unitOfWork)
        {
            db = unitOfWork;
        }  

      

        public IEnumerable<City> GetAllCities()
        {
            try
            {
                return db.Cities.GetAll();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }




        public void Dispose()
        {
            db.Dispose();
        }

    }
}
