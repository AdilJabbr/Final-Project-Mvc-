using Domain.Models.Common;

namespace Final_Project.Models
{
    public class Abt : BaseEntity
    {
       
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public string VideoLink { get; set; }
    }
}
