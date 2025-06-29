using Domain.Models;
using Domain.Models.Common;

namespace Final_Project.Models
{
    public class Employee : BaseEntity
    {
        public string ImageUrl { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Position { get; set; } = null!;
    }
}
