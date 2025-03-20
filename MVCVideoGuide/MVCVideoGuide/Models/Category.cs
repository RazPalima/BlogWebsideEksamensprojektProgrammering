using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MVCVideoGuide.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Category
    {
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;
        public ICollection<BlogCategory> BlogCategories { get; } = [];

    }
}
