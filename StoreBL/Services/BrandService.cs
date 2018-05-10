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
    public class BrandService: IBrandService
    {

        private IUnitOfWork db;

        public BrandService(IUnitOfWork unitOfWork)
        {
            db = unitOfWork;
        }

     
        public void CreateBrand(Brand brand)
        {
            db.Brands.Create(brand);
            db.Save();
        }

        public void DeleteBrand(Brand brand)
        {
           
            db.Brands.Delete(brand);
            db.Save();

        }

        
        public bool Exists(Brand brand)
        {
            var br = db.Brands.Get(b => b.Name.Equals(brand.Name));

            if (br!=null)
            {
                return true;
            }

            return false;

        }

        public IEnumerable<Brand> GetAllBrands()
        {
            try
            {

                return db.Brands.GetAll();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public Brand GetBrandById(int brandId)
        {
            try
            {
                return db.Brands.Get(brandId);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }


        }

        public void UpdateBrand(Brand brand)
        {
            try
            {

                var brnd = db.Brands.Get(brand.Id);

                brnd.Name = brand.Name;
                db.Brands.Update(brnd);
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
