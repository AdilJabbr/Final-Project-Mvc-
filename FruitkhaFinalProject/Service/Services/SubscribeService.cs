using AutoMapper;
using Domain.Models;
using Repository.Repositories.Interfaces;
using Service.DTOs.Admin.Products;
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
        private readonly IMapper mapper;
        private readonly ISubscribeRepository subscribeRepository;

        public SubscribeService(IMapper _map , ISubscribeRepository _subscribeRepository)
        {
            mapper = _map;
            subscribeRepository = _subscribeRepository;
        }
        public async Task CreateAsync(SubscribeCreateVM entity)
        {
            if (entity is null) throw new ArgumentNullException();

            var product = mapper.Map<Subscribe>(entity);
            await subscribeRepository.CreateAsync(product);
            await subscribeRepository.SaveChanges();
        }

      

        //public Task DeleteAsync(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task EditAsync(int? id, SubscribeEditVM model)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<IEnumerable<SubscribeVM>> GetAllAsync()
        {
            return mapper.Map<IEnumerable<SubscribeVM>>(await subscribeRepository.GetAllAsync());
        }

        //public Task<SubscribeVM> GetAsync(int id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
