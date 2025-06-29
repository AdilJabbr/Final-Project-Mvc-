using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModel.Admin.Comments
{
    public class CommentVM
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public int ProductId { get; set; }
        public string Text { get; set; } = null!;
        public DateTime CreatedTime { get; set; } = DateTime.Now;
    }
}
