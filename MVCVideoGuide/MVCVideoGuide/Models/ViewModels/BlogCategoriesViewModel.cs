﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace MVCVideoGuide.Models.ViewModels
{
    public class BlogCategoriesViewModel
    {
        public Blog Blog { get; set; } = new Blog();
        [DisplayName("Categories")]
        public IEnumerable<int>? SelectedCategoryIds { get; set; }
        public List<SelectListItem> Categories { get; set; } = [];
    }
}
