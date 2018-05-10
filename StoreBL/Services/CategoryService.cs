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
    public class CategoryService: ICategoryService
    {

        private readonly IUnitOfWork db;
        // private IMapper mapper;


        public CategoryService(IUnitOfWork unitOfWork)
        {
            db = unitOfWork;

        }

        public void CreateMainCategory(MainCategory mainCategory)
        {
            try
            {
                db.MainCategories.Create(mainCategory);

                db.Save();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }

        public void CreateSubCategory(SubCategory subCategory)
        {
            try
            {

                db.SubCategories.Create(subCategory);
                db.Save();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public void DeleteMainCategory(MainCategory mainCategory)
        {
            try
            {
                db.MainCategories.Delete(mainCategory);
                db.Save();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public void DeleteSubCategory(SubCategory subCategory)
        {
            try
            {
                db.SubCategories.Delete(subCategory);
                db.Save();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool Exists(MainCategory mainCategory)
        {
            try
            {
                var mc = db.MainCategories.Get(m => m.CategoryName.Equals(mainCategory.CategoryName));

                if (mc != null)
                {
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool Exists(SubCategory subCategory)
        {
            try
            {
                var sc = db.SubCategories.Get(s => s.CategoryName.Equals(subCategory.CategoryName) && s.MainCategoryId == subCategory.MainCategoryId);

                if (sc != null)
                {
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public IEnumerable<MainCategory> GetAllMainCategories()
        {
            try
            {
                return db.MainCategories.GetAll();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public MainCategory GetMainCategoryById(int categoryId)
        {
            try
            {
                return db.MainCategories.Get(categoryId);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public IEnumerable<SubCategory> GetSubCategoriesByMain(int mainCategoryId)
        {
            try
            {
                return db.SubCategories.GetMany(s => s.MainCategoryId == mainCategoryId);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public IEnumerable<SubCategory> GetSubCategoriesByMain(MainCategory mainCategory)
        {
            try
            {
                return db.SubCategories.GetMany(s => s.MainCategory == mainCategory);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public SubCategory GetSubCategoryById(int categoryId)
        {
            try
            {

                return db.SubCategories.Get(categoryId);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public void UpdateMainCategory(MainCategory mainCategory)
        {
            try
            {
                var mainCat = db.MainCategories.Get(mainCategory.Id);

                mainCat.CategoryName = mainCategory.CategoryName;


                db.MainCategories.Update(mainCat);
                db.Save();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public void UpdateSubCategory(SubCategory subCategory)
        {
            try
            {
                var subCat = db.SubCategories.Get(subCategory.Id);

                subCat.MainCategoryId = subCategory.MainCategoryId;
                subCat.CategoryName = subCategory.CategoryName;

                db.SubCategories.Update(subCat);
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
