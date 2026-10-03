using Eshop_RazorPage.Models;
using Eshop_RazorPage.Models.Auth;

namespace Eshop_RazorPage.Services.Auth
{
    public class AuthServices : IAuthServices
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _accessor;
        public AuthServices(HttpClient httpClient, IHttpContextAccessor accessor)
        {
            _httpClient = httpClient;
            _accessor = accessor;
        }

        public async Task<ApiResult<LoginResponse>?> Login(LoginCommand command)
        {
            var result = await _httpClient.PostAsJsonAsync("auth/login", command);
            return await result.Content.ReadFromJsonAsync<ApiResult<LoginResponse>>();
        }

        public async Task<ApiResult?> Logout()
        {
            var result = await _httpClient.DeleteAsync("auth/Logout");
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        }

        public async Task<ApiResult<LoginResponse>?> RefreshToken()
        {
            var refreshToken = _accessor.HttpContext.Request.Cookies["refreshToken"];
            var result = await _httpClient.PostAsync($"auth/RefreshToken?refreahtoken={refreshToken}", null);
            return await result.Content.ReadFromJsonAsync<ApiResult<LoginResponse>>();
        }

        public async Task<ApiResult?> Register(RegisterCommand command)
        {
            var result = await _httpClient.PostAsJsonAsync("auth/Register", command);
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        }
    }
}