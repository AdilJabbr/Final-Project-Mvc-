using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModel.Admin.News
{
    public class NewsVM
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Tag { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;
    }
}
