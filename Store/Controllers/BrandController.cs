using StoreBL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Store.Models;
using System.Web.Mvc;
using AutoMapper;
using StoreModel.Models;

namespace Store.Controllers
{
    public class BrandController : Controller
    {
        private readonly IBrandService brandService;

        public BrandController(IBrandService brandService)
        {
            this.brandService = brandService;
        }

        public ActionResult Index()
        {
            var brands = brandService.GetAllBrands();

            var brandModels = Mapper.Map<IEnumerable<Brand>, IEnumerable<BrandViewModel>>(brands);


            return View(brandModels);
        }
        
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(BrandViewModel brand)
        {

            var brnd = Mapper.Map<BrandViewModel, Brand>(brand);

            if (brandService.Exists(brnd))
            {
                ModelState.AddModelError("Name", "Объект с таким именем уже существует.");
            }


            if (ModelState.IsValid)
            {              

                brandService.CreateBrand(brnd);

                return RedirectToAction("Index", "Brand");
            }

            return View(brand);
        }
        
        public ActionResult Edit(int id)
        {
            var brand = brandService.GetBrandById(id);

            if (brand != null)
            {
                var brnd = Mapper.Map<Brand, BrandViewModel>(brand);
                return View(brnd);
            }

            return RedirectToAction("Index", "Brand");

        }
        
        [HttpPost]
        public ActionResult Edit(BrandViewModel brand)
        {     
           

            if (ModelState.IsValid)
            {
                var brnd = Mapper.Map<BrandViewModel, Brand>(brand);
                brandService.UpdateBrand(brnd);

                return RedirectToAction("Index");
                
            }

            return View(brand);

        }
        
        public ActionResult Delete(int id)
        {
            var brand = brandService.GetBrandById(id);

            if (brand!=null)
            {
                return View(brand);
            }

            return RedirectToAction("Index");

        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteBrand(int id)
        {

            var brand = brandService.GetBrandById(id);

            if (brand!=null)
            {
                brandService.DeleteBrand(brand);
               
            }

            return RedirectToAction("Index");

        }



    }
}