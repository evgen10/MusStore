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
    [Authorize]
    public class HomeController : Controller
    {

        //UnitOfWork db = new UnitOfWork(new StoreContext());

        private readonly ICategoryService categoryService;
        private readonly IProductService productService;
        private readonly IBrandService brandService;
        private readonly IUserService userService;

        public HomeController(ICategoryService categoryService, IProductService productService, IBrandService brandService, IUserService userService)
        {
            this.categoryService = categoryService;
            this.productService = productService;
            this.brandService = brandService;
            this.userService = userService;
        }

        // GET: Home

        [AllowAnonymous]
        public ActionResult Index()
        {
            int topCount = 10;

            var top = productService.GetTopProducts(topCount);

            var products = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(top);

            return View(products);
        }



        [AllowAnonymous]
        public PartialViewResult Menu()
        {
            var category = categoryService.GetAllMainCategories();



            return PartialView(category);
        }


        [AllowAnonymous]
        public ActionResult List(int id = 0)
        {
            var product = productService.GetProductsByCategory(id);

            var brand = brandService.GetAllBrands();

            ViewBag.Category = categoryService.GetSubCategoryById(id).CategoryName;

            //var image = imageService.GetImagesByProduct()

            var products = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(product);
                
                //from p in product
                //           from b in brand
                //           where p.BrandId == b.Id
                //           select new ProductViewModel
                //           {
                //               Id = p.Id,
                //               Title = p.Title,
                //               BrandName = b.Name,
                //               Discription = p.Description,
                //               Price = p.Price,
                //               Image = p.Image
                               

                //           };


            return View(products);

        }


        //public ActionResult OrderList()
        //{

        //    List<CartViewModel> orders = new List<CartViewModel>();

        //    IEnumerable<Order> or = db.Orders.GetAll();

        //    foreach (var item in or)
        //    {
        //   //     orders.Add(new CartViewModel { UserName = item.ApplicationUser.FirstName + " " + item.ApplicationUser.LastName, ProductName = item.Product.Title, Brand = item.Product.Brand.Name, DataCreate = item.CreateDate });
        //    }



        //    return View(orders);
        //}

        //public ActionResult AddOrder(int productId)
        //{
        //   string userId = User.Identity.GetUserId();



        //    Product product = db.Products.Get(productId);


        //    Order order = new Order
        //    {
        //        ApplicationUserId = userId,
        //        Product = product,
        //        OrderStatus = OrderStatus.IsInCart

        //    };


        //    db.Orders.Create(order);
        //    db.Save();


        //    return RedirectToAction("Index");

        //}


        //public ActionResult Create()
        //{

        //    ViewBag.Category = new SelectList(db.SubCategories.GetAll(), "Id", "CategoryName");
        //    ViewBag.Brand = new SelectList(db.Brands.GetAll(), "Id", "Name");


        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Create(Product product)
        //{
        //    db.Products.Create(product);
        //    db.Save();

        //    return RedirectToAction("Create");
        //}

        //[HttpGet]
        //public ActionResult CreateCategory()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult CreateCategory(MainCategory category)
        //{
        // //   categoryService.CreateMainCategory(category.CategoryName);


        //    return View();
        //}

        //public ActionResult CreateSubCategory()
        //{
        //    SelectList maincategoryId = new SelectList(categoryService.GetAllMainCategories(), "Id", "CategoryName");

        //    ViewBag.MainCategory = maincategoryId;

        //    return View();
        //}

        //[HttpPost]
        //public ActionResult CreateSubCategory(SubCategory category)
        //{
        //  //  categoryService.CreateSubCategory(category.CategoryName, category.MainCategoryId);

        //    return RedirectToAction("CreateSubCategory");


        //}

    }
}