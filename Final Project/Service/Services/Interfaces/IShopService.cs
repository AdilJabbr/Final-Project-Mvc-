using Service.ViewModel.Admin.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IShopService
    {
        Task<ShopVM> GetShop();
        Task<ShopVM> GetShop(string? search, string? sort, int? categoryId = null);
    }
}
