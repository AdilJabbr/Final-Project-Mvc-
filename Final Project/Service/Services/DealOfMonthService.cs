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
        private readonly IMapper mapper;

        public DealOfMonthService(IProductRepository productRepository, IMapper _mapper)
        {
            productRepo = productRepository;
            mapper = _mapper;
        }

        //public async Task<DealOfMonthVM> DealOfMonthAsync()
        //{
        //    //throw new NotImplementedException();

        //    var product = await productRepo.GetAllWithIncludes(p => p.Price).OrderBy(p => p.Price).FirstOrDefaultAsync();

        //    return mapper.Map<DealOfMonthVM>(product);


           
        //}


    }
}
