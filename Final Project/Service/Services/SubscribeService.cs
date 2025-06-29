using AutoMapper;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Repositories.Interfaces;
using Service.DTOs.Admin.Categories;
using Service.DTOs.Admin.Products;
using Service.Helpers.Exceptions;
using Service.Services.Interfaces;
using Service.ViewModel.Admin.Subscribes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class SubscribeService : ISubscribeService
    {
        //private readonly IMapper mapper;
        //private readonly ISubscribeRepository subscribeRepository;

        //public SubscribeService(IMapper _map , ISubscribeRepository _subscribeRepository)
        //{
        //    mapper = _map;
        //    subscribeRepository = _subscribeRepository;
        //}
        //public async Task<bool> CreateAsync(SubscribeCreateVM vm)
        //{
        //    if (vm == null) return false;

        //    if (string.IsNullOrWhiteSpace(vm.Email))
        //        throw new NotFoundException("Category is null");

        //    var model = new Subscribe
        //    {
        //        Email = vm.Email,
        //        SubscribedDate = DateTime.Now,

        //    };

        //    await subscribeRepository.CreateAsync(model);
        //    return true;
        //}



        ////public Task DeleteAsync(int id)
        ////{
        ////    throw new NotImplementedException();
        ////}

        ////public Task EditAsync(int? id, SubscribeEditVM model)
        ////{
        ////    throw new NotImplementedException();
        ////}

        //public async Task<IEnumerable<SubscribeVM>> GetAllAsync()
        //{
        //    return mapper.Map<IEnumerable<SubscribeVM>>(await subscribeRepository.GetAllAsync());
        //}

        ////public Task<SubscribeVM> GetAsync(int id)
        ////{
        ////    throw new NotImplementedException();
        ////}
        ///





        private readonly Db _context;
        private readonly IEmailService _emailService;

        public SubscribeService(Db context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task AddAsync(string email)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                var newsletter = new Subscribe { Email = email };
                _context.Subscribe.Add(newsletter);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<List<Subscribe>> GetAllAsync()
        {
            return await _context.Subscribe
                .OrderByDescending(n => n.CreatedDate)
                .ToListAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var newsletter = await _context.Subscribe.FindAsync(id);
            if (newsletter != null)
            {
                _context.Subscribe.Remove(newsletter);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> CheckEmailExistsAsync(string email)
        {
            return await _context.Subscribe.AnyAsync(n => n.Email.ToLower() == email.ToLower());
        }
    }
}
