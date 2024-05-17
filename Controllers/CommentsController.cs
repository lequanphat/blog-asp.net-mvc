using System.Security.Claims;
using Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class CommentsController : Controller
    {
        private readonly BlogContext _context;
        public CommentsController(BlogContext context)
        {
            _context = context;
        }

        [HttpPost("{postId}")]
        public async Task<IActionResult> Create(int postId, CreateCommentDto model)
        {
            var post = _context.Posts.Find(postId);
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Details", "Posts", new { id = postId });   
            }
            var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            var comment = new Comment
            {
                Message = model.Message,
                Post = post,
                CreatedBy = user
            };
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Posts", new { id = postId });   
        }
    }
}