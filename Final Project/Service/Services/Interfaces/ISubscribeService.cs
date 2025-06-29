using Domain.Models;
using Microsoft.EntityFrameworkCore.Query;
using Service.DTOs.Admin.Categories;
using Service.DTOs.Admin.Products;
using Service.ViewModel.Admin.Subscribes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ISubscribeService
    {
       //// Task<SubscribeVM> GetAsync(int id);
       // Task<IEnumerable<SubscribeVM>> GetAllAsync();
       // Task<bool> CreateAsync(SubscribeCreateVM vm);

       // //  Task EditAsync(int? id, SubscribeEditVM model);
       // // Task DeleteAsync(int id);


        Task AddAsync(string email);
        Task<List<Subscribe>> GetAllAsync();
        Task DeleteAsync(int id);
        Task<bool> CheckEmailExistsAsync(string email);
    }
}
