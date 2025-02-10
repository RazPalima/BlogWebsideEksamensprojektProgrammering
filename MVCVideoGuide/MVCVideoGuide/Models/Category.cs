using System.ComponentModel.DataAnnotations;

namespace MVCVideoGuide.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<BlogCategory>? BlogCategories { get; set; }

    }
}
