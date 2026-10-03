using Eshop_RazorPage.Models;
using Eshop_RazorPage.Models.User;

namespace Eshop_RazorPage.Services.User
{
    public interface IUserServices
    {
        Task<ApiResult> CreateUser(CreateUserCommand command);
        Task<ApiResult> EditUser(EditUserCommand command);
        Task<ApiResult> EditUserCurrent(EditUserCommand command);
        Task<ApiResult> ChangePassword(ChangePasswordCommand command);
        Task<ApiResult> SetActive(SetActiveCommand command);

        Task<UserFilterResult> GetUserByFilter(UserFilterParams filterParams);
        Task<UserDto?> GetUserbyId(long userId);
        Task<UserDto?> GetCurrentUser();
        Task<List<UserDto>> GetAllUser();
    }
}
