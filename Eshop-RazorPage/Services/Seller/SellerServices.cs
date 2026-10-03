using Eshop_RazorPage.Infrastructure;
using Eshop_RazorPage.Models;
using Eshop_RazorPage.Models.Seller;
using Microsoft.AspNetCore.Mvc;

namespace Eshop_RazorPage.Services.Seller
{
    public class SellerServices: ISellerServices
    {
        private readonly HttpClient _client;
        public SellerServices(HttpClient client)
        {
            _client = client;
        }

        public async Task<ApiResult> CreateSeller(CreateSellerCommand command)
        {
            var result = await _client.PostAsJsonAsync("Seller",command);
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        }

        public async Task<ApiResult> EditSeller(EditSellerCommand command)
        {
            var result = await _client.PutAsJsonAsync("Seller",command);
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        }

        public async Task<SellerDto?> GetCurrentUserSeller()
        {
            var result = await _client.GetFromJsonAsync<ApiResult<SellerDto>>("Seller/Current");
            return result?.Data;
        }

        public async Task<SellerFilterResult?> GetSellerByFilter(SellerFilterParams filterParams)
        {
            var url = filterParams.GenerateBaseFilterUrl("Seller") +
                  $"&NationalCode={filterParams.NationalCode}&ShopName={filterParams.ShopName}";

            var result = await _client.GetFromJsonAsync<ApiResult<SellerFilterResult>>(url);
            return result?.Data;
        }

        public async Task<SellerDto?> GetSellerById(long sellerId)
        {
            var result = await _client.GetFromJsonAsync<ApiResult<SellerDto>>($"Seller/{sellerId}");
            return result?.Data;
        }
    }
}
