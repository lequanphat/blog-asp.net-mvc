using Blog.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly BlogContext _context;
        public UsersController(BlogContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult Index()
        {
            ViewData["Title"] = "Users";
             var users = _context.Users.ToList();
            return View(users);
        }
        [HttpGet("create")]
        public ActionResult Create()
        {
            ViewData["Title"] = "Create New User";
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] CreatePostDto model, IFormFile Image)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Title"] = "Create New User";
                return View(model);
            }
            // var post = new Post
            // {
            //     Title = model.Title,
            //     Content = model.Description
            // };
            //  _context.Posts.Add(post);
            // await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value" + id;
        }
    }
}