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
    [Authorize(Roles = "Administrator")]
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
            //В главной категории всегда должны быть подкатегории
            //При создании главной категории добавляем подкатегорию
            List<SubCategory> subCategories = new List<SubCategory>
                {
                      new SubCategory { CategoryName = category.SubCategoryName }
                };

            MainCategory mainCategory = new MainCategory
            {
                CategoryName = category.MainCategoryName,
                SubCategories = subCategories
            };

            //проверяем существует ли категория с таким именем
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

        // GET: Category/Edit/5
        public ActionResult EditMainCategory(int id)
        {
            var mainCategory = categoryService.GetMainCategoryById(id);

            if (mainCategory != null)
            {
                var mainCat = Mapper.Map<MainCategory, EditCategoryViewModel>(mainCategory);

                return View(mainCat);

            }
            else
            {
                return View("Error", new string[] { "Категория не найдена" });
            }


        }

        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult EditMainCategory(EditCategoryViewModel mainCategory)
        {

            if (ModelState.IsValid)
            {
                var mainCat = Mapper.Map<EditCategoryViewModel, MainCategory>(mainCategory);

                categoryService.UpdateMainCategory(mainCat);

                return RedirectToAction("Index");

            }

            return View(mainCategory);

        }

        // GET: Category/Delete/5
        public ActionResult DeleteMainCategory(int id)
        {
            var mainCategory = categoryService.GetMainCategoryById(id);

            if (mainCategory != null)
            {
                return View(mainCategory);
            }
            else
            {
                return View("Error", new string[] { "Категория не найдена" });
            }

        }

        // POST: Category/Delete/5
        [HttpPost]
        [ActionName("DeleteMainCategory")]
        public ActionResult DeleteMainCategoryPost(int id)
        {

            var mainCategory = categoryService.GetMainCategoryById(id);

            if (mainCategory != null)
            {
                categoryService.DeleteMainCategory(mainCategory);
            }
            else
            {
                return View("Error", new string[] { "Категория не найдена" });
            }

            return RedirectToAction("Index");

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

        // GET: Category/Edit/5
        public ActionResult EditSubCategory(int id)
        {
            var subCategory = categoryService.GetSubCategoryById(id);

            if (subCategory != null)
            {
                var subCat = Mapper.Map<SubCategory, SubCategoryViewModel>(subCategory);


                IEnumerable<MainCategory> categories = categoryService.GetAllMainCategories();
                int selectedCategory = subCat.MainCategoryId;

                SelectList category = new SelectList(categories, "Id", "CategoryName", selectedCategory);
                ViewBag.Category = category;

                return View(subCat);
            }
            else
            {
                return View("Error", new string[] { "Категория не найдена" });
            }


        }

        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult EditSubCategory(SubCategoryViewModel subCategory)
        {

            if (ModelState.IsValid)
            {
                var subCat = Mapper.Map<SubCategoryViewModel, SubCategory>(subCategory);
                var prevSubCat = categoryService.GetSubCategoryById(subCategory.Id);

                //проверяем была ли изменена главная категория
                if (subCat.MainCategoryId != prevSubCat.MainCategoryId)
                {
                    //получаем главную категорию которая была до изменения
                    var prevMainCat = categoryService.GetMainCategoryById(prevSubCat.MainCategoryId);


                    //если данная подкатегория последняя в главной категории
                    if (prevMainCat.SubCategories.Count < 2)
                    {

                        //обновляем подкатегорию
                        categoryService.UpdateSubCategory(subCat);


                        //удаляем главную категорию, так как в ней не осталось подкатегорий
                        categoryService.DeleteMainCategory(prevMainCat);

                        return RedirectToAction("Index");
                    }

                }

                categoryService.UpdateSubCategory(subCat);

                return RedirectToAction("Index");

            }

            IEnumerable<MainCategory> categories = categoryService.GetAllMainCategories();
            int selectedCategory = subCategory.MainCategoryId;

            SelectList category = new SelectList(categories, "Id", "CategoryName", selectedCategory);
            ViewBag.Category = category;

            return View(subCategory);
        }

        // GET: Category/Delete/5
        public ActionResult DeleteSubCategory(int id)
        {
            var subCategory = categoryService.GetSubCategoryById(id);

            if (subCategory != null)
            {
                return View(subCategory);
            }
            else
            {
                return View("Error", new string[] { "Подкатегория не найдена" });
            }


        }

        // POST: Category/Delete/5
        [HttpPost]
        [ActionName("DeleteSubCategory")]
        public ActionResult DeleteSubCategoryPost(int id)
        {

            var subCategory = categoryService.GetSubCategoryById(id);
            var category = categoryService.GetMainCategoryById(subCategory.MainCategoryId);

            if (subCategory != null)
            {
                if (category.SubCategories.Count < 2)
                {
                    //удаляем главную категорию если данная подкатегория была в ней последней
                    categoryService.DeleteMainCategory(category);
                }

                
                categoryService.DeleteSubCategory(subCategory);
            }
            else
            {
                return View("Error", new string[] { "Подкатегория не найдена" });
            }

            return RedirectToAction("Index");

        }



    }
}
