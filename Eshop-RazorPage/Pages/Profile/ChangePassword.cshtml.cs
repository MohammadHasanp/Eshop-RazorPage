using Eshop_RazorPage.Infrastructure.RazorUtil;
using Eshop_RazorPage.Models.User;
using Eshop_RazorPage.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Eshop_RazorPage.Pages.Profile
{
    [BindProperties]

    public class ChangePasswordModel : BaseRazorPage
    {
        private readonly IUserServices _services;
        public ChangePasswordModel(IUserServices services)
        {
            _services = services;
        }

        #region Properties
        [Display(Name = "کلمه عبور فعلی")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Display(Name = "کلمه عبور جدید")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [MinLength(6, ErrorMessage = "کلمه عبور باید بیشتر از 5 کاراکتر باشد")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "تکرار کلمه عبور")]
        [Compare(nameof(NewPassword), ErrorMessage = "کلمه های عبور یکسان نیستند")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string ConfirmPassword { get; set; }

        #endregion
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPost()
        {
            var result = await _services.ChangePassword(new ChangePasswordCommand()
            {
                ConfirmPassword = ConfirmPassword,
                CurrentPassword = CurrentPassword,
                NewPassword = NewPassword,
            });
            return RedirectAndShowAlert(result, RedirectToPage("Index"));
        }
    }
}
