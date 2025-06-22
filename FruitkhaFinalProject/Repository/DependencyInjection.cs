using Microsoft.Extensions.DependencyInjection;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositoryLayer(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IAbtRepository, AbtRepository>();
            services.AddScoped<IBannerImageRepository, BannerImageRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IContactDetailRepository, ContactDetailRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IDealOfMonthRepository, DealOfMonthRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IHeroAreaRepository, HeroAreaRepository>();
            services.AddScoped<IHeroUnderSectionRepository, HeroUnderSectionRepository>();
            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddScoped<IProductImagesRepository, ProductImagesRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISubscribeRepository, SubscribeRepository>();
            services.AddScoped<IWhyFruitkaRepository, WhyFruitkaRepository>();
            return services;
        }
    }
}
