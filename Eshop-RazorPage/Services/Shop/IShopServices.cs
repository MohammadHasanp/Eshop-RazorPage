using Eshop_RazorPage.Models;
using Eshop_RazorPage.Models.Banner;
using Eshop_RazorPage.Models.Product;
using Eshop_RazorPage.Models.Slider;

namespace Eshop_RazorPage.Services.Shop
{
    public interface IShopServices
    {
        Task<MainPageDto> GetMainPageData();
    }

    public class ShopServices : IShopServices
    {
        private readonly HttpClient _httpClient;

        public ShopServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<MainPageDto> GetMainPageData()
        {
            var result = await _httpClient.GetFromJsonAsync<ApiResult<MainPageDto>>("Shop");
            return result.Data;
        }
    }

    public class MainPageDto
    {
        public List<BannerDto> Banners { get; set; }
        public List<SliderDto> Slider { get; set; }
        public List<ProductShopDto> SpecialProduct { get; set; } = new();
        public List<ProductShopDto> LatesProduct { get; set; } = new();
        public List<ProductShopDto> BestSellersProduct { get; set; } = new();

    }
}
