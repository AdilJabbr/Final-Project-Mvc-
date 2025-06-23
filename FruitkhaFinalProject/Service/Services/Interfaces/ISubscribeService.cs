using Microsoft.EntityFrameworkCore.Query;
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
       // Task<SubscribeVM> GetAsync(int id);
        Task<IEnumerable<SubscribeVM>> GetAllAsync();
        Task CreateAsync(SubscribeCreateVM entity);
      //  Task EditAsync(int? id, SubscribeEditVM model);
       // Task DeleteAsync(int id);
    }
}
