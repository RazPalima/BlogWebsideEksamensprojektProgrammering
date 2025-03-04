namespace MVCVideoGuide.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public required string User { get; set; }
        public required string Text { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
