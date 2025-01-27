using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCVideoGuide.Data;
using MVCVideoGuide.Data.Entities;
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
            List<string> values = _context.Blogs
                                          .Select(p => p.Category)
                                          .Distinct()
                                          .OrderBy(category => category)
                                          .ToList();

            return View(values);
        }
        /*public IActionResult SeeCategories()
        {
            List<Blog> result = _context.Blogs.OrderBy(p => p.Category).ToList();
            //List<Blog> noDupes = result.Distinct().ToList();
            //var noDupes = new HashSet<Blog>(result).ToList();
            //var values = new HashSet<Blog>(result).ToList();
            List<string> values = new List<string>();
            /*foreach (var item in result)
            {
                if (values.Contains(item.Category) == false)
                {
                    values.Add(item.Category);
                }
            }
            return View(values);*/
            



       // }

        [HttpGet]
        public IActionResult WriteBlog()
        {
            return View();

        }
        [HttpPost]
        public IActionResult WriteBlog(Blog blog_instance)
        {
            blog_instance.CreatedDate = DateTime.Now;
            blog_instance.LikeCount = 0;
            _context.Blogs.Add(blog_instance);
            _context.SaveChanges();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
