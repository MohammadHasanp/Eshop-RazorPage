namespace Eshop_RazorPage.Models.User
{
    public class CreateUserCommand
    {
        public string UserName { get;  set; }
        public string FullName { get;  set; }
        public string Password { get; set; }
        public string Email { get;  set; }
        public string PhoneNumber { get;  set; }
        public Gender Gender { get;  set; }
    }
    public class EditUserCommand
    {
        public long UserId { get;  set; }
        public string UserName { get;  set; }
        public string FullName { get;  set; }
        public string Email { get;  set; }
        public string PhoneNumber { get;  set; }
        public Gender Gender { get;  set; }
        public IFormFile? Avatar { get;  set; }
    }
    public class ChangePasswordCommand
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
    public enum Gender
    {
        None,
        Male,
        Famele
    }

}

