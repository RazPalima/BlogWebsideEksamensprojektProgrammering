using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace MVCVideoGuide.Models
{
    public class Blog
    {
        public int Id { get; set; }
        [Display(Name = "Date written (or last updated)")]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        [Required]
        public string User { get; set; } = string.Empty;
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Text { get; set; } = string.Empty;
        [Display(Name = "Like count")]
        public int LikeCount { get; set; }
        [Display(Name = "Categories")]
        public ICollection<BlogCategory> BlogCategories { get; } = [];
        public ICollection<Comment> Comments { get; } = [];
        public string ShortenedText()
        {
            return Text.Length > 50 ? $"{Text.Substring(0, 50)}...": Text;
        }
        public string ConcatenatedBlogCategories()
        {
            return string.Join(", ", BlogCategories.Select(bc => bc.Category.Name));
        }
    }
}
