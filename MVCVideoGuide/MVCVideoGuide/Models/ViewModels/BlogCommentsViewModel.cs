namespace MVCVideoGuide.Models.ViewModels
{
    public class BlogCommentsViewModel
    {
        public Blog Blog { get; set; } = new Blog();
        public Comment NewComment { get; set; } = new Comment
        {
            Blog = new Blog()
        };

        public List<Comment> Comments { get; set; } = [];
    }
}
