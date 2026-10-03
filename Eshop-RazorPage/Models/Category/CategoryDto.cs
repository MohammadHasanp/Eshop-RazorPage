using Eshop_RazorPage.Models;

namespace Eshop_RazorPage.Models.Category
{
    public class CategoryDto : BaseDto
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public SeoData SeoData { get; set; }
        public List<SubCategoryDto> Childs { get; set; }
    }
    public class SubCategoryDto : BaseDto
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public SeoData SeoData { get; set; }
        public long PrantId { get; set; }
        public List<SecondaryChildCategoryDto> Childs { get; set; }
    }
    public class SecondaryChildCategoryDto : BaseDto
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public SeoData SeoData { get; set; }
        public long PrantId { get; set; }
    }
}
