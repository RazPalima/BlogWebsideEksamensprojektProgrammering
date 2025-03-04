using System.ComponentModel.DataAnnotations;

namespace MVCVideoGuide.Models
{
    public class Blog
    {
        public int Id { get; set; }
        [Display(Name = "Date written")]
        public DateTime CreatedDate { get; set; }
        [Required]
        public required string User { get; set; }
        [Required]
        public required string Title { get; set; }
        [Required]
        public required string Text { get; set; }
        [Display(Name = "Like count")]
        public int LikeCount { get; set; }
        [Display(Name = "Categories")]
        public ICollection<BlogCategory> BlogCategories { get; set; } = new List<BlogCategory>();
    }
}
