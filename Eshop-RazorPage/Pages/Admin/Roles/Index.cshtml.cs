using Eshop_RazorPage.Infrastructure.RazorUtil;
using Eshop_RazorPage.Models;
using Eshop_RazorPage.Models.Role;
using Eshop_RazorPage.Services.Role;
using Eshop_RazorPage.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Eshop_RazorPage.Pages.Admin.Roles
{
    public class IndexModel : BaseRazorPage
    {
        private readonly IRoleServices _service;
        private readonly IRenderViewToString _render;
        public IndexModel(IRoleServices service, IRenderViewToString render)
        {
            _service = service;
            _render = render;
        }
        public List<RoleDto> Roles { get; set; }

        public async Task OnGet()
        {
            Roles = await _service.GetAllRole();
        }


        public async Task<IActionResult> OnGetShowAddPage()
        {
            return await AjaxTryCatch(async () =>
            {
                var view = await _render.RenderToStringAsync("_Add", new AddRoleViewModel(), PageContext);
                return ApiResult<string>.Success(view);
            });
        }
        public async Task<IActionResult> OnGetShowEditPage(long roleId)
        {
            return await AjaxTryCatch(async () =>
            {
                var role = await _service.GetRoleById(roleId);
                if (role == null)
                    return ApiResult<string>.Error();

                var model = new EditRoleViewModel()
                {
                    Permissions = role.Permissions,
                    Title = role.Title,
                    RoleId = role.Id,
                };

                var view = await _render.RenderToStringAsync("_Edit", model, PageContext);
                return ApiResult<string>.Success(view);
            });
        }
        public async Task<IActionResult> OnPostAddRole(List<Permission> permissions, AddRoleViewModel viewModel)
        {
            //var permissionModel = new List<Permission>();
            //foreach (var item in permissions)
            //{
            //    try
            //    {
            //        permissionModel.Add(EnumUtils.ParseEnum<Permission>(item.ToString()));
            //    }
            //    catch
            //    {
            //        // 
            //    }
            //}
            if (permissions.Count<=0)
                ModelState.AddModelError(nameof(viewModel.Title),"نقشی برای این عنوان انتخاب کنید");
           
            return await AjaxTryCatch(async () =>
            {
                var model = new AddRoleCommend()
                {
                    Title = viewModel.Title,
                    Permissions = permissions
                };
                var result = await _service.AddRole(model);
                return result;
            });
        }
        public async Task<IActionResult> OnPostEditRole(EditRoleViewModel viewModel,List<Permission> permissions)
        {
            return await AjaxTryCatch(async () =>
            {
                var model = new EditRoleCommand()
                {
                    Permissions = permissions,
                    RoleId = viewModel.RoleId,
                    Title = viewModel.Title,
                };
                var result = await _service.EditRole(model);
                return result;

            });
        }
        public async Task<IActionResult> OnPostDeleteRole(long roleId)
        {
            return await AjaxTryCatch(() => _service.DeleteRole(roleId));
        }
    }
}
