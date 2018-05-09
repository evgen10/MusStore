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
                new City{ Name = "Астана"},
                new City{ Name = "Алма-Ата"},
                new City{ Name = "Шымкент" },
                new City{ Name = "Актобе"},
                new City{ Name = "Тараз"},
                new City{ Name = "Павлодар" },
                new City{ Name = "Усть-Каменогорск"},
                new City{ Name = "Семей"},
                new City{ Name = "Костанай"},
                new City{ Name = "Уральск"}
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

            context.SaveChanges();


            CreateBrands(context);
            CreateMainCategory(context);

            CreateProduct(context);


        }


        private void CreateBrands(StoreContext db)
        {
            List<Brand> brands = new List<Brand>
            {
                new Brand{ Name = "Yamaha"},
                new Brand{ Name = "Fender"},
                new Brand{ Name ="Jackson" },
                new Brand{ Name ="Admira" },
                new Brand{ Name ="Mapex" },
                new Brand{ Name ="Soundking" },
                new Brand{ Name ="Hohner" },
                new Brand{ Name ="Casio" },
                new Brand{ Name ="Ritmuller" }
            };

            foreach (var brand in brands)
            {
                db.Brands.Add(brand);
            }


            db.SaveChanges();

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
                        new SubCategory{ CategoryName = "Синтезаторы"},
                      
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

            db.SaveChanges();

        }

        private void CreateProduct(StoreContext db)
        {

            List<Product> products = new List<Product>
            {
                new Product
                {
                  SubCategoryId = 1,
                  BrandId = 1,
                  Title = "Takamine P6NC BSB",
                  Price = 800000,
                  Description = " Электроакустическая гитара, верхняя дека из цельной ели, обечайка и " +
                                "задняя дека из волнистого клёна, накладка грифа из палисандра, бридж из палисандра, " +
                                "предусилитель CT4-DX, цвет Brown Sunburst, кейс в комплекте. ",
                  OrderCount = 18,

                },


                new Product
                {
                  SubCategoryId = 1,
                  BrandId = 2,
                  Title = "Fender F-1020 SCE",
                  Price = 166000,
                  Description = "Акустическая гитара, форма дредноут, топ из массива кедра, задняя дека, гриф и обечайка из красного дерева;" +
                                 " накладка на гриф из палисандра, 20 ладов, ширина верхнего порожка 43 мм, мензура 643 мм, скалопированные Х-образные пружины в деке," +
                                 " хромированные литые колки в винтажном исполнении, бридж из палисандра, натуральный цвет.",
                  OrderCount = 0,

                },


                new Product
                {
                  SubCategoryId = 1,
                  BrandId = 4,
                  Title = "Cort SFX AB NAT",
                  Price = 710000,
                  Description = "Электроакустическая гитара, тип корпуса SFX с вырезом, гриф из красного дерева, " +
                                 "накладка из палисандра, мензура 647.7 мм, бридж из палисандра, колки никелированные Die Cast," +
                                 " фигурная окантовка корпуса c акриловым орнаментом, натуральный цвет.",
                  OrderCount = 0,

                },



                 new Product
                {
                  SubCategoryId = 2,
                  BrandId = 4,
                  Title = "Cort SFX AB NAT",
                  Price = 710000,
                  Description = "Электроакустическая гитара, тип корпуса SFX с вырезом, гриф из красного дерева, " +
                                 "накладка из палисандра, мензура 647.7 мм, бридж из палисандра, колки никелированные Die Cast," +
                                 " фигурная окантовка корпуса c акриловым орнаментом, натуральный цвет.",
                  OrderCount = 0,

                },


                   new Product
                {
                  SubCategoryId = 2,
                  BrandId = 5,
                  Title = "Ravelle Elite RVLE08-SBK",
                  Price = 188000,
                  Description = "Электрогитара, корпус из красного дерева, верхняя дека из" +
                  " волнистого клёна 5A Quilted 15 мм, гриф из красного дерева глубокой вклейки, накладка из палисандра, верхний порожек Graphtec Trem Nut, " +
                  "локовые колки, бридж Gotoh GE103B Tune-o-Matic, струнодержатель Gotoh GE101A " +
                  "Tailpiece, хамбакер Seymour Duncan SH4(JB) + Sustainer, темброблоки - громкость, тембр, 3-позиционный переключатель, " +
                  "переключатели режимов сустейнера, в комплекте жёсткий кейс.",
                  OrderCount = 24,

                },

                     new Product
                {
                  SubCategoryId = 2,
                  BrandId = 3,
                  Title = "CORT M600T BC",
                  Price = 85000,
                  Description = "Электрогитара, 22 лада, корпус из красного дерева с верхом из волнистого клена, гриф из красного дерева формой Modern "+
                  " (вклеенный), накладка из палисандра, звукосниматели H/H (EMG-HZ OPEN SA1(F) & TB1(R), 3-х позиционный переключатель звукоснимателей, бридж Wilkinson WVPC Tremolo, регуляторы V/T (с пуш-пул переключателем), хромированная фурнитура, колки Die-cast, мензура 629 мм, окантовка цвета слоновой кости, цвет Black Cherry (темная вишня).",
                  OrderCount = 0,

                },

                 new Product
                {
                  SubCategoryId = 3,
                  BrandId = 7,
                  Title = "Jazz Bass",
                  Price = 253000,
                  Description ="Бас-гитара, корпус из ольхи, гриф из клена, накладка на гриф из палисандра, 20 ладов, мензура 34' (864 мм), регуляторы - громкость, тон, бридж 4-Saddle American Vintage Bass, колки Fender '70s Vintage-Style Stamped Open-Gear, хромированная фурнитура, черный цвет.",
                  OrderCount = 44,

                },


                 new Product
                {
                  SubCategoryId = 3,
                  BrandId = 5,
                  Title = "Cort Action Bass ACTION BASS TR",
                  Price = 35000,
                  Description ="Бас-гитара, корпус из агатиса, кленовый гриф на болтах, накладка грифа из палисандра, мензура 34, верхний порожек из графита, 24 лада, бридж EB7, звукосниматели Power Sound Jazz & P, 2-х полосный активный эквалайзер, хромированная механика, цвет Transparent Red (прозрачный красный), комплектация: гитара, ключи.",
                  OrderCount = 0,

                },



                 new Product
                {
                  SubCategoryId = 3,
                  BrandId = 5,
                  Title = "Cort Action Bass BK",
                  Price = 253000,
                  Description ="Бас-гитара, корпус из агатиса, кленовый гриф на болтах, накладка грифа из палисандра, мензура 34, верхний порожек из графита, 24 лада, бридж EB7, звукосниматели Power Sound Jazz & P, 2-х полосный активный эквалайзер, хромированная механика, цвет черный BK (Black), комплектация: гитара, ключи",
                  OrderCount = 0,

                },



                 new Product
                {
                  SubCategoryId = 4,
                  BrandId = 5,
                  Title = "Cort Action Bass BK",
                  Price = 253000,
                  Description ="",
                  OrderCount = 0,

                },

                 new Product
                {
                  SubCategoryId = 4,
                  BrandId = 5,
                  Title = "GP160R1 A112",
                  Price = 3400000,
                  Description ="Кабинетный акустический рояль, длина 160 см, цвет белый полированный, в комлекте с банкеткой и чехлом.",
                  OrderCount = 55,

                },

                 new Product
                {
                  SubCategoryId = 4,
                  BrandId = 8,
                  Title = "GP148R1 A111",
                  Price = 3000000,
                  Description ="Акустический кабинетный рояль, элегантный дизайн, механика Rittmuller High Action, молоточки Rittmuller Grand Hammers из прочной древесины, мост из белого бука и клёна, головки покрыты войлоком, вирбельбанк инструмента из 17-слойной доски клена, чугунная рама инструмента выносит любое давление струн, колки из немецкой никелированной стали, 3 педали, черный цвет.",
                  OrderCount = 0,

                },


                   new Product
                {
                  SubCategoryId = 5,
                  BrandId = 4,
                  Title = "UP118R2 A107",
                  Price = 15000000,
                  Description ="Компактное акустическое пианино, элегантный дизайн, рипки и резонирующая дека из ели, механика Rittmuller High Action, молоточки Rittmuller Deluxe из прочной древесины, головки покрыты войлоком, вирбельбанк инструмента из 17-слойной доски клена, чугунная рама инструмента выносит любое давление струн, колки из немецкой никелированной стали, цвет - орех.",
                  OrderCount = 15,

                },

                     new Product
                {
                  SubCategoryId = 5,
                  BrandId = 6,
                  Title = "PX-770 BKC7",
                  Price = 450000,
                  Description ="Цифровое пианино, 88 фортепианных клавиш с динамической чувствительностью и молоточковым механизмом, многомерный источник звука AiR, полифония 128 нот, 19 тонов, эффекты яркости, хоруса и реверберации; имитатор акустики концертного зала, демпферный резонанс, функция дуэт, метроном, MIDI, устройство записи композиций, 3 педали, цвет черного дерева.",
                  OrderCount = 0,

                },

                       new Product
                {
                  SubCategoryId = 5,
                  BrandId = 4,
                  Title = "UP110RB A5C1",
                  Price = 1350000,
                  Description ="Акустическое пианино, корпус из отборной ели, чугунная рама, колки из немецкой никелированой стали, струны Rösslau, дека из ели высочайшего качества, мост из немецкого бука, молотки Rittmuller Deluxe, механика Rittmuller High Action, 3 педали, цвет Cherry Satin.",
                  OrderCount = 0,

                },



                 new Product
                {
                  SubCategoryId = 6,
                  BrandId = 4,
                  Title = "CTK-245H2",
                  Price = 540000,
                  Description ="Синтезатор базового уровня, 61 чуствительная клавиша рояльного типа, 120 тембров, 70 ритмов, 50 ритмов танцевальной музыки, 32 ноты полифонии, эффекты - танцевальной музыки, обучающая функция Лайт Лекшн, выход для наушников, монохромный ЖК-дисплей, чёрный цвет, блок питания в комплекте.",
                  OrderCount = 53,

                },

                    new Product
                {
                  SubCategoryId = 6,
                  BrandId = 2,
                  Title = "CTK-1500K7",
                  Price = 540000,
                  Description ="Синтезатор, 49 стандартных клавиш, 12-нотная полифония (максимально), 100 тембров, 100 стилей, 50 пьес для разучивания, ЖК дисплей, динамики 2 x 1,6 Вт, источник питания блок питания или 6 Аккумуляторов AA ( приобретается дополнительно).",
                  OrderCount = 0,

                },
                          new Product
                {
                  SubCategoryId = 6,
                  BrandId = 8,
                  Title = "KP100",
                  Price = 79000,
                  Description ="Синтезатор базового уровня, 61-клавишная синтезаторная клавиатура, автоаккомпонимент 220, полифония 128 нот, 633 пресетов, 32 звука банка любимых звуков .favorites, 2 х 3-ваттная 2-спикерная стерео-система, разделение/наложение, транспонирование, эффекты, 1 порт USB, со встроенным интерфейсом MIDI, в комплекте блок питания и пюпитр.",
                  OrderCount = 0,

                },


                   new Product
                {
                  SubCategoryId = 7,
                  BrandId = 8,
                  Title = "Mapex Mars Series 5",
                  Price = 201000,
                  Description ="Piece Rock Shell Pack MA529SFBZW",
                  OrderCount = 16,

                },


                   new Product
                {
                  SubCategoryId = 7,
                  BrandId = 8,
                  Title = "MP446SJSV",
                  Price = 94000,
                  Description ="Ударная установка, 7-слойные кленовые корпуса всех барабанов, корпусы малого барабана и томов толщиной 5.8 мм, 7.2-миллиметровый стенки корпуса большого барабана, новая конструкция утюжков с меньшей площадью опорной поверхности и меньшей массой, штампованные обода Mapex Powerhoops толщиной 2.3 мм, упругие вибропоглощающие опоры ножек напольных томов,",
                  OrderCount = 0,

                },




                   new Product
                {
                  SubCategoryId = 7,
                  BrandId = 8,
                  Title = "Piece Studioease Fast Shell Pack",
                  Price = 104000,
                  Description ="6-ти предметная ударная установка, корпуса барабанов 6-ти слойная комбинация с чередованием береза/клен/береза. ",
                  OrderCount = 0,

                }          
                   
            };

            foreach (var product in products)
            {
                db.Products.Add(product);
            }

            db.SaveChanges();

        }

    }
}
