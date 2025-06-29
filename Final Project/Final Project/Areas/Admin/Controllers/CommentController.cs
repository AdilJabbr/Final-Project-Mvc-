using Final_Project.Helpers.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;
using Service.ViewModel.Admin.Comments;
using System.Security.Claims;

namespace Final_Project.Areas.Admin.Controllers
{
    public class CommentController : BaseController
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
           
        }
    
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Delete(int commentUserId, int productUserId)
        {
            await _commentService.DeleteCommentAsync(commentUserId, productUserId);

            string returnUrl = Request.GetReturnUrl();
            return Redirect(returnUrl);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CommentCreateVM dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { success = false, message = "Unauthorized" });

            var newDto = await _commentService.AddCommentAsync(dto, userId);

            return PartialView("_CommentPartial", newDto);
        }
    }
}
