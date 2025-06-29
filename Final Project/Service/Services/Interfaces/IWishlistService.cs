using Service.ViewModel.Admin.Wishlists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IWishlistService
    {
        Task<bool> AddToWishListAsync(int id);
        Task<int> WishlistCount();
        Task<WishListCardVM> WishListCardVM();
    }
}
