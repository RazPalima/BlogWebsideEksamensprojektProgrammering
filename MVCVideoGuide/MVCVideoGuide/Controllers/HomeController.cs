using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCVideoGuide.Data;
using MVCVideoGuide.Models;

namespace MVCVideoGuide.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BlogDbContext _context;

        public HomeController(ILogger<HomeController> logger, BlogDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        // Video 4 timestamp 26:31. Den helt nye Persons database er ikke blevet introduceret
        // i videoen.
        //public IActionResult Index2(Persons p)
        //{
            
        //    return View();
        //}

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult SeeBlogs()
        {
            List<Blog> result = _context.Blogs.OrderBy(p => p.Title).ToList(); 
            return View(result);

        }
        public IActionResult SeeBlogsDate()
        {
            List<Blog> result = _context.Blogs.OrderBy(p => p.CreatedDate).ToList(); 
            return View(result);

        }
        public IActionResult SeeCategories()
        {
            // Get distinct categories using LINQ



            List<string> values = _context.Categories.Select(c =>c.Name).ToList();
            return View(values);
        }


        [HttpGet]
        public IActionResult WriteBlog()
        {
            var categories = _context.Categories.OrderBy(c => c.Name).ToList();
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        public IActionResult WriteBlog(Blog blog, string[] selectedCategoryIds)
        {
            if (ModelState.IsValid)
            {
                if (selectedCategoryIds != null)
                {
                    blog.BlogCategories = new List<BlogCategory>();
                    foreach (string categoryId in selectedCategoryIds)
                    {
                        BlogCategory CategoryToAdd = new BlogCategory { BlogId = blog.Id, CategoryId = int.Parse(categoryId) };
                        blog.BlogCategories.Add(CategoryToAdd);
                    }
                }
                blog.CreatedDate = DateTime.Now;
                _context.Blogs.Add(blog);
                _context.SaveChanges();


                _context.SaveChanges();
                return RedirectToAction(nameof(SeeBlogs));
            }

            return View(blog);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
