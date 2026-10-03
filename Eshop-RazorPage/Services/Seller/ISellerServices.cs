using Eshop_RazorPage.Models;
using Eshop_RazorPage.Models.Seller;

namespace Eshop_RazorPage.Services.Seller
{
    public interface ISellerServices
    {
        Task<ApiResult> CreateSeller(CreateSellerCommand command);
        Task<ApiResult> EditSeller(EditSellerCommand command);


        Task<SellerFilterResult> GetSellerByFilter(SellerFilterParams filterParams);
        Task<SellerDto?> GetSellerById(long sellerId);
        Task<SellerDto?> GetCurrentUserSeller();
    }
}
