using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Blog.Models;

namespace Blog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly BlogContext _context;

        public BlogController(BlogContext context)
        {
            _context = context;
        }
        [HttpGet]
        public string Get()
        {
            return "hello";
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "hello";
        }
    }
}