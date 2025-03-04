namespace MVCVideoGuide.Models.ViewModels
{
    public class BlogCommentsViewModel
    {
        public Blog Blog { get; set; }
        public Comment NewComment { get; set; } = new Comment();
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
