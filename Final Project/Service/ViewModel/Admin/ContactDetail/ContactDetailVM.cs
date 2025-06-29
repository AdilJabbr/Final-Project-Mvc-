using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModel.Admin.ContactDetail
{
    public class ContactDetailVM
    {
        public int Id { get; set; }
        public string ShopCountry { get; set; }
        public string ShopCity { get; set; }
        public string ShopAddress { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string StartingWorkDay { get; set; }
        public string FinishingWorkDay { get; set; }
        public DateTime StartWorkTime { get; set; }
        public DateTime FinishWorkTime { get; set; }
    }
}
