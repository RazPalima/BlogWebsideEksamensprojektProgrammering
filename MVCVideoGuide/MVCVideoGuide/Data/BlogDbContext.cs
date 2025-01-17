using Microsoft.EntityFrameworkCore;
using MVCVideoGuide.Data.Entities;

namespace MVCVideoGuide.Data
{
    public class BlogDbContext: DbContext
    {
        private readonly IConfiguration _config;

        public BlogDbContext(IConfiguration config)
        {
            _config = config;
        }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_config.GetConnectionString("BlogDbConnection"));
        }

    }
}
