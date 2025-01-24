namespace MVCVideoGuide.Data.Entities
{
    public class BlogCategory
    {
        public int Id { get; set; }
        public Blog Blog { get; set; }
        public Category Category { get; set; }
    }
}
