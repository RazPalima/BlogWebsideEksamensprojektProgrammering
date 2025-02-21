using System.ComponentModel.DataAnnotations;

namespace MVCVideoGuide.Models
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
        [Display(Name = "Like count")]
        public int LikeCount { get; set; }
        [Display(Name = "Categories")]
        public ICollection<BlogCategory> BlogCategories { get; set; }
    }
}
