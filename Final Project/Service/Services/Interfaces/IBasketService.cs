using Service.ViewModel.Admin.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IBasketService
    {
        Task<bool> AddToBasketAsync(int id, int count = 1);
        Task<CardVM> GetBasketAsync();
        Task<int> GetBasketCountAsync();
        Task<decimal> GetBasketTotalAsync();
        Task<bool> DecreaseFromBasketAsync(int productId);
        Task<bool> RemoveAllFromBasketAsync(int productId);
    }
}
