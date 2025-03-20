using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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
        public async Task<IActionResult> Index(BlogCollectionCategoriesViewModel blogCollectionCategoriesVM)
        {
            
            IQueryable<Blog> blogs = _context.Blogs
                .Include(b => b.BlogCategories)
                .ThenInclude(bc => bc.Category)
                .AsQueryable();

            
            if (blogCollectionCategoriesVM.SelectedCategoryIds != null && blogCollectionCategoriesVM.SelectedCategoryIds.Any())
            {
                blogs = blogs.Where(b => b.BlogCategories.All(bc => blogCollectionCategoriesVM.SelectedCategoryIds.Contains(bc.CategoryId)));
            }

            
            if (!string.IsNullOrEmpty(blogCollectionCategoriesVM.BlogAttributeSorting))
            {
                switch (blogCollectionCategoriesVM.BlogAttributeSorting)
                {
                    case "CreatedDate":
                        blogs = blogs.OrderBy(b => b.CreatedDate);
                        break;
                    case "User":
                        blogs = blogs.OrderBy(b => b.User);
                        break;
                    case "Title":
                        blogs = blogs.OrderBy(b => b.Title);
                        break;
                    case "LikeCount":
                        blogs = blogs.OrderBy(b => b.LikeCount);
                        break;
                    default:
                        break;
                }
            }
            
            blogCollectionCategoriesVM.BlogCollection = await blogs.ToListAsync();

            
            
            blogCollectionCategoriesVM.Categories = await _context.Categories
                .OrderBy(c => c.Name)
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToListAsync();

            return View(blogCollectionCategoriesVM);
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
                .Include(c => c.Comments)
                .Include(b => b.BlogCategories)
                    .ThenInclude(bc => bc.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (blog == null)
            {
                return NotFound();
            }

            var blogCommentsVM = new BlogCommentsViewModel
            {
                Blog = blog,
                Comments = blog.Comments.ToList()
            };

            return View(blogCommentsVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(int id, BlogCommentsViewModel blogCommentsVM)
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

            var blogCategoriesVM = new BlogCategoriesViewModel
            {
                Categories = await _context.Categories
                    .OrderBy(c => c.Name)
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    })
                    .ToListAsync()
            };
            return View(blogCategoriesVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogCategoriesViewModel blogCategoriesVM)
        {
            if (ModelState.IsValid)
            {
                if (blogCategoriesVM.SelectedCategoryIds != null && blogCategoriesVM.SelectedCategoryIds.Any())
                {
                    var blogCategoriesToCreate = blogCategoriesVM.SelectedCategoryIds.Select(categoryId => new BlogCategory
                    {
                        BlogId = blogCategoriesVM.Blog.Id, CategoryId = categoryId
                    });
                    blogCategoriesVM.Blog.BlogCategories.AddRange(blogCategoriesToCreate);

                    blogCategoriesVM.Blog.CreatedDate = DateTime.Now;
                    _context.Blogs.Add(blogCategoriesVM.Blog);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
            }
            
            blogCategoriesVM.Categories = await _context.Categories
                .OrderBy(c => c.Name)
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name,
                    Selected = blogCategoriesVM.SelectedCategoryIds!.Contains(c.Id)
                })
                .ToListAsync();
            return View(blogCategoriesVM);
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
                var blog = await _context.Blogs
                    .Include(b => b.BlogCategories)
                        .ThenInclude(bc => bc.Category)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (blog == null)
                {
                    return NotFound();
                }

            var allCategories = await _context.Categories
                .OrderBy(c => c.Name)
                .ToListAsync();

           
            var blogCategoriesVM = new BlogCategoriesViewModel
            {
                Blog = blog,
                Categories = allCategories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name,
                    Selected = blog.BlogCategories.Any(bc => bc.CategoryId == c.Id) 
                }).ToList()
            };
            return View(blogCategoriesVM);
            }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BlogCategoriesViewModel blogCategoriesVM)
        {
            if (id != blogCategoriesVM.Blog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                var blogToUpdate = await _context.Blogs
                    .Include(b => b.BlogCategories)
                    .FirstOrDefaultAsync(b => b.Id == id);

                if (blogToUpdate == null)
                {
                    return NotFound();
                }

                blogToUpdate.CreatedDate = DateTime.Now;
                blogToUpdate.User = blogCategoriesVM.Blog.User;
                blogToUpdate.Title = blogCategoriesVM.Blog.Title;
                blogToUpdate.Text = blogCategoriesVM.Blog.Text;
                blogToUpdate.LikeCount = blogCategoriesVM.Blog.LikeCount;

                blogToUpdate.BlogCategories.Clear();
                var blogCategoriesToUpdate = blogCategoriesVM.SelectedCategoryIds!.Select(categoryId => new BlogCategory
                {
                    BlogId = blogCategoriesVM.Blog.Id, CategoryId = categoryId
                });
                blogToUpdate.BlogCategories.AddRange(blogCategoriesToUpdate);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogExists(blogCategoriesVM.Blog.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }
            blogCategoriesVM.Categories = await _context.Categories
                .OrderBy(c => c.Name)
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
                .ToListAsync();
            return View(blogCategoriesVM);
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
