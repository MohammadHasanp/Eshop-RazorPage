using Eshop_RazorPage.Models;
using Eshop_RazorPage.Models.UserAddress;
using Microsoft.AspNetCore.Mvc;

namespace Eshop_RazorPage.Services.UserAddress
{
    public class UserAddressServices: IUserAddressservices
    {
        private readonly HttpClient _client;
        public UserAddressServices(HttpClient client)
        {
            _client = client;
        }

        public async Task<ApiResult> ActivateAddress(long addressId)
        {
            var result = await _client.PutAsync($"UserAddress/ActivateById/{addressId}",null);
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        }

        public async Task<ApiResult> AddUserAddress(AddUserAddressCommand address)
        {
            var result = await _client.PostAsJsonAsync("UserAddress",address);
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        }

        public async Task<ApiResult> DeleteUserAddress(long AddressId)
        {
            var result = await _client.DeleteAsync($"UserAddress/{AddressId}");
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        }

        public async Task<ApiResult> EditUserAddress(EditUserAddressCommand address)
        {
            var result = await _client.PutAsJsonAsync("UserAddress", address);
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        }

        public async Task<List<AddressDto>> GetAllUserAddress()
        {
            var result = await _client.GetFromJsonAsync<ApiResult<List<AddressDto>>>("UserAddress");
            return  result?.Data;
        }

        public async Task<AddressDto?> GetUserAddressById(long addressId)
        {
            var result = await _client.GetFromJsonAsync<ApiResult<AddressDto>>($"UserAddress/GetAddressBy{addressId}");
            return result?.Data;
        }
    }
}
