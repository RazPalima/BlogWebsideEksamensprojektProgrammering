using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace MVCVideoGuide.Models.ViewModels
{
    public class BlogCollectionCategoriesViewModel
    {
        public IEnumerable<Blog> BlogCollection { get; set; } = [];
        [DisplayName("Categories")]
        public IEnumerable<int>? SelectedCategoryIds { get; set; }
        public string BlogAttributeSorting { get; set; } = string.Empty;
        public List<SelectListItem> Categories { get; set; } = [];
    }
}
