using Eshop_RazorPage.Models;

namespace Eshop_RazorPage.Models.Product
{
    public class ProductFilterResult:BaseFilter<ProductFilterData,ProductFilterParams>
    {
    }
    public class ProductFilterParams : BaseFilterParam
    {
        public long? Id { get; set; }
        public string? Title { get; set; }
        public string? Slug { get; set; }
    }
    public class ProductFilterData:BaseDto
    {
        public string Slug { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
    }
}
