using AutoMapper;
using Store.Models;
using StoreBL.Services.Interfaces;
using StoreModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Store.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        // GET: Category
        public ActionResult Index()
        {
            var category = categoryService.GetAllMainCategories();

            return View(category);
        }

        // GET: Category/Create
        public ActionResult CreateMainCategory()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        public ActionResult CreateMainCategory(CreateCategoryViewModel category)
        {
            try
            {
                List<SubCategory> subCategories = new List<SubCategory>
                {
                      new SubCategory { CategoryName = category.SubCategoryName }
                };

                MainCategory mainCategory = new MainCategory
                {
                    CategoryName = category.MainCategoryName,
                    SubCategories = subCategories
                };

                if (categoryService.Exists(mainCategory))
                {
                    ModelState.AddModelError("MainCategoryName", "Категория с таким именем уже существует");
                }

                if (ModelState.IsValid)
                {
                    categoryService.CreateMainCategory(mainCategory);
                    return RedirectToAction("Index");

                }

                return View(category);
                
            }
            catch
            {
                return View();
            }
        }

        // GET: Category/Edit/5
        public ActionResult EditMainCategory(int id)
        {
            var mainCategory = categoryService.GetMainCategoryById(id);

            if (mainCategory!=null)
            {
                var mainCat = Mapper.Map<MainCategory, EditCategoryViewModel>(mainCategory);

                return View(mainCat);

            }           

            return View();
        }

        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult EditMainCategory(EditCategoryViewModel mainCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var mainCat = Mapper.Map<EditCategoryViewModel, MainCategory>(mainCategory);

                    categoryService.UpdateMainCategory(mainCat);

                    return RedirectToAction("Index");

                }

                return View(mainCategory);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Category/Delete/5
        public ActionResult DeleteMainCategory(int id)
        {
            var mainCategory = categoryService.GetMainCategoryById(id);

            if (mainCategory!=null)
            {
               return View(mainCategory);
            }

            return RedirectToAction("Index");
        }

        // POST: Category/Delete/5
        [HttpPost]
        [ActionName("DeleteMainCategory")]
        public ActionResult DeleteMainCategoryPost(int id)
        {
            try
            {
                var mainCategory = categoryService.GetMainCategoryById(id);

                if (mainCategory != null)
                {
                    categoryService.DeleteMainCategory(mainCategory);
                }
                              

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



        // GET: Category/Create
        public ActionResult CreateSubCategory(int mainCategoryId)
        {
            ViewBag.MainCategoryId = mainCategoryId;

            return View();
        }

        // POST: Category/Create
        [HttpPost]
        public ActionResult CreateSubCategory(SubCategoryViewModel subCategory)
        {
            try
            {
                var subCat = Mapper.Map<SubCategoryViewModel, SubCategory>(subCategory);

                if (categoryService.Exists(subCat))
                {
                    ModelState.AddModelError("CategoryName", "Подкатегория с таким именем уже существует");
                }


                if (ModelState.IsValid)
                {
                    categoryService.CreateSubCategory(subCat);
                    return RedirectToAction("Index");
                }


                return View(subCategory);
            }
            catch
            {
                return View();
            }
        }

        // GET: Category/Edit/5
        public ActionResult EditSubCategory(int id)
        {
            var subCategory = categoryService.GetSubCategoryById(id);            

            if (subCategory!=null)
            {
                var subCat = Mapper.Map<SubCategory, SubCategoryViewModel>(subCategory);

                IEnumerable<MainCategory> categories = categoryService.GetAllMainCategories();
                int selectedCategory = subCat.MainCategoryId;

                SelectList category = new SelectList(categories, "Id", "CategoryName", selectedCategory);
                ViewBag.Category = category;
                
                return View(subCat);
            }            

            return RedirectToAction("Index");
        }

        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult EditSubCategory(SubCategoryViewModel subCategory)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var subCat = Mapper.Map<SubCategoryViewModel, SubCategory>(subCategory);

                    categoryService.UpdateSubCategory(subCat);

                    return RedirectToAction("Index");
                }

                IEnumerable<MainCategory> categories = categoryService.GetAllMainCategories();
                int selectedCategory = subCategory.MainCategoryId;

                SelectList category = new SelectList(categories, "Id", "CategoryName", selectedCategory);
                ViewBag.Category = category;

                return View(subCategory);


               
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Category/Delete/5
        public ActionResult DeleteSubCategory(int id)
        {
            var subCategory = categoryService.GetSubCategoryById(id);
            
            if (subCategory!=null)
            {
                return View(subCategory);
            }

            return RedirectToAction("Index");
        }

        // POST: Category/Delete/5
        [HttpPost]
        [ActionName("DeleteSubCategory")]
        public ActionResult DeleteSubCategoryPost(int id)
        {
            try
            {
                var subCategory = categoryService.GetSubCategoryById(id);

                if (subCategory != null)
                {
                    categoryService.DeleteSubCategory(subCategory);
                }

                return RedirectToAction("Index");

                
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }



    }
}
