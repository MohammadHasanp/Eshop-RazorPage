using Common.Application.SecurityUtil;
using Eshop_RazorPage.Models;
using Eshop_RazorPage.Models.Slider;

namespace Eshop_RazorPage.Services.Slider
{
    public class SliderServices:ISliderServices
    {
        private readonly HttpClient _client;
        public SliderServices(HttpClient clien)
        {
            _client = clien;
        }

        public async Task<ApiResult> CreateSlider(CreateSliderCommand command)
        {
            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(command.Title), "Title");
            formData.Add(new StringContent(command.Link), "Link");
            if (command.ImageFile.IsImage())
                formData.Add(new StreamContent(command.ImageFile.OpenReadStream()), "ImageFile", command.ImageFile.FileName);

            var result = await _client.PostAsync("Slider", formData);
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        
        }

        public async Task<ApiResult> DeleteSlider(long sliderId)
        {
            var result = await _client.DeleteAsync($"DeleteSlider/{sliderId}");
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        }

        public async Task<ApiResult> Editslider(EditSliderCommand command)
        {
            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(command.Title),"Title");
            formData.Add(new StringContent(command.Link),"Link");
            formData.Add(new StringContent(command.SliderId.ToString()), "SliderId");
            if (command.ImageFile != null && command.ImageFile.IsImage())
                formData.Add(new StreamContent(command.ImageFile.OpenReadStream()), "ImageFile", command.ImageFile.FileName);
            var result = await _client.PutAsync("Slider", formData);
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        }

        public async Task<List<SliderDto>> GetAllSlider()
        {
            var result = await _client.GetFromJsonAsync<ApiResult<List<SliderDto>>>("Slider");
            return result?.Data;
        }

        public async Task<SliderDto> GetSliderById(long sliderId)
        {
            var result = await _client.GetFromJsonAsync<ApiResult<SliderDto>>($"Slider/{sliderId}");
            return result?.Data;
        }
    }
}
