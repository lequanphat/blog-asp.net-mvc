using System.Security.Claims;
using Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace Blog.Controllers
{
    [Authorize]
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
            var users =  _context.Users.ToList();
            return View(users);
        }
        [HttpGet("create")]
        public ActionResult Create()
        {
            ViewData["Title"] = "Create New User";
            return View();
        }
    }
}