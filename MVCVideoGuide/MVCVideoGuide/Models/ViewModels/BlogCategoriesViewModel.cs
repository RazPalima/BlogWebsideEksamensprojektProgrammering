using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCVideoGuide.Models.ViewModels
{
    public class BlogCategoriesViewModel
    {
        public List<Blog>? Movies { get; set; }
        public SelectList? Genres { get; set; }
        public string? MovieGenre { get; set; }
        public string? SearchString { get; set; }
    }
}
