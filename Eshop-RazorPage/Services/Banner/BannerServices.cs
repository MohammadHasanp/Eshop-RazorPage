using Common.Application.SecurityUtil;
using Eshop_RazorPage.Models;
using Eshop_RazorPage.Models.Banner;

namespace Eshop_RazorPage.Services.Banner
{
    public class BannerServices : IBannerServices
    {
        private readonly HttpClient _client;
        public BannerServices(HttpClient client)
        {
            _client = client;
        }
        public async Task<ApiResult> CreateBanner(CreateBannerCommand command)
        {
            var fromData = new MultipartFormDataContent();
            fromData.Add(new StringContent(command.Link), "Link");
            fromData.Add(new StringContent(command.Position.ToString()), "Position");
            fromData.Add(new StreamContent(command.ImageFile.OpenReadStream()), "ImageFile",command.ImageFile.FileName);
            var result = await _client.PostAsync("Banner", fromData);
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        }

        public async Task<ApiResult?> DeleteBanner(long bannerId)
        {
            var result = await _client.DeleteAsync($"Banner/{bannerId}");
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        }

        public async Task<ApiResult> EditBanner(EditBannerCommand command)
        {
            var fromData = new MultipartFormDataContent();
            fromData.Add(new StringContent(command.Id.ToString()), "BannerId");
            fromData.Add(new StringContent(command.Link), "Link");
            fromData.Add(new StringContent(command.Position.ToString()), "Position");
            if(command.ImageFile.IsImage() &&command.ImageFile!=null)
            fromData.Add(new StreamContent(command.ImageFile.OpenReadStream()), "ImageFile", command.ImageFile.FileName);

            var result = await _client.PutAsync("Banner",fromData);
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        }

        public async Task<List<BannerDto>?> GetAllBanner()
        {
            var result = await _client.GetFromJsonAsync<ApiResult<List<BannerDto>>>("Banner");

            if (result.Data == null)
                return new List<BannerDto>();

            return result?.Data;
        }

        public async Task<BannerDto?> GetById(long Id)
        {
            var result = await _client.GetFromJsonAsync<ApiResult<BannerDto>>($"Banner/{Id}");
            return result?.Data;
        }
    }
}
