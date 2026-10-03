using Eshop_RazorPage.Models;
using Eshop_RazorPage.Models.Role;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Eshop_RazorPage.Services.Role
{
    public interface IRoleServices
    {
        Task<ApiResult> AddRole(AddRoleCommend commend);
        Task<ApiResult> EditRole(EditRoleCommand command);
        Task<ApiResult> DeleteRole(long RoleId);

        Task<List<RoleDto?>> GetAllRole();
        Task<RoleDto?> GetRoleById(long roleId);
    }
}
