using Eshop_RazorPage.Infrastructure.RazorUtil;
using Eshop_RazorPage.Models.Auth;
using Eshop_RazorPage.Services.Auth;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Eshop_RazorPage.Pages.Auth
{
    [BindProperties]
    public class RegisterModel : BaseRazorPage
    {
        private readonly IAuthServices _services;
        public RegisterModel(IAuthServices services)
        {
            _services = services;
        }
        #region Properties
        [Display(Name = " شماره موبایل")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [MaxLength(11, ErrorMessage = "فیلد تلفن باید 11 کاراکتر باشد")]
        public string PhoneNumber { get; set; }
        //_____________________________________________________
        [Display(Name = "کلمه عبور ")]
        [Required(ErrorMessage = "{0} را وارد کنید ")]
        [MinLength(7, ErrorMessage = "فیلد کلمه عبور باید بیشتر از 7 کاراکتر باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        //_____________________________________________________
        [Display(Name = " تکرار کلمه عبور ")]
        [Required(ErrorMessage = " {0}  را  وارد  کنید    ")]
        [MinLength(7, ErrorMessage = "فیلد تکرار کلمه عبور باید بیشتر از 7 کاراکتر باشد")]
        [Compare("Password", ErrorMessage = "کلمه عبور با تکرار کلمه عبور یکسان نیست")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        #endregion

        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPost()
        {
            var result = await _services.Register(new RegisterCommand()
            {
                ConfirmPassword = ConfirmPassword,
                Password = Password,
                PhoneNumber = PhoneNumber
            });

            return RedirectAndShowAlert(result, RedirectToPage("Login"));
        }
    }
}
