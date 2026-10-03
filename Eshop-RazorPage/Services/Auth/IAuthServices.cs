using Eshop_RazorPage.Models;
using Eshop_RazorPage.Models.Auth;
namespace Eshop_RazorPage.Services.Auth
{
    public interface IAuthServices
    {
        Task<ApiResult<LoginResponse>?> Login(LoginCommand command);
        Task<ApiResult?> Register(RegisterCommand command);
        Task<ApiResult<LoginResponse>?> RefreshToken();
        Task<ApiResult?> Logout();
    }
}
