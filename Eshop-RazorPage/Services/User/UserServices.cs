using Common.Application.SecurityUtil;
using Eshop_RazorPage.Infrastructure;
using Eshop_RazorPage.Models;
using Eshop_RazorPage.Models.User;

namespace Eshop_RazorPage.Services.User
{
    public class UserServices: IUserServices
    {
        private readonly HttpClient _client;
        public UserServices(HttpClient client)
        {
            _client = client;
        }

        public async Task<ApiResult> ChangePassword(ChangePasswordCommand command)
        {
            var result = await _client.PutAsJsonAsync("User/ChangePassword",command);
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        }

        public async Task<ApiResult> CreateUser(CreateUserCommand command)
        {
            var result = await _client.PostAsJsonAsync("User",command);
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        }

        public async Task<ApiResult> EditUser(EditUserCommand command)
        {
            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(command.Email), "Email");
            formData.Add(new StringContent(command.UserId.ToString()), "UserId");
            formData.Add(new StringContent(command.UserName), "UserName");
            if(command.FullName != null)
                 formData.Add(new StringContent(command.FullName), "FullName");
            formData.Add(new StringContent(command.PhoneNumber), "PhoneNumber");
            formData.Add(new StringContent(command.Gender.ToString()), "Gender");

            if(command.Avatar != null && command.Avatar.IsImage())
              formData.Add(new StreamContent(command.Avatar.OpenReadStream()), "Avatar",command.Avatar.FileName);

            var result = await _client.PutAsync("User/Edit",formData);
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        }

        public async Task<ApiResult> EditUserCurrent(EditUserCommand command)
        {
            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(command.Email), "Email");
            formData.Add(new StringContent(command.UserId.ToString()), "UserId");
            formData.Add(new StringContent(command.UserName), "UserName");
            if (command.FullName != null)
                formData.Add(new StringContent(command.FullName), "FullName");
            formData.Add(new StringContent(command.PhoneNumber), "PhoneNumber");
            formData.Add(new StringContent(command.Gender.ToString()), "Gender");

            if (command.Avatar != null)
                formData.Add(new StreamContent(command.Avatar.OpenReadStream()), "Avatar", command.Avatar.FileName);

            var result = await _client.PutAsync("User/Current", formData);
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        }

        public async Task<List<UserDto?>> GetAllUser()
        {
            var result = await _client.GetFromJsonAsync<ApiResult<List<UserDto>>>("User");
            return result?.Data;
        }

        public async Task<UserDto?> GetCurrentUser()
        {
            var result = await _client.GetFromJsonAsync<ApiResult<UserDto>>("User/Current");
            return result?.Data;
        }

        public async Task<UserFilterResult?> GetUserByFilter(UserFilterParams filterParams)
        {
            var url = filterParams.GenerateBaseFilterUrl("User/GetByFilter") +
                   $"&email={filterParams.Email}&phoneNumber={filterParams.PhoneNumber}&id={filterParams.Id}";

            var reasult = await _client.GetFromJsonAsync<ApiResult<UserFilterResult>>(url);
            return reasult?.Data;
        }

        public async Task<UserDto?> GetUserbyId(long userId)
        {
            var result = await _client.GetFromJsonAsync<ApiResult<UserDto>>($"User/byId/{userId}");
            return result?.Data;
        }

        public async Task<ApiResult> SetActive(SetActiveCommand command)
        {
            var result = await _client.PostAsJsonAsync("User/SetActive",command);
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        }
    }
}
