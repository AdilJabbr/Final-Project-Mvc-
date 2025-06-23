using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModel.Admin.Comments
{
    public class CommentCreateVM
    {
        public string Text { get; set; } = null!;
        public int ProductId { get; set; }
    }
}
