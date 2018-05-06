using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreBL.Services.Interfaces;
using StoreData.Infrastructure.Interfaces;
using StoreModel.Models;

namespace StoreBL.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork db;


        public ProductService(IUnitOfWork unitOfWork)
        {
            db = unitOfWork;


        }

        public void CreateProduct(Product product)
        {
            db.Products.Create(product);
            db.Save();
        }

        public void DeleteProduct(Product product)
        {

            db.Products.Delete(product);
            db.Save();

        }

               

        public Product GetProductById(int productId)
        {


            return db.Products.Get(productId);

        }

        public IEnumerable<Product> GetProductsByCategory(int categoryId)
        {

            return db.Products.GetMany(p => p.SubCategoryId == categoryId);

        }
        
     

        public IEnumerable<Product> GetTopProducts(int count)
        {            

            var popularProduts = db.Products.GetAll().OrderByDescending(p => p.OrderCount);

            return popularProduts.Take(count);

        }

        public void UpdateProduct(Product product)
        {

            Product prd = GetProductById(product.Id);

            prd.BrandId = product.BrandId;
           // prd.Count = product.Count;
            prd.Description = product.Description;
            prd.Price = product.Price;
            prd.SubCategoryId = product.SubCategoryId;
            prd.Title = product.Title;
            prd.Image = product.Image;


            db.Products.Update(prd);
            db.Save();
        }
            
    }
}
