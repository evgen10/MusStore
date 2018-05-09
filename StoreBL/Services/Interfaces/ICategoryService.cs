using StoreModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBL.Services.Interfaces
{
    public interface ICategoryService : IDisposable
    {
        void CreateMainCategory(MainCategory mainCategory);
        void CreateSubCategory(SubCategory subCategory);
        void UpdateMainCategory(MainCategory mainCategory);
        void UpdateSubCategory(SubCategory subCategory);
        void DeleteMainCategory(MainCategory mainCategory);
        void DeleteSubCategory(SubCategory subCategory);


        bool Exists(MainCategory mainCategory);
        bool Exists(SubCategory subCategory);

        IEnumerable<SubCategory> GetSubCategoriesByMain(int mainCategoryId);
        IEnumerable<SubCategory> GetSubCategoriesByMain(MainCategory mainCategory);
        IEnumerable<MainCategory> GetAllMainCategories();


        MainCategory GetMainCategoryById(int categoryId);
        SubCategory GetSubCategoryById(int categoryId);

    }
}
