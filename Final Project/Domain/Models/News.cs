using Domain.Models.Common;

namespace Final_Project.Models
{
    public class News : BaseEntity
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string Tag { get; set; } = null!;
    }
}
