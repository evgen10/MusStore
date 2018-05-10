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

            try
            {
                db.Products.Create(product);
                db.Save();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public void DeleteProduct(Product product)
        {
            try
            {


                db.Products.Delete(product);
                db.Save();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }



        public IEnumerable<Product> GetAll()
        {
            try
            {
                return db.Products.GetAll();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }


        }

        public Product GetProductById(int productId)
        {

            try
            {
                return db.Products.Get(productId);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public IEnumerable<Product> GetProductsByCategory(int categoryId)
        {
            try
            {
                return db.Products.GetMany(p => p.SubCategoryId == categoryId);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }



        public IEnumerable<Product> GetTopProducts(int count)
        {
            try
            {
                var popularProduts = db.Products.GetAll().OrderByDescending(p => p.OrderCount);

                return popularProduts.Take(count);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }


        }

        public void UpdateProduct(Product product)
        {
            try
            {



                Product prd = GetProductById(product.Id);

                prd.BrandId = product.BrandId;

                prd.Description = product.Description;
                prd.Price = product.Price;
                prd.SubCategoryId = product.SubCategoryId;
                prd.Title = product.Title;
                prd.Image = product.Image;


                db.Products.Update(prd);
                db.Save();
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
