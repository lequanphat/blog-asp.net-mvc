using System.Security.Claims;
using Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class PostsController : Controller
    {
        private readonly BlogContext _context;
        public PostsController(BlogContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult Index()
        {
            ViewData["Title"] = "Posts";
            var posts =  _context.Posts.Include(p => p.CreatedBy).OrderByDescending(p => p.CreatedAt).ToList();
            return View(posts);
        }
        [HttpGet("create")]
        public ActionResult Create()
        {
            ViewData["Title"] = "Create New Post";
            var categories = _context.Categories.ToList();
            ViewData["Categories"] = categories;
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreatePostDto model, IFormFile Image)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Title"] = "Create New Post";
                var categories = _context.Categories.ToList();
                ViewData["Categories"] = categories;
                return View(model);
            }
            if (Image != null)
            {
                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", uniqueFileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await Image.CopyToAsync(stream);
                }
                path = "/uploads/" + uniqueFileName;
                var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
                var user = _context.Users.FirstOrDefault(u => u.Email == email);
                var selectedCategory = _context.Categories.Find(model.CategoryId);
                var post = new Post
                {
                    Title = model.Title,
                    Content = model.Description,
                    Image = path,
                    CreatedBy = user,
                    Category = selectedCategory
                };
                _context.Posts.Add(post);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        [HttpGet("{id}")]
        public ActionResult Details(int id)
        {
            ViewData["Title"] = "Posts Details";
            var post =  _context.Posts.Include(p => p.CreatedBy)
            .Include(p => p.Comments)
            .ThenInclude(c => c.CreatedBy)
            .FirstOrDefault(p => p.Id == id);
            if (post != null)
            {
                post.Comments = post.Comments.OrderByDescending(c => c.CreatedAt).ToList();
            }
            return View(post);
        }
    }
}