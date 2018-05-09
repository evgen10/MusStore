using Store.Models;
using StoreBL.Services.Interfaces;
using StoreData;
using StoreData.Infrastructure;
using StoreModel.Models;
using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web;

using System.Web.Mvc;
using AutoMapper;

namespace Store.Controllers
{    
    public class HomeController : Controller
    {    
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;
        private readonly IBrandService brandService;
        
        public HomeController(ICategoryService categoryService, IProductService productService, IBrandService brandService, IUserService userService)
        {
            this.categoryService = categoryService;
            this.productService = productService;
            this.brandService = brandService;

        }

        // GET: Home
                
        public ActionResult Index()
        {
            int topCount = 10;

            //самые покупаемые товары
            var top = productService.GetTopProducts(topCount);

            var products = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(top);

            return View(products);
        }
        
      
        public PartialViewResult Menu()
        {
            var category = categoryService.GetAllMainCategories();

            return PartialView(category);
        }
        
       
        public ActionResult List(int id = 0)
        {
            //товары по определенной подкатегории
            var product = productService.GetProductsByCategory(id);

            var brand = brandService.GetAllBrands();

            ViewBag.Category = categoryService.GetSubCategoryById(id).CategoryName;                     

            var products = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(product);
                             

            return View(products);

        }


    }
}