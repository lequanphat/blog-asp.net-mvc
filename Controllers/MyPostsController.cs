using System.Security.Claims;
using Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class MyPostsController : Controller
    {
        private readonly BlogContext _context;
        public MyPostsController(BlogContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult Index()
        {
            ViewData["Title"] = "Posts";
            var posts =  _context.Posts.Include(p => p.CreatedBy).OrderByDescending(p => p.CreatedAt).Where(p => p.CreatedBy.Email == User.FindFirst(ClaimTypes.Email).Value).ToList();
            return View(posts);
        }
    }
}