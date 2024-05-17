using Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class CategoriesController : Controller
    {
        private readonly BlogContext _context;
        public CategoriesController(BlogContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult Index()
        {
            ViewData["Title"] = "Categories";
            var categories = _context.Categories.ToList();
            return View(categories);
        }
        [HttpGet("create")]
        public ActionResult Create()
        {
            ViewData["Title"] = "Create New Category";
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] CreateCategoryDto model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Title"] = "Create New Category";
                return View(model);
            }
            var category = new Category
            {
                Name = model.Name
            };
             _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet("update/{id}")]
        public ActionResult Update(int id)
        {
            ViewData["Title"] = "Update Category";
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost("update/{id}")]
        public async Task<IActionResult> Update(int id, CreateCategoryDto model)
        {
            var category = _context.Categories.Find(id);
            if(category == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                ViewData["Title"] = "Update Category";
                return View(category);
            }
            try
            {
                category.Name = model.Name;
                _context.Categories.Update(category);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            ViewData["Title"] = "Update Category";
            ViewData["success"] = "Category updated successfully";
            return View(category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
    

    
}