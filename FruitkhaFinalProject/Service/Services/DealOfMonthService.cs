using AutoMapper;
using Final_Project.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;
using Service.DTOs.Admin.Products;
using Service.Services.Interfaces;
using Service.ViewModel.Admin.DealOfMonth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class DealOfMonthService : IDealOfMonthService
    {
        private readonly IProductRepository productRepo;
        public DealOfMonthService(IProductRepository productRepository)
        {
            productRepo = productRepository;
        }

        public async Task<DealOfMonthVM> DealOfMonthAsync()
        {

            var product = await productRepo.GetAllIncludes().OrderBy(p => p.Price).FirstOrDefaultAsync();

            var dealOfMonthVM = new DealOfMonthVM
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Categories = product.Category.Name,
                Brand = product.Brand.Name,
                Count = product.Count,
                Description = product.Description,
                MainImage = product.ProductImages.FirstOrDefault(x=>x.IsMain ==true).Name
            };

            return dealOfMonthVM;          
        }

 
    }
}
