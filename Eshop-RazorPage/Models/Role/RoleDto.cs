using Eshop_RazorPage.Models;

namespace Eshop_RazorPage.Models.Role
{
    public class RoleDto : BaseDto
    {
        public string Title { get; set; }
        public List<Permission> Permissions { get; set; }
    }
    public class AddRoleCommend
    {
        public string Title { get; set; }
        public List<Permission> Permissions { get; set; }
    }

    public class EditRoleCommand   
    {
        public long RoleId { get; set; }
        public string Title { get; set; }
        public List<Permission> Permissions { get; set; }
    }

}
