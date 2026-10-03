using Eshop_RazorPage.Infrastructure.RazorUtil;
using Eshop_RazorPage.Models.Slider;
using Eshop_RazorPage.Services.Slider;
using Microsoft.AspNetCore.Mvc;

namespace Eshop_RazorPage.Pages.Admin.Slider
{
    public class IndexModel : BaseRazorPage
    {
        private readonly ISliderServices _services;

        public IndexModel(ISliderServices services)
        {
            _services = services;
        }

        public List<SliderDto> Sliders{ get; set; }

        public async Task OnGet()
        {
            Sliders =await _services.GetAllSlider();
        }

        public async Task<IActionResult> OnPostDeleteSlider(long sliderId)
        {
            return await AjaxTryCatch(async () =>
            {
                return await _services.DeleteSlider(sliderId);
            });
        }
    }
}
