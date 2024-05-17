using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using Blog.Models;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
}).AddCookie().AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
{
    options.ClientId = builder.Configuration.GetSection("GoogleKeys:ClientId").Value;
    options.ClientSecret = builder.Configuration.GetSection("GoogleKeys:ClientSecret").Value;
    options.Scope.Add("profile");
    options.Events.OnCreatingTicket = (context) =>
    {                      
        var picture = context.User.GetProperty("picture").GetString();
        context.Identity.AddClaim(new Claim("picture", picture));
        return Task.CompletedTask;
    };
});

builder.Services.AddRazorPages();
builder.Services.AddDbContext<BlogContext>(opt =>
    opt.UseMySql("Server=localhost;Database=blog_app;User=root;Password=;", 
        new MySqlServerVersion(new Version(8, 0, 21))));


builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}");

app.Run();
