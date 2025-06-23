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

            CreateMap<News, NewsVM>().ForMember(c => c.Image, opt => opt.MapFrom(cd => Path.Combine(baseUrl, "images", cd.Image)));
            CreateMap<NewsCreateVM, News>();
            CreateMap<NewsEditVM, News>()
                .ForMember(d => d.Image, opt => opt.Condition(s => s.Image is not null));

            CreateMap<Category, CategoryVM>();
            CreateMap<CategoryCreateVM, Category>();
            CreateMap<CategoryEditVM, Category>();

            CreateMap<Brands, BrandVM>().ForMember(c => c.Image, opt => opt.MapFrom(cd => Path.Combine(baseUrl, "images", cd.Image)));
            CreateMap<BrandCreateVM, Brands>();
            CreateMap<BrandEditVM, Brands>();

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
                       .ForMember(c => c.MainImage, opt => opt.MapFrom(cd => Path.Combine(baseUrl, "images", cd.ProductImages.FirstOrDefault(m => m.IsMain).Name)))
                       .ForMember(d => d.Categories, opt => opt.MapFrom(s => s.Category.Name))
                       //.ForMember(d => d.MainImage, opt => opt.MapFrom(s => s.ProductImages.FirstOrDefault(m => m.IsMain).Name))
                       .ForMember(d => d.Brand, opt => opt.MapFrom(m => new BrandVM
                       {
                           Id = m.Brand.Id,
                           Name = m.Brand.Name,
                           Image = m.Brand.Image,
                       }))
                       .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comment));

            CreateMap<ProductCreateVM, Product>();
            CreateMap<ProductImages, ProductImageVM>();
            CreateMap<Product, ProductDetailVM>()
                .ForMember(d => d.ProductImages, opt => opt.MapFrom(s => s.ProductImages.Select(img => new ProductImageVM
                {
                    Name = Path.Combine(baseUrl, "images", img.Name),
                    IsMain = img.IsMain
                }).ToList()))
                .ForMember(d => d.Categories, opt => opt.MapFrom(s => s.Category.Name))
                .ForMember(d => d.Brand, opt => opt.MapFrom(m => new BrandVM
                {
                   Id = m.Brand.Id,
                  Name = m.Brand.Name,
                  Image = m.Brand.Image,
                 }))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comment));

            CreateMap<ProductEditVM, Product>();



            //CreateMap<Product, DealOfMonthVM>()
            //          .ForMember(c => c.MainImage, opt => opt.MapFrom(cd => Path.Combine(baseUrl, "images", cd.ProductImages.FirstOrDefault(m => m.IsMain).Name)))
            //          .ForMember(d => d.Categories, opt => opt.MapFrom(s => s.Category.Name))
            //          //.ForMember(d => d.MainImage, opt => opt.MapFrom(s => s.ProductImages.FirstOrDefault(m => m.IsMain).Name))
            //          .ForMember(d => d.Brand, opt => opt.MapFrom(m => new BrandVM
            //          {
            //              Id = m.Brand.Id,
            //              Name = m.Brand.Name,
            //              Image = m.Brand.Image,
            //          }))
            //          .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comment));



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
