using Eshop_RazorPage.Models;
using Eshop_RazorPage.Models.Role;

namespace Eshop_RazorPage.Services.Role
{
    public class RoleServices: IRoleServices
    {
        private readonly HttpClient _client;
        public RoleServices(HttpClient client)
        {
            _client = client;
        }

        public async Task<ApiResult> AddRole(AddRoleCommend commend)
        {
            var result = await _client.PostAsJsonAsync("Role",commend);
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        }

        public async Task<ApiResult> DeleteRole(long RoleId)
        {
            var result = await _client.DeleteAsync($"Role/{RoleId}");
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        }

        public async Task<ApiResult> EditRole(EditRoleCommand command)
        {
            var result = await _client.PutAsJsonAsync("Role", command);
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        }

        public async Task<List<RoleDto?>> GetAllRole()
        {
            var result = await _client.GetFromJsonAsync<ApiResult<List<RoleDto>>>("Role");
            return result?.Data;
        }

        public async Task<RoleDto?> GetRoleById(long roleId)
        {
            var result = await _client.GetFromJsonAsync<ApiResult<RoleDto>>($"Role/{roleId}");
            return result?.Data;
        }
    }
}
