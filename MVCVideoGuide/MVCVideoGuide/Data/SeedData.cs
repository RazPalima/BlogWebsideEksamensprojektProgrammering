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

                if (context.Blogs.Any() || context.Categories.Any() || context.Comments.Any())
                {
                    return;
                }


                var categories = new[]
                {
                    new Category { Name = "Entertainment" },
                    new Category { Name = "Lifestyle" },
                    new Category { Name = "Personal" },
                    new Category { Name = "Rant" },
                };
                context.Categories.AddRange(categories);
                context.SaveChanges();

                var random = new Random();
                Blog[] blogs = [
                    new Blog
                    {
                        CreatedDate = DateTime.Now.AddDays(-random.Next(10,100)),
                        User = "BestUser",
                        Title = "FunnyBlogStory",
                        Text = $"I couldn't really be bothered to write an entire story, but here's the catch...",
                        LikeCount = random.Next(1,100)
                    },
                    new Blog
                    {
                        CreatedDate = DateTime.Now.AddDays(-random.Next(10, 100)),
                        User = "TheHiddenOne",
                        Title = "NovemberRains",
                        Text = "It's a sad day really, it's not yet November but it still rains.",
                        LikeCount = random.Next(1, 100)
                    },
                    new Blog
                    {
                        CreatedDate = DateTime.Now.AddDays(-random.Next(10, 100)),
                        User = "APIRebel",
                        Title = "Rebel Against robots",
                        Text = "I wish this was seeded automatically, but it must be okay with 10 blogs",
                        LikeCount = random.Next(1, 100)
                    },
                    new Blog
                    {
                        CreatedDate = DateTime.Now.AddDays(-random.Next(10, 100)),
                        User = "NocturnalLion",
                        Title = "Sleep deprivation",
                        Text = "What's your favourite late night activity you just cannot live without?",
                        LikeCount = random.Next(1, 100)
                    },
                    new Blog
                    {
                        CreatedDate = DateTime.Now.AddDays(-random.Next(10, 100)),
                        User = "YoungKing",
                        Title = "This will be my villain arc",
                        Text = "I cannot take it any longer, my posts are not getting any likes. This is unacceptable, I'm the king here",
                        LikeCount = random.Next(1, 100)
                    },
                    new Blog
                    {
                        CreatedDate = DateTime.Now.AddDays(-random.Next(10,100)),
                        User = "WesternYapper",
                        Title = "Podcasting is the new thing",
                        Text = "No one will believe someone posting on the internet, but if they post a podcast... then maybe still not.",
                        LikeCount = random.Next(1,100)
                    },
                    new Blog
                    {
                        CreatedDate = DateTime.Now.AddDays(-random.Next(10, 100)),
                        User = "Overhyped",
                        Title = "Live the life and talk the talk",
                        Text = "It can't all be true. Some talk the talk but won't walk the walk. It's not your life, buddy.",
                        LikeCount = random.Next(1, 100)
                    },
                    new Blog
                    {
                        CreatedDate = DateTime.Now.AddDays(-random.Next(10, 100)),
                        User = "DestinationViral",
                        Title = "If you ever get sick",
                        Text = "Pain is sickness leaving the body, but sickness is the new viral trend. Go fast and break things.",
                        LikeCount = random.Next(1, 100)
                    },
                    new Blog
                    {
                        CreatedDate = DateTime.Now.AddDays(-random.Next(10, 100)),
                        User = "FastestNinja",
                        Title = "This is not the end",
                        Text = "If you get through the tough times, you will be stronger than before",
                        LikeCount = random.Next(1, 100)
                    },
                    new Blog
                    {
                        CreatedDate = DateTime.Now.AddDays(-random.Next(10, 100)),
                        User = "CrustyCrisps",
                        Title = "Silicon Chip flavours",
                        Text = "Have you ever had a craving that wasn't quite human? Maybe for like a machine?",
                        LikeCount = random.Next(1, 100)
                    }
                ];




                context.Blogs.AddRange(blogs);
                context.SaveChanges();
                
                foreach (var blog in blogs)
                {
                    var selectedCategories = categories
                        .OrderBy(c => random.Next())
                        .Take(random.Next(1, 3))
                        .ToList();

                    foreach (var category in selectedCategories)
                    {
                        blog.BlogCategories.Add(new BlogCategory { BlogId = blog.Id, CategoryId = category.Id });
                    }
                }
                context.SaveChanges();

                
                foreach (var blog in blogs)
                {
                    var comments = new List<Comment>();
                    for (int j = 0; j < 3; j++)
                    {
                        comments.Add(new Comment
                        {
                            CreatedDate = DateTime.Now.AddDays(-random.Next(1, 10)),
                            User = $"Commenter{random.Next(100,1000)}",
                            Text = $"This is comment {j} for blog '{blog.Title}'.",
                            Blog = blog
                        });
                    }

                    context.Comments.AddRange(comments);
                }
                context.SaveChanges();
            }
        }
    }
}
