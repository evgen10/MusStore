using StoreModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBL.Services.Interfaces
{
    public interface IProductService
    {
        void CreateProduct(Product product);
        void DeleteProduct(Product product);
        void UpdateProduct(Product product);





        IEnumerable<Product> GetTopProducts(int count);
        IEnumerable<Product> GetProductsByCategory(int categoryId);        

        Product GetProductById(int productId);

   






































    }
}
