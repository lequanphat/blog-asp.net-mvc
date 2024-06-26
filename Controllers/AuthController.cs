using System.Security.Claims;
using Blog.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly BlogContext _context;
        public AuthController(BlogContext context)
        {
            _context = context;
        }
        [HttpGet("login")]
        public ActionResult Login()
        {
            if(User.Identity.IsAuthenticated){
                return RedirectToAction("Index", "Home");
            }
            ViewData["Title"] = "Login";
            return View();
        }
        [HttpGet("google-login")]
        public async Task GoogleLogin()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, 
            new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") });
        }

        public async Task<IActionResult> GoogleResponse(){
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = result.Principal.Identities.FirstOrDefault().Claims;
            var avatar = claims.FirstOrDefault(c => c.Type == "picture")?.Value;
            var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var fullname = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if(user == null){
                user = new User{
                    Email = email,
                    Name = fullname,
                    Avatar = avatar
                };
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
        
    }
}