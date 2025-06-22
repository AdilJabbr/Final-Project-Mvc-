using Domain.Models.Common;
using Final_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Comment : BaseEntity
    {
        public AppUser User { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public string Text { get; set; } = null!;
        public int? Rating { get; set; }
        public int? ParentId { get; set; }
        public Comment? Parent { get; set; } = null!;
        public List<Comment> Children { get; set; } = [];
    }
}
