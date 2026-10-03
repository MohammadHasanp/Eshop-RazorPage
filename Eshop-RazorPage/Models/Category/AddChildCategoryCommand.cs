using Eshop_RazorPage.Models;

namespace Eshop_RazorPage.Models.Category
{
    public class AddChildCategoryCommand
    {
        public long parentId { get; set; }
        public string title { get; set; }
        public string slug { get; set; }
        public SeoData seoData { get; set; }
    }
}
