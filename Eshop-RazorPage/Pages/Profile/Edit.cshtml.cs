using Eshop_RazorPage.Infrastructure.RazorUtil;
using Eshop_RazorPage.Models;
using Eshop_RazorPage.Models.User;
using Eshop_RazorPage.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Eshop_RazorPage.Pages.Profile
{
    [Authorize]
    [BindProperties]
    public class EditModel : BaseRazorPage
    {
        private readonly IUserServices _service;
        public EditModel(IUserServices service)
        {
            _service = service;
        }

        #region Properties

        [Display(Name = "نام کاربری ")]
        [Required(ErrorMessage = "{0}را وارد کنید ")]
        public string UserName { get; set; }

        [Display(Name = "نام خوانوادگی")]
        [Required(ErrorMessage = "{0} راوارد کنید ")]
        public string FullName { get; set; }

        [Display(Name = "پست الکترونیکی")]
        [Required(ErrorMessage = "{0} را وارد کنید ")]
        public string Email { get; set; }

        [Display(Name = "شماره مبایل")]
        [Required(ErrorMessage = "{0} را وارد کنید ")]
        [MaxLength(11,ErrorMessage ="فیلد شماره مبایل باید 11 کاراکتر باشد")]
        [MinLength(11,ErrorMessage ="فیلد شماره مبایل باید 11 کاراکتر باشد")]
        public string PhoneNumber { get; set; }

        [Display(Name = "جنسیت")]
        public Gender Gender { get; set; } = Gender.None;

        [Display(Name = "تصویر پروفایل")]
        public IFormFile? Avatar { get; set; }
        #endregion

        public async Task OnGet()
        {
            var user = await _service.GetCurrentUser();
            UserName = user.userName;
            FullName = user.fullName;
            Email = user.email;
            PhoneNumber = user.phoneNumber;
            Gender = user.Gender;
            Avatar = Avatar;
        }
        public async Task<IActionResult> OnPost()
        {
            var result = await _service.EditUserCurrent(new EditUserCommand()
            {
                Avatar = Avatar,
                Email = Email,
                FullName = FullName,
                Gender = Gender,
                PhoneNumber = PhoneNumber,
                UserName = UserName
            });
            return RedirectAndShowAlert(result,RedirectToPage("Index"));
        }
        public async Task<IActionResult> OnGetData()
        {
            return await AjaxTryCatch(async () =>
            {
                var user = await _service.GetCurrentUser();
                return ApiResult.Success();
            });
        }
    }
}
