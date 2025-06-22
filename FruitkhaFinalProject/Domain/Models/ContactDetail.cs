using Domain.Models.Common;

namespace Final_Project.Models
{
    public class ContactDetail : BaseEntity
    {
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
