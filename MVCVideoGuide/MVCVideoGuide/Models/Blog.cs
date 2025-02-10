using System.ComponentModel.DataAnnotations;

namespace MVCVideoGuide.Data.Entities
{
    public class Blog
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required]
        public string User { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        public int LikeCount { get; set; }
        public ICollection<BlogCategory>? BlogCategories { get; set; }
    }
}
