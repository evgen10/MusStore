using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Store.Models;
using StoreModel.Models;

namespace Store.Mapping
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {                   


            Mapper.Initialize(

             cnf =>
             {
                 cnf.CreateMap<Product, ProductCreateViewModel>()
                .ForMember("MainCategoryId", opt => opt.MapFrom(p => p.SubCategory.MainCategoryId));
                 cnf.CreateMap<ProductCreateViewModel, Product>();

                 cnf.CreateMap<Product, ProductViewModel>()
                 .ForMember("BrandName", opt => opt.MapFrom(p => p.Brand.Name))
                 .ForMember("SubCategoryName", opt => opt.MapFrom(p => p.SubCategory.CategoryName));


                 cnf.CreateMap<RegisterViewModel, ApplicationUser>()
                 .ForMember("PhoneNumber", opt => opt.MapFrom(u=>u.Phone));

                 cnf.CreateMap<Brand, BrandViewModel>();
                 cnf.CreateMap<BrandViewModel, Brand>();

                 cnf.CreateMap<SubCategory, SubCategoryViewModel>();
                 cnf.CreateMap<SubCategoryViewModel, SubCategory>();

                 cnf.CreateMap<MainCategory, EditCategoryViewModel>()
                 .ForMember("Name", opt => opt.MapFrom(c=>c.CategoryName));
                 cnf.CreateMap<EditCategoryViewModel, MainCategory>()
                 .ForMember("CategoryName", opt =>opt.MapFrom(c=>c.Name));

                 cnf.CreateMap<Order, CartViewModel>()
                 .ForMember("Brand", opt => opt.MapFrom(b => b.Product.Brand.Name))
                 .ForMember("ProductName",opt => opt.MapFrom(p =>p.Product.Title))
                 .ForMember("Price",opt =>opt.MapFrom(p=>p.Product.Price));


             }



            );




        }

    }
}