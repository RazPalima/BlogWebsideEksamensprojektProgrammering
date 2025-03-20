using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MVCVideoGuide.Data;
using MVCVideoGuide.Models;
using System;
using System.Linq;

namespace MVCVideoGuide.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BlogDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<BlogDbContext>>()))
            {
                // Check if the database has already been seeded.
                if (context.Blogs.Any() || context.Categories.Any() || context.Comments.Any())
                {
                    return; // DB has been seeded
                }

                // Seed categories.
                var categories = new[]
                {
                    new Category { Name = "Technology" },
                    new Category { Name = "Lifestyle" },
                    new Category { Name = "Travel" },
                    new Category { Name = "Education" },
                    new Category { Name = "Food" },
                    new Category { Name = "Entertainment" }
                };
                context.Categories.AddRange(categories);
                context.SaveChanges();

                // Seed blogs.
                var blogs = Enumerable.Range(1, 10).Select(i => new Blog
                {
                    Title = $"Sample Blog {i}",
                    User = $"User{i}",
                    CreatedDate = DateTime.Now.AddDays(-i),
                    Text = $"This is the content of blog {i}. It is meant to provide an example of how seeding works in ASP.NET Core.",
                    LikeCount = i * 10
                }).ToArray();

                context.Blogs.AddRange(blogs);
                context.SaveChanges();

                // Create a single instance of Random.
                var random = new Random();
                // Assign random categories to blogs.
                foreach (var blog in blogs)
                {
                    var selectedCategories = categories
                        .OrderBy(c => random.Next()) // Use Random.Next() to shuffle
                        .Take(3)                    // Select 3 random categories
                        .ToList();

                    foreach (var category in selectedCategories)
                    {
                        blog.BlogCategories.Add(new BlogCategory { Blog = blog, Category = category });
                    }
                }
                context.SaveChanges();

                // Seed comments for each blog.
                foreach (var blog in blogs)
                {
                    var comments = Enumerable.Range(1, 3).Select(j => new Comment
                    {
                        CreatedDate = DateTime.Now.AddDays(-j),
                        User = $"Commenter{j}",
                        Text = $"This is comment {j} for blog '{blog.Title}'.",
                        Blog = blog
                    }).ToList();

                    context.Comments.AddRange(comments);
                }
                context.SaveChanges();
            }
        }
    }
}
