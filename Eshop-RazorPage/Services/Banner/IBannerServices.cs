using Eshop_RazorPage.Models;
using Eshop_RazorPage.Models.Banner;

namespace Eshop_RazorPage.Services.Banner
{
    public interface IBannerServices
    {
        Task<List<BannerDto>> GetAllBanner();
        Task<BannerDto?> GetById(long Id);
        Task<ApiResult> DeleteBanner(long BannerId);

        Task<ApiResult> CreateBanner(CreateBannerCommand command);
        Task<ApiResult> EditBanner(EditBannerCommand command);

    }
}
