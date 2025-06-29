using Domain.Models.Common;

namespace Final_Project.Models
{
    public class WhyFruitka : BaseEntity
    {
        
        public string Img { get; set; }
        public IEnumerable<string> Title { get; set; }
        public IEnumerable<string> Desc { get; set; }
    }
}
