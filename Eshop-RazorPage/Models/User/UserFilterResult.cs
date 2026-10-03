using Eshop_RazorPage.Models;

namespace Eshop_RazorPage.Models.User
{
    public class UserFilterResult:BaseFilter<UserFilterData,UserFilterParams>
    {
    }
    public class UserFilterParams:BaseFilterParam
    {
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public long? Id { get; set; }
    }
    public class UserFilterData:BaseDto
    {
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string AvatarName { get; set; }
        public bool IsActive { get; set; }
        public Gender Gender { get; set; }
    }
}
