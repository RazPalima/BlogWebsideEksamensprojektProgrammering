using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace MVCVideoGuide.Models
{
    public class Blog
    {
        public int Id { get; set; }
        [Display(Name = "Date written (or last updated)")]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        [Required]
        public string User { get; set; } = "none";
        [Required]
        public string Title { get; set; } = "none";
        [Required]
        public string Text { get; set; } = "none";
        [Display(Name = "Like count")]
        public int LikeCount { get; set; }
        [Display(Name = "Categories")]
        public ICollection<BlogCategory> BlogCategories { get; } = [];
        public ICollection<Comment> Comments { get; } = [];
    }
}
