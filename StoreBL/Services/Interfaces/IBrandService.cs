using StoreModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBL.Services.Interfaces
{
    public interface IBrandService : IDisposable
    {
        void CreateBrand(Brand brand);
        void UpdateBrand(Brand brand);
        void DeleteBrand(Brand brand);


        bool Exists(Brand brand);

        IEnumerable<Brand> GetAllBrands();

        Brand GetBrandById(int brandId);




    }
}
