using Microsoft.EntityFrameworkCore;
using Blog.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddDbContext<BlogContext>(opt =>
    opt.UseMySql("Server=localhost;Database=blog-app;User=root;Password=;", 
        new MySqlServerVersion(new Version(8, 0, 21))));

builder.Services.AddControllers();
var app = builder.Build();



app.UseRouting();

app.MapControllers();
app.UseStaticFiles();

app.Run();
