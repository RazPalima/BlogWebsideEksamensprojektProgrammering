namespace MVCVideoGuide.Data.Entities
{
    public class Blog
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string User { get; set; }
        public string Title { get; set; }
        public string BlogText { get; set; }
        public int LikeCount { get; set; }
        public Comment? Comment { get; set; }
    }
}
