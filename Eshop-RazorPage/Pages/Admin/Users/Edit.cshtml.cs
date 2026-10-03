using Eshop_RazorPage.Infrastructure.CustomValidation.IFormFile;
using Eshop_RazorPage.Infrastructure.RazorUtil;
using Eshop_RazorPage.Models.User;
using Eshop_RazorPage.Services.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Abstractions;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Eshop_RazorPage.Pages.Admin.Users
{
    [BindProperties]
    public class EditModel : BaseRazorPage
    {
        private readonly IUserServices _service;
        public EditModel(IUserServices service)
        {
            _service = service;
        }

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "{0} را وارد کنید ")]
        public string UserName { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "{0} را وارد کنید ")]
        public string Email { get; set; }
        [Display(Name = "تلفن")]
        [Required(ErrorMessage = "{0} را وارد کنید ")]
        public string PhoneNumber { get; set; }
        [Display(Name = "جنسیت")]
        [Required(ErrorMessage = "{0} را وارد کنید ")]
        public Gender Gender { get; set; }

        [Display(Name = "پروفایل")]
        [FileImage(ErrorMessage = "فایل نامعتبر است")]
        public IFormFile? Avatar { get; set; }
        public async Task<IActionResult> OnGet(long userId)
        {
            var user = await _service.GetUserbyId(userId);
            if (user == null)
                return RedirectToPage("Index");

            UserName = user.userName;
            Email = user.email;
            PhoneNumber = user.phoneNumber;
            Gender = user.Gender;

            return Page();
        }
        public async Task<IActionResult> OnPost(long userId)
        {
            var model = new EditUserCommand()
            {
                Avatar = Avatar,
                Email = Email,
                Gender = Gender,
                PhoneNumber = PhoneNumber,
                UserId = userId,
                UserName = UserName,
            };

            var result = await _service.EditUser(model);
            return RedirectAndShowAlert(result, RedirectToPage("Index"));
        }

        public async Task<IActionResult> OnPostSetActiveUser(long userId)
        {
            var user = await _service.GetUserbyId(userId);
            if (user == null)
                return null;

            var model = new SetActiveCommand()
            {
                IsActive = !user.IsActive,
                UserId = userId,
            };
            var result = await _service.SetActive(model);
            return await AjaxTryCatch(async () =>
            {
                return result;
            },checkModelState:false);
        }
    }
}
