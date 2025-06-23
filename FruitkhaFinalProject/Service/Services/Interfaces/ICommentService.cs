using Service.ViewModel.Admin.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ICommentService
    {
        Task<bool> DeleteCommentAsync(int commentId, int productId);
        Task<CommentVM> AddCommentAsync(CommentCreateVM dto, string userId);
    }
}
