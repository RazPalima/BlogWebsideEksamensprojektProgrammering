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
        private readonly VideoDbContext _context;

        public HomeController(ILogger<HomeController> logger, VideoDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        // Video 4 timestap 26:31. Den helt nye Persons database er ikke blevet introduceret
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
        public IActionResult ShowProducts()
        {
            List<Product> result = _context.Products.OrderBy(p => p.Cost).ToList();
            return View(result);

        }
        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();

        }
        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            _context.Products.Add(product);
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
