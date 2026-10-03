using Eshop_RazorPage.Models;
using Eshop_RazorPage.Models.Slider;

namespace Eshop_RazorPage.Services.Slider
{
    public interface ISliderServices
    {
        Task<ApiResult> CreateSlider(CreateSliderCommand command);
        Task<ApiResult> Editslider(EditSliderCommand command);
        Task<ApiResult> DeleteSlider(long sliderId);


        Task<SliderDto?> GetSliderById(long sliderId);
        Task<List<SliderDto?>> GetAllSlider();
    }
}
