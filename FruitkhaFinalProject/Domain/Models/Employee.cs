using Domain.Models;
using Domain.Models.Common;

namespace Final_Project.Models
{
    public class Employee : BaseEntity
    {
        public string Image { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }

    }
}
