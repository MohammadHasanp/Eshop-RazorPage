using Eshop_RazorPage.Models.Role;
using System.ComponentModel.DataAnnotations;

namespace Eshop_RazorPage.ViewModel
{
    public class AddRoleViewModel
    {
        [Display(Name ="عنوان")]
        [Required(ErrorMessage ="{0} را وارد کنید ")]
        public string Title{ get; set; }
    }
    public class EditRoleViewModel
    {
        public long RoleId { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "{0} را وارد کنید ")]
        public string Title { get; set; }
        public List<Permission> Permissions { get; set; }
    }
}
