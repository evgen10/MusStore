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

    [Authorize(Roles = "Administrator")]
    public class BrandController : Controller
    {
        private readonly IBrandService brandService;

        public BrandController(IBrandService brandService)
        {
            this.brandService = brandService;
        }

        public ActionResult Index()
        {
            try
            {
                var brands = brandService.GetAllBrands();

                var brandModels = Mapper.Map<IEnumerable<Brand>, IEnumerable<BrandViewModel>>(brands);

                return View(brandModels);
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
                return View();
            }
            catch (Exception e)
            {
                return View("Error", new string[] { e.Message });

            }
        }

        [HttpPost]
        public ActionResult Create(BrandViewModel brand)
        {
            try
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
            catch (Exception e)
            {
                return View("Error", new string[] { e.Message });

            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                var brand = brandService.GetBrandById(id);

                if (brand != null)
                {
                    var brnd = Mapper.Map<Brand, BrandViewModel>(brand);
                    return View(brnd);
                }
                else
                {
                    return View("Error", new string[] { "Бренд не найден" });
                }
            }
            catch (Exception e)
            {
                return View("Error", new string[] { e.Message });

            }

        }

        [HttpPost]
        public ActionResult Edit(BrandViewModel brand)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var brnd = Mapper.Map<BrandViewModel, Brand>(brand);
                    brandService.UpdateBrand(brnd);

                    return RedirectToAction("Index");

                }

                return View(brand);
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
                var brand = brandService.GetBrandById(id);

                if (brand != null)
                {
                    return View(brand);
                }
                else
                {
                    return View("Error", new string[] { "Бренд не найден" });
                }
            }
            catch (Exception e)
            {
                return View("Error", new string[] { e.Message });

            }

        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteBrand(int id)
        {
            try
            {
                var brand = brandService.GetBrandById(id);

                if (brand != null)
                {
                    brandService.DeleteBrand(brand);

                }
                else
                {
                    return View("Error", new string[] { "Бренд не найден" });
                }

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View("Error", new string[] { e.Message });

            }

        }

    }
}