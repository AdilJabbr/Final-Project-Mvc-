using FluentValidation;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using service.services.ınterfaces;
using Service.DTOs.Admin.Categories;
using Service.Helpers;
using Service.Services;
using Service.Services.Interfaces;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountService = Service.Services.AccountService;
using ProductService = Service.Services.ProductService;

namespace Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServiceLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<UrlHelperService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICloudinaryManager, CloudinaryManager>();

            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IDealOfMonthService, DealOfMonthService>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<ISubscribeService, SubscribeService>();

              services.AddScoped<IWishlistService, WishlistService>();
              services.AddScoped<IBasketService, BasketService>();

            return services;
        }

    }
}
