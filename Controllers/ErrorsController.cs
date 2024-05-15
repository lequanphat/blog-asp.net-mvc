using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Route("[controller]")]
    public class ErrorsController : Controller
    {
        [Route("Errors/{statusCode}")]
        public IActionResult Show(int statusCode)
        {
            return statusCode switch
            {
                404 => View("404"),
                _ => View("Error"),
            };
        }
    }
}