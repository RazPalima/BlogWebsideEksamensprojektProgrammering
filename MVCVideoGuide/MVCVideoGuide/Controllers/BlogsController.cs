using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCVideoGuide.Data;
using MVCVideoGuide.Models;

namespace MVCVideoGuide.Controllers
{
    public class BlogsController : Controller
    {
        private readonly BlogDbContext _context;

        public BlogsController(BlogDbContext context)
        {
            _context = context;
        }


        // GET: Blogs
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Blog> categories = await _context.Blogs.Include(b => b.BlogCategories)
                .ThenInclude(bc => bc.Category)
                .ToListAsync();
            ViewBag.Categories = await _context.Categories.OrderBy(c => c.Name).ToListAsync();
            return View(categories);
        }
        [HttpPost]
        public async Task<IActionResult> Index(int[] selectedCategoryIds)
        {
            // Get all categories to populate the dropdown list
            ViewBag.Categories = await _context.Categories.OrderBy(c => c.Name).ToListAsync();

            // Fetch all blogs with their categories
            var query = _context.Blogs.Include(b => b.BlogCategories)
                .ThenInclude(bc => bc.Category)
                .AsQueryable();

            if (selectedCategoryIds != null && selectedCategoryIds.Length > 0)
            {
                // Filter blogs that contain all the selected categories
                query = query.Where(b => b.BlogCategories.All(bc => selectedCategoryIds.Contains(bc.CategoryId)));
            }

            // Execute the query and get the filtered blogs
            var blogs = await query.ToListAsync();

            return View(blogs);
        }


        // GET: Blogs/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var blog = await _context.Blogs
            //    .FirstOrDefaultAsync(m => m.Id == id);

            var blog = await _context.Blogs
            .Include(b => b.BlogCategories) // Include BlogCategories
            .ThenInclude(bc => bc.Category) // Include Category inside BlogCategories
            .FirstOrDefaultAsync(m => m.Id == id);

            if (blog == null)
            {
                return NotFound();
            }
            List<Comment> comments = await _context.Comments.ToListAsync();
            ViewBag.Comments = comments;
            return View(blog);
        }

        [HttpPost]
        public async Task<IActionResult> Details(int? id, [Bind("User,Text")] Comment comment)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                comment.CreatedDate = DateTime.Now;
                comment.BlogId = id.Value;
                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Details));
            }

            //var blog = await _context.Blogs
            //    .FirstOrDefaultAsync(m => m.Id == id);

            var blog = await _context.Blogs
            .Include(b => b.BlogCategories) // Include BlogCategories
            .ThenInclude(bc => bc.Category) // Include Category inside BlogCategories
            .FirstOrDefaultAsync(m => m.Id == id);

            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // GET: Blogs/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            List<Category> categories = await _context.Categories.OrderBy(c => c.Name).ToListAsync();
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("User,Title,Text,LikeCount")] Blog blog, int[] selectedCategoryIds)
        {
            if (ModelState.IsValid)
            {
                if (selectedCategoryIds != null)
                {
                    // ✅ Ensure BlogCategories is initialized properly
                    blog.BlogCategories = new List<BlogCategory>();
                    foreach (int categoryId in selectedCategoryIds)
                    {
                        blog.BlogCategories.Add(new BlogCategory { BlogId = blog.Id, CategoryId = categoryId });
                    }

                    blog.CreatedDate = DateTime.Now;
                    _context.Blogs.Add(blog);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
            }
            List<Category> categories = await _context.Categories.OrderBy(c => c.Name).ToListAsync();
            ViewBag.Categories = categories;
            return View();
        }



        // POST: Blogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        // GET: Blogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CreatedDate,User,Title,Text,BlogCategories,LikeCount")] Blog blog)
        {
            if (id != blog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogExists(blog.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }

        // GET: Blogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
              .FirstOrDefaultAsync(m => m.Id == id);


            

            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog != null)
            {
                _context.Blogs.Remove(blog);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogExists(int id)
        {
            return _context.Blogs.Any(e => e.Id == id);
        }
    }
}
