using System.Security.Claims;
using Blog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public class MenuViewComponent : ViewComponent
{
 
    private readonly BlogContext _context;
    public MenuViewComponent(BlogContext context){
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
        var user = _context.Users.FirstOrDefault(u => u.Email == email);
        return View(user);
    }
}