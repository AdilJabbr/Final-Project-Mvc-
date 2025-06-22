using Domain.Models.Common;

namespace Final_Project.Models
{
    public class News : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string User { get; set; }
        public string UserRole { get; set; }
        public string Image { get; set; }

    }
}
