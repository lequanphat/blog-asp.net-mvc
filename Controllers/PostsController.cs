using Blog.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
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
            var posts = _context.Posts.ToList();
            return View(posts);
        }
        [HttpGet("create")]
        public ActionResult Create()
        {
            ViewData["Title"] = "Create New Post";
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreatePostDto model, IFormFile Image)
        {
            if(!ModelState.IsValid)
            {
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
            }
            var post = new Post
            {
                Title = model.Title,
                Content = model.Description
            };

             _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value" + id;
        }
    }
}