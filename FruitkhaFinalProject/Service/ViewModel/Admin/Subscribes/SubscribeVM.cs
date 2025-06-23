using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModel.Admin.Subscribes
{
    public class SubscribeVM
    {
        public string Email { get; set; } = null!;
        public DateTime SubscribedDate { get; set; }
    }
}
