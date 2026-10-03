namespace Eshop_RazorPage.Models.User
{
    public class UserRoleDto
    {
        public int roleId { get; set; }
        public string roleTitle { get; set; }
    }

    public class UserDto : BaseDto
    {
        public string userName { get; set; }
        public string fullName { get; set; }
        public string avatarName { get; set; }
        public string password { get; set; }
        public bool IsActive { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public Gender Gender{ get; set; }
        public List<UserRoleDto> roles { get; set; }
    }
}
