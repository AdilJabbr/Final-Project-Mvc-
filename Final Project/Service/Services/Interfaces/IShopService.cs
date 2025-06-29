using Service.ViewModel.Admin.Shop;

namespace Service.Services.Interfaces
{
    public interface IShopService
    {
        Task<ShopVM> GetShop();
        Task<ShopVM> GetShop(int pageRow, string? search, int? sort, List<int>? categoryIds, List<int>? brandIds, int? maxPrice = null, int? minPrice = null);
    }
}
    