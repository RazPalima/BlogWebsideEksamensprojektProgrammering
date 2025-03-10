namespace MVCVideoGuide.Models
{
    public class BlogCategory
    {
        public int BlogId { get; set; }
        public int CategoryId { get; set; }
        public Blog Blog { get; set; } = null!;
        public Category Category { get; set; } = null!;
    }
}
