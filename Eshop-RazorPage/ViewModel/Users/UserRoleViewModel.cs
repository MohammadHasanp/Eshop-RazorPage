using Eshop_RazorPage.Models.User;

namespace Eshop_RazorPage.ViewModel.Users
{
    public class UserRoleViewModel
    {
        public List<long> UserId { get; set; }
        public List<string> PhoneNumber { get; set; }
        public List<UserRoleDto> Roles{ get; set; }
    }
}
