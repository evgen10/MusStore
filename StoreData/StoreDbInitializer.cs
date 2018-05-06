using Microsoft.AspNet.Identity.EntityFramework;
using StoreData.Identity;
using StoreModel.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreData
{

    public class StoreDbInitializer : DropCreateDatabaseIfModelChanges<StoreContext>
    {
        protected override void Seed(StoreContext context)
        {

            List<City> cities = new List<City>
            {
                new City{ Name = "Караганда" },
                new City{ Name = "Астана"}

            };

            foreach (var item in cities)
            {
                context.Cities.Add(item);
            }


            ApplicationUserManager userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            ApplicationRoleManager roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));

            string roleName = "Administrator";
            

            string userName = "Admin";
            string password = "qwert123";
            string email = "admin@mail.ru";


            roleManager.Create(new ApplicationRole(roleName));
            roleManager.Create(new ApplicationRole("User"));

            userManager.Create(new ApplicationUser() { UserName = userName, Email = email, FirstName = "Evgen", LastName = "Chernyshkov", CityId = context.Cities.FirstOrDefault().Id }, password);

            ApplicationUser user = userManager.FindByName("Admin");

            userManager.AddToRole(user.Id, roleName);



            CreateBrands(context);
            CreateMainCategory(context);
            //CreateProduct(context);

            context.SaveChanges();
        }


        private void CreateBrands(StoreContext db)
        {
            List<Brand> brands = new List<Brand>
            {
                new Brand{ Name = "Yamaha"},
                new Brand{ Name = "Fender"  }
            };

            foreach (var brand in brands)
            {
                db.Brands.Add(brand);
            }


        }



        private void CreateMainCategory(StoreContext db)
        {
            List<MainCategory> mainCategories = new List<MainCategory>()
            {
                new MainCategory
                {
                    CategoryName = "Гитары",
                    SubCategories = new List<SubCategory>
                    {
                        new SubCategory{ CategoryName = "Аккустические"},
                        new SubCategory{ CategoryName = "Электрогитары"},
                        new SubCategory{ CategoryName = "Бас-гитары"}
                    }

                },

                new MainCategory
                {
                    CategoryName = "Клавишные",
                    SubCategories = new List<SubCategory>
                    {
                        new SubCategory{ CategoryName = "Рояли"},
                        new SubCategory{ CategoryName = "Пианино"},
                        new SubCategory{ CategoryName = "Синтезаторы"}
                    }



                },
                new MainCategory
                {

                    CategoryName = "Ударные",
                    SubCategories = new List<SubCategory>
                    {
                        new SubCategory{ CategoryName = "Барабаны"},
                        new SubCategory{ CategoryName = "Тарелки"},
                        new SubCategory{ CategoryName = "Перкуссия"}
                    }},

                new MainCategory
                {
                    CategoryName = "Оркестровые",
                    SubCategories = new List<SubCategory>
                    {
                        new SubCategory{ CategoryName = "Духовые"},
                        new SubCategory{ CategoryName = "Струнные"}
                    }


                }



            };

            foreach (var category in mainCategories)
            {
                db.MainCategories.Add(category);
            }

        }

        private void CreateProduct(StoreContext db)
        {

            List<Product> products = new List<Product>
            {
                new Product
                {

                  SubCategoryId = 1,
                  Brand = new Brand { Name = "Yamaha" },
                  Title = "F310",
                  Price = 180000,
                  Description = " Good dff ",
                  //Count = 5,
                  OrderCount = 0,
                  //IsInStock = true

                },

                new Product
                {

                  SubCategoryId = 2,
                  Brand = new Brand { Name = "Fender" },
                  Title = "AS552",
                  Price = 1120000,
                  Description = " Good  dff ",
                  //Count = 5,
                  OrderCount = 0,
                  //IsInStock = true

                }

            };

            foreach (var product in products)
            {
                db.Products.Add(product);
            }

        }

    }
}
