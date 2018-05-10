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
    public class ProductController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IBrandService brandService;
        private readonly IProductService productService;

        public ProductController(ICategoryService categoryService, IBrandService brandService, IProductService productService)
        {
            this.categoryService = categoryService;
            this.brandService = brandService;
            this.productService = productService;
        }

        //выводит все продукты
        public ActionResult Index()
        {
            try
            {
                var product = productService.GetAll();

                var prod = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(product);

                return View(prod);
            }
            catch (Exception e)
            {
                return View("Error", new string[] { e.Message });

            }
        }

        public ActionResult Create()
        {
            try
            {
                IEnumerable<MainCategory> categories = categoryService.GetAllMainCategories();
                int selectedCategory = categories.FirstOrDefault().Id;
                IEnumerable<SubCategory> subCategories = categoryService.GetSubCategoriesByMain(selectedCategory);
                IEnumerable<Brand> brands = brandService.GetAllBrands();


                SelectList category = new SelectList(categories, "Id", "CategoryName", selectedCategory);
                ViewBag.Category = category;

                SelectList subCategory = new SelectList(subCategories, "Id", "CategoryName");
                ViewBag.SubCategory = subCategory;

                SelectList brand = new SelectList(brands, "Id", "Name");
                ViewBag.Brand = brand;

                return View();
            }
            catch (Exception e)
            {
                return View("Error", new string[] { e.Message });

            }
        }

        [HttpPost]
        public ActionResult Create(ProductCreateViewModel productModel, HttpPostedFileBase fileUpload)
        {
            try
            {
                if (productModel.Price < 0)
                {
                    ModelState.AddModelError("Price", "Цена не может быть меншь 0!");
                }


                if (ModelState.IsValid)
                {
                    Product product = Mapper.Map<ProductCreateViewModel, Product>(productModel);

                    if (fileUpload != null)
                    {
                        product.Image = new byte[fileUpload.ContentLength];
                        fileUpload.InputStream.Read(product.Image, 0, fileUpload.ContentLength);
                    }

                    productService.CreateProduct(product);

                    return RedirectToAction("Index");

                }

                IEnumerable<MainCategory> categories = categoryService.GetAllMainCategories();
                int selectedCategoryId = productModel.MainCategoryId;

                IEnumerable<SubCategory> subCategories = categoryService.GetSubCategoriesByMain(selectedCategoryId);
                int selectedSubCategoryId = productModel.SubCategoryId;

                IEnumerable<Brand> brands = brandService.GetAllBrands();
                int selectedBrandId = productModel.BrandId;


                SelectList category = new SelectList(categories, "Id", "CategoryName", selectedCategoryId);
                ViewBag.Category = category;

                SelectList subCategory = new SelectList(subCategories, "Id", "CategoryName", selectedSubCategoryId);
                ViewBag.SubCategory = subCategory;

                SelectList brand = new SelectList(brands, "Id", "Name", selectedBrandId);
                ViewBag.Brand = brand;


                return View(productModel);
            }
            catch (Exception e)
            {
                return View("Error", new string[] { e.Message });

            }
        }       


        public ActionResult GetSubCategories(int id)
        {
            try
            {
                var subCategories = categoryService.GetSubCategoriesByMain(id);

                return PartialView(subCategories);
            }
            catch (Exception e)
            {
                return View("Error", new string[] { e.Message });

            }
        }


        public ActionResult Delete(int id)
        {
            try
            {
                var product = productService.GetProductById(id);

                if (product != null)
                {
                    productService.DeleteProduct(product);
                }
                else
                {
                    return View("Error", new string[] { "Товар не найден" });
                }


                return RedirectToAction("Index");

            }
            catch (Exception e)
            {
                return View("Error", new string[] { e.Message });

            }
        }


        public ActionResult Edit(int id)
        {
            try
            {
                var prd = productService.GetProductById(id);

                if (prd != null)
                {

                    ProductCreateViewModel product = Mapper.Map<Product, ProductCreateViewModel>(prd);

                    IEnumerable<MainCategory> categories = categoryService.GetAllMainCategories();
                    int selectedCategoryId = product.MainCategoryId;

                    IEnumerable<SubCategory> subCategories = categoryService.GetSubCategoriesByMain(selectedCategoryId);
                    int selectedSubCategoryId = product.SubCategoryId;

                    IEnumerable<Brand> brands = brandService.GetAllBrands();
                    int selectedBrandId = product.BrandId;

                    SelectList category = new SelectList(categories, "Id", "CategoryName", selectedCategoryId);
                    ViewBag.Category = category;

                    SelectList subCategory = new SelectList(subCategories, "Id", "CategoryName", selectedSubCategoryId);
                    ViewBag.SubCategory = subCategory;

                    SelectList brand = new SelectList(brands, "Id", "Name", selectedBrandId);
                    ViewBag.Brand = brand;


                    return View(product);

                }
                else
                {
                    return View("Error", new string[] { "Продукт не найден" });
                }
            }
            catch (Exception e)
            {
                return View("Error", new string[] { e.Message });

            }

        }

        [HttpPost]
        public ActionResult Edit(ProductCreateViewModel product, HttpPostedFileBase fileUpload)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (fileUpload != null)
                    {

                        product.Image = new byte[fileUpload.ContentLength];
                        fileUpload.InputStream.Read(product.Image, 0, fileUpload.ContentLength);

                    }
                    else
                    {
                        var p = productService.GetProductById(product.Id);

                        if (p.Image != null)
                        {
                            product.Image = p.Image;
                        }


                    }

                    Product prd = Mapper.Map<ProductCreateViewModel, Product>(product);

                    productService.UpdateProduct(prd);

                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ProductId = product.Id;

                    IEnumerable<MainCategory> categories = categoryService.GetAllMainCategories();
                    int selectedCategoryId = product.MainCategoryId;

                    IEnumerable<SubCategory> subCategories = categoryService.GetSubCategoriesByMain(selectedCategoryId);
                    int selectedSubCategoryId = product.SubCategoryId;

                    IEnumerable<Brand> brands = brandService.GetAllBrands();
                    int selectedBrandId = product.BrandId;

                    SelectList category = new SelectList(categories, "Id", "CategoryName", selectedCategoryId);
                    ViewBag.Category = category;

                    SelectList subCategory = new SelectList(subCategories, "Id", "CategoryName", selectedSubCategoryId);
                    ViewBag.SubCategory = subCategory;

                    SelectList brand = new SelectList(brands, "Id", "Name", selectedBrandId);
                    ViewBag.Brand = brand;

                    var p = productService.GetProductById(product.Id);

                    if (p.Image != null)
                    {
                        product.Image = p.Image;
                    }

                    if (product.Image == null)
                    {
                        product.Image = new byte[0];
                    }

                    return View(product);
                }
            }
            catch (Exception e)
            {
                return View("Error", new string[] { e.Message });

            }

        }




        [AllowAnonymous]
        public ActionResult GetImage(int id)
        {
            try
            {
                var image = productService.GetProductById(id);
                return File(image.Image, "image/jpeg");
            }
            catch (Exception e)
            {
                return View("Error", new string[] { e.Message });

            }

        }


        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            try
            {
                var product = productService.GetProductById(id);

                if (product != null)
                {
                    var prd = Mapper.Map<Product, ProductViewModel>(product);

                    return View(prd);

                }
                else
                {
                    return View("Error", new string[] { "Продукт не найден" });
                }
            }
            catch (Exception e)
            {
                return View("Error", new string[] { e.Message });

            }
        }

    }
}