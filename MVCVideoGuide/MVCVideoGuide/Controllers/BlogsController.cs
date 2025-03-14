using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using MVCVideoGuide.Data;
using MVCVideoGuide.Models;
using MVCVideoGuide.Models.ViewModels;
using NuGet.Packaging;

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
        public async Task<IActionResult> Index(int? id)
        {
            IQueryable<Blog> blogs = _context.Blogs
                .Include(b => b.BlogCategories)
                .ThenInclude(bc => bc.Category)
                .AsQueryable();
            if (id != null)
            {
                switch (id.Value)
                {
                    case 1:
                        blogs = blogs.OrderBy(b => b.CreatedDate);
                        break;
                    case 2:
                        blogs = blogs.OrderBy(b => b.User);
                        break;
                    case 3:
                        blogs = blogs.OrderBy(b => b.Title);
                        break;
                    default:
                        break;
                }
            }           
            ViewBag.Categories = await _context.Categories.OrderBy(c => c.Name).ToListAsync();
            return View(await blogs.AsNoTracking().ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Index(int? id, string selectedBlogAttributeSorting, int[] selectedCategoryIds)
        {

            IQueryable<Blog> blogs = _context.Blogs
                .Include(b => b.BlogCategories)
                .ThenInclude(bc => bc.Category)
                .AsQueryable();

            if (selectedCategoryIds != null && selectedCategoryIds.Length > 0)
            {
                // Filter blogs that contain all the selected categories
                blogs = blogs.Where(b => b.BlogCategories.All(bc => selectedCategoryIds.Contains(bc.CategoryId)));
            }

            if (id != null)
            {
                switch (id.Value)
                {
                    case 1:
                        blogs = blogs.OrderBy(b => b.CreatedDate);
                        break;
                    case 2:
                        blogs = blogs.OrderBy(b => b.User);
                        break;
                    case 3:
                        blogs = blogs.OrderBy(b => b.Title);
                        break;
                    default:
                        break;
                }
            }
            // Get all categories to populate the dropdown list
            ViewBag.Categories = await _context.Categories.OrderBy(c => c.Name).ToListAsync();



            return View(await blogs.AsNoTracking().ToListAsync());
        }


        // GET: Blogs/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .Include(b => b.BlogCategories)
                    .ThenInclude(bc => bc.Category)
                .Include(c => c.Comments)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (blog == null)
            {
                return NotFound();
            }

            //var comments = await _context.Comments.Where(c => c.BlogId == id).ToListAsync();

            var blogCommentsVM = new BlogCommentsViewModel
            {
                Blog = blog,
                Comments = blog.Comments.ToList()
            };

            return View(blogCommentsVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(int id, [Bind("NewComment")] BlogCommentsViewModel blogCommentsVM)
        {
            var blog = await _context.Blogs
                .Include(b => b.Comments)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (blog == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Add new comment
                blog.Comments.Add(new Comment
                {
                    CreatedDate = DateTime.Now,
                    User = blogCommentsVM.NewComment.User,
                    Text = blogCommentsVM.NewComment.Text,
                    BlogId = id,
                    Blog = blog
                });
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id });
            }
            foreach (var error in ModelState)
            {
                foreach (var subError in error.Value.Errors)
                {
                    Console.WriteLine($"Error in {error.Key}: {subError.ErrorMessage}");
                }
            }
            // Return ViewModel with existing blog and comments if validation fails
            var viewModel = new BlogCommentsViewModel
            {
                Blog = blog,
                Comments = blog.Comments.ToList(),
                NewComment = blogCommentsVM.NewComment
            };

            return View(viewModel);
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
                    var blogCategoriesToCreate = selectedCategoryIds.Select(categoryId => new BlogCategory { BlogId = blog.Id, CategoryId = categoryId });
                    blog.BlogCategories.AddRange(blogCategoriesToCreate);

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
            ViewBag.Categories = await _context.Categories.OrderBy(c => c.Name).ToListAsync();
            var blog = await _context.Blogs
                .Include(b => b.BlogCategories)
                    .ThenInclude(bc => bc.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            ViewBag.CreatedDate = DateTime.Now;
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
        public async Task<IActionResult> Edit(int id, int[] selectedCategoryIds, [Bind("Id,CreatedDate,User,Title,Text,LikeCount")] Blog blog)
        {
            if (id != blog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Load existing Blog including navigation properties
                var blogToUpdate = await _context.Blogs
                    .Include(b => b.BlogCategories)
                    .FirstOrDefaultAsync(b => b.Id == id);

                if (blogToUpdate == null)
                {
                    return NotFound();
                }

                blogToUpdate.CreatedDate = DateTime.Now;
                blogToUpdate.User = blog.User;
                blogToUpdate.Title = blog.Title;
                blogToUpdate.Text = blog.Text;
                blogToUpdate.LikeCount = blog.LikeCount;

                // Handle BlogCategories update manually
                blogToUpdate.BlogCategories.Clear();
                var blogCategoriesToUpdate = selectedCategoryIds.Select(categoryId => new BlogCategory { BlogId = blog.Id, CategoryId = categoryId });
                blogToUpdate.BlogCategories.AddRange(blogCategoriesToUpdate);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogExists(blog.Id))
                    {
                        return NotFound();
                    }
                    throw;
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
