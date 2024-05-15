using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Blog.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewData["Title"] = "Dashboard";
            return View();
        }
    }
}