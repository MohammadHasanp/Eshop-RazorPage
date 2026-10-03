using Eshop_RazorPage.Infrastructure.RazorUtil;
using Eshop_RazorPage.Models;
using Eshop_RazorPage.Models.Banner;
using Eshop_RazorPage.Services.Banner;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Eshop_RazorPage.Pages.Admin.Banners
{
    public class IndexModel : BaseRazorPage
    {
        private readonly IBannerServices _service;
        private readonly IRenderViewToString _render;
        public IndexModel(IBannerServices service, IRenderViewToString render)
        {
            _service = service;
            _render = render;
        }
        public List<BannerDto> Banners{ get; set; }

        public async Task OnGet()
        {
            Banners = await _service.GetAllBanner();
        }
          
        public async Task<IActionResult> OnGetShowAddPage()
        {
            return await AjaxTryCatch(async () =>
            {
                var view = await _render.RenderToStringAsync("_Add",new CreateBannerCommand(),PageContext);
                return ApiResult<string>.Success(view);
            });
        }
        public async Task<IActionResult> OnGetShowEditPage(long bannerId)
        {
            return await AjaxTryCatch(async () =>
            {
                var result = await _service.GetById(bannerId);

                if (result == null)
                    return ApiResult<string>.Error();
                var model = new EditBannerCommand()
                {
                    Id = result.Id,
                    Link = result.Link,
                    Position = result.Position
                };
                var view = await _render.RenderToStringAsync("_Edit",model,PageContext);
                return ApiResult<string>.Success(view);
            });
        }
        public async Task<IActionResult> OnPostCreateBanner(CreateBannerCommand command)
        {
            return await AjaxTryCatch(() =>
           _service.CreateBanner(command));
        }
        public async Task<IActionResult> OnPostEditBanner(EditBannerCommand command)
        {
            return await AjaxTryCatch(async () =>
            {
                var result = await _service.EditBanner(command);
                return result;
            });
        }
        public async Task<IActionResult> OnPostDeleteBanner(long bannerId)
        {
            return await AjaxTryCatch(() => _service.DeleteBanner(bannerId));
        }
    }
}
