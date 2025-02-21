using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MVCVideoGuide.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<BlogCategory>? BlogCategories { get; set; }

    }
}
