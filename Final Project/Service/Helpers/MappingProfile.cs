using AutoMapper;
using Domain.Models;
using Final_Project.Models;
using Service.DTOs.Admin.Categories;
using Service.DTOs.Admin.Products;
using Service.ViewModel.Admin.Brands;
using Service.ViewModel.Admin.Comments;
using Service.ViewModel.Admin.Contact;
using Service.ViewModel.Admin.DealOfMonth;
using Service.ViewModel.Admin.Employees;
using Service.ViewModel.Admin.News;
using Service.ViewModel.Admin.Products;
using Service.ViewModel.Admin.Settings;
using Service.ViewModel.Admin.Subscribes;
using Stripe;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Product = Final_Project.Models.Product;

namespace Service.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            var request = HttpContextHelper.Current?.Request;
            string baseUrl = $"{request?.Scheme}://{request?.Host}{request?.PathBase}";

            CreateMap<Contact, ContactVM>();
            CreateMap<ContactCreateVM, Contact>();

            CreateMap<News, NewsCreateVM>().ReverseMap();
            CreateMap<News, NewsVM>().ReverseMap();
            CreateMap<News, NewsEditVM>().ReverseMap();

            CreateMap<Employee, EmployeeCreateVM>().ReverseMap();
            CreateMap<Employee, EmployeeVM>().ReverseMap();
            CreateMap<Employee, EmployeeEditVM>().ReverseMap();

            CreateMap<Category, CategoryVM>();
            CreateMap<CategoryCreateVM, Category>();
            CreateMap<CategoryEditVM, Category>();

            CreateMap<Brands, BrandVM>().ReverseMap();
            CreateMap<Brands, BrandCreateVM>().ReverseMap();
            CreateMap<Brands, BrandEditVM>().ReverseMap();

            CreateMap<Setting, SettingVM>();
            CreateMap<SettingCreateVM, Setting>();
            CreateMap<SettingEditVM, Setting>();

            CreateMap<Employee,EmployeeVM>();
            CreateMap<EmployeeCreateVM, Employee>();
            CreateMap<EmployeeEditVM, Employee>();


            CreateMap<Subscribe,SubscribeVM>();
            CreateMap<SubscribeCreateVM, Subscribe>();
            CreateMap<SubscribeEditVM, Subscribe>();

            CreateMap<Product, ProductVM>()
                    .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null))
                  .ForMember(dest => dest.Categories, opt => opt.MapFrom(src =>
                src.Category != null ? new List<CategoryVM> { new CategoryVM { Id = src.Category.Id, Name = src.Category.Name } } : new List<CategoryVM>()
            ))
                    .ReverseMap()
                    .ForMember(dest => dest.Category, opt => opt.Ignore());


            CreateMap<Product, ProductCreateVM>().ReverseMap();
            CreateMap<Product, ProductEditVM>().ReverseMap();




            CreateMap<ProductImages, ProductImageVM>().ReverseMap();
            CreateMap<ProductImages, ProductImageCreateVM>().ReverseMap();
            CreateMap<ProductImages, ProductImageEditVM>().ReverseMap();

            CreateMap<Product, DealOfMonthVM>()
                   .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null))
                 .ForMember(dest => dest.Categories, opt => opt.MapFrom(src =>
               src.Category != null ? new List<CategoryVM> { new CategoryVM { Id = src.Category.Id, Name = src.Category.Name } } : new List<CategoryVM>()
           ))
                   .ReverseMap()
                   .ForMember(dest => dest.Category, opt => opt.Ignore());

            CreateMap<Setting, SettingCreateVM>().ReverseMap();
            CreateMap<Setting, SettingVM>().ReverseMap();
            CreateMap<Setting, SettingEditVM>().ReverseMap();

            //CreateMap<Wishlist, WishlistDto>();
            //CreateMap<WishlistDto, Wishlist>();
            //CreateMap<WishlistProduct, WishlistProductDto>();
            //CreateMap<WishlistProductDto, WishlistProduct>();

            //CreateMap<Basket, BasketDto>()/*.ForMember(c => c.Image, opt => opt.MapFrom(cd => Path.Combine(baseUrl, "images", cd.Image)))*/;
            //CreateMap<BasketCreateDto, Basket>();
            //CreateMap<BasketProduct, BasketProductDto>().ReverseMap();
            ////CreateMap<BasketProductDto, BasketProduct>();

            CreateMap<Comment, CommentCreateVM>().ReverseMap();
            CreateMap<Comment, CommentUpdateVM>().ReverseMap();

            CreateMap<Comment, CommentVM>()
                   .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                   .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
                   .ReverseMap();
        }
    }
}
