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
           
            db.MainCategories.Create(mainCategory);

            db.Save();

        }

        public void CreateSubCategory(SubCategory subCategory)
        {        
            
            db.SubCategories.Create(subCategory);
            db.Save();

        }

        public void DeleteMainCategory(MainCategory mainCategory)
        {
            db.MainCategories.Delete(mainCategory);
            db.Save();
        }

        public void DeleteSubCategory(SubCategory subCategory)
        {
            db.SubCategories.Delete(subCategory);
            db.Save();
        }

        public bool Exists(MainCategory mainCategory)
        {
            var mc = db.MainCategories.Get(m => m.CategoryName.Equals(mainCategory.CategoryName));

            if (mc != null)
            {
                return true;
            }

            return false;
        }

        public bool Exists(SubCategory subCategory)
        {
            var sc = db.SubCategories.Get(s => s.CategoryName.Equals(subCategory.CategoryName) && s.MainCategoryId == subCategory.MainCategoryId);

            if (sc != null)
            {
                return true;
            }

            return false;

        }

        public IEnumerable<MainCategory> GetAllMainCategories()
        {

            return db.MainCategories.GetAll();
        }

        public MainCategory GetMainCategoryById(int categoryId)
        {
            return db.MainCategories.Get(categoryId);
        }

        public IEnumerable<SubCategory> GetSubCategoriesByMain(int mainCategoryId)
        {
            return db.SubCategories.GetMany(s => s.MainCategoryId == mainCategoryId);
        }

        public IEnumerable<SubCategory> GetSubCategoriesByMain(MainCategory mainCategory)
        {
            return db.SubCategories.GetMany(s => s.MainCategory == mainCategory);
        }

        public SubCategory GetSubCategoryById(int categoryId)
        {
            return db.SubCategories.Get(categoryId);
        }

        public void UpdateMainCategory(MainCategory mainCategory)
        {
            var mainCat = db.MainCategories.Get(mainCategory.Id);

            mainCat.CategoryName = mainCategory.CategoryName;


            db.MainCategories.Update(mainCat);
            db.Save();
        }

        public void UpdateSubCategory(SubCategory subCategory)
        {
            var subCat = db.SubCategories.Get(subCategory.Id);

            subCat.MainCategoryId = subCategory.MainCategoryId;
            subCat.CategoryName = subCategory.CategoryName;

            db.SubCategories.Update(subCat);
            db.Save();

        }

    }
}
