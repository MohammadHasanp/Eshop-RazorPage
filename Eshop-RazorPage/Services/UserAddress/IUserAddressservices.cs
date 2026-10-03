using Eshop_RazorPage.Models;
using Eshop_RazorPage.Models.UserAddress;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Eshop_RazorPage.Services.UserAddress
{
    public interface IUserAddressservices
    {
        Task<ApiResult> AddUserAddress(AddUserAddressCommand address);
        Task<ApiResult> EditUserAddress(EditUserAddressCommand address);
        Task<ApiResult> DeleteUserAddress(long AddressId);
        Task<ApiResult> ActivateAddress(long addressId);




        Task<List<AddressDto>> GetAllUserAddress();
        Task<AddressDto?> GetUserAddressById(long addressId);
    }
}
