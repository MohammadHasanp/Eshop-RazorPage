using Eshop_RazorPage.Models;
using Eshop_RazorPage.Models.ShippingMethod;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;

namespace Eshop_RazorPage.Services.ShippingMethods
{
    public interface IShippingMethodService
    {
        Task<ApiResult> CreateShippingMethod(CreateShippingMethodCommand command);
        Task<ApiResult> EditShippingMethod(EditShippingMethodCommand command);
        Task<ApiResult> DeleteShippingMethod(long Id);


        Task<List<ShippingMethodDto>> GetAllShippingMethod();
        Task<ShippingMethodDto> GetShippingMethodeByID(long Id);
    }


    public class ShippingMethodeService : IShippingMethodService
    {
        private readonly HttpClient _client;
        public ShippingMethodeService(HttpClient client)
        {
            _client = client;
        }

        public async Task<ApiResult> CreateShippingMethod(CreateShippingMethodCommand command)
        {
            var result = await _client.PostAsJsonAsync("ShippingMethod", command);
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        }

        public async Task<ApiResult> DeleteShippingMethod(long Id)
        {
            var result = await _client.DeleteAsync($"ShippingMethod/{Id}");
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        }

        public async Task<ApiResult> EditShippingMethod(EditShippingMethodCommand command)
        {
            var result = await _client.PutAsJsonAsync("ShippingMethod",command);
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        }

        public async Task<List<ShippingMethodDto>> GetAllShippingMethod()
        {
            var result = await _client.GetFromJsonAsync<ApiResult<List<ShippingMethodDto>>>("ShippingMethod");
            return result.Data;
        }

        public async Task<ShippingMethodDto> GetShippingMethodeByID(long Id)
        {
            var result = await _client.GetFromJsonAsync<ApiResult<ShippingMethodDto>>($"ShippingMethod/{Id}");
            return result.Data;
        }
    }
}
