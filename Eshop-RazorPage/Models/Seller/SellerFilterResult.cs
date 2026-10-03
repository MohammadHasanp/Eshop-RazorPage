using Eshop_RazorPage.Models;

namespace Eshop_RazorPage.Models.Seller
{
    public class SellerFilterResult:BaseFilter<SellerDto,SellerFilterParams>
    {
    }
    public class SellerFilterParams:BaseFilterParam
    {
        public string? ShopName { get; set; }
        public string? NationalCode { get; set; }
    }
    public class SellerDto:BaseDto
    {
        public int userId { get; set; }
        public string shopName { get; set; }
        public string nationalCode { get; set; }
        public int status { get; set; }
    }
}
