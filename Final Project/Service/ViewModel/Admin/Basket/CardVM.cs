using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModel.Admin.Basket
{
    public class CardVM
    {
        public List<BasketItemVM> Product { get; set; } = new List<BasketItemVM>();
        public int Count { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
