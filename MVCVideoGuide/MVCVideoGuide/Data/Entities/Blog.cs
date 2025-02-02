using System.ComponentModel.DataAnnotations;

namespace MVCVideoGuide.Data.Entities
{
    public class Blog
    {
        public int Id { get; set; }
        //[DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        public string User { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int LikeCount { get; set; }
        public ICollection<BlogCategory>? Categories { get; set; }
    }
}
