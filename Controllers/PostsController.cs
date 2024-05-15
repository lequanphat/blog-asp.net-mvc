using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Blog.Controllers
{
    [Route("[controller]")]
    public class PostsController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewData["Title"] = "Posts";
            return View();
        }
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value" + id;
        }
    }
}