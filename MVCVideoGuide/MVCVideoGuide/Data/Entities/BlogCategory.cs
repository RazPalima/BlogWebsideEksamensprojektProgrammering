namespace MVCVideoGuide.Data.Entities
{
    public class BlogCategory
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public Blog Blogs { get; set; }

        public int CategoryId { get; set; }
        public Category Categories { get; set; }
    }
}
