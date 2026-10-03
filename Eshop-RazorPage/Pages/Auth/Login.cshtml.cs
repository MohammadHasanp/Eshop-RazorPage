using Eshop_RazorPage.Infrastructure.RazorUtil;
using Eshop_RazorPage.Models.Auth;
using Eshop_RazorPage.Services.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Eshop_RazorPage.Pages.Auth
{
    [BindProperties]

    public class LoginModel : BaseRazorPage
    {
        private readonly IAuthServices _service;
        public LoginModel(IAuthServices service)
        {
            _service = service;
        }

        #region Properties
        [Display(Name = "شماره مبایل")]
        [Required(ErrorMessage = "{0} را وارد کنید ")]
        [MaxLength(11, ErrorMessage = "فیلد تلفن باید 11 کاراکتر باشد")]
        public string PhoneNumber { get; set; }
        // ----------------------------------------------
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "{0} را وارد کنید ")]
        [MinLength(7, ErrorMessage = "فیلد کلمه عبور باید بیشتر از 7 کاراکتر باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string? RedirectTo { get; set; }

        #endregion

        public IActionResult OnGet(string redirectTo)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }
            RedirectTo = redirectTo;
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            var result = await _service.Login(new LoginCommand()
            {
                Password = Password,
                PhoneNumber = PhoneNumber
            });
            if (!result.IsSuccess)
            {
                ModelState.AddModelError(nameof(PhoneNumber), result.MetaData.Message);
                return Page();
            }
            var token = result.Data.Token;
            var refreshToken = result.Data.RefreshToken;
            HttpContext.Response.Cookies.Append("token", token, new CookieOptions()
            {
                HttpOnly = true,
                Expires = DateTimeOffset.Now.AddDays(7)
            });
            HttpContext.Response.Cookies.Append("refresh_Token", refreshToken, new CookieOptions()
            {
                HttpOnly = true,
                Expires = DateTimeOffset.Now.AddDays(10)
            });

            if (!string.IsNullOrWhiteSpace(RedirectTo))
                return LocalRedirect(RedirectTo);

            return RedirectToPage("../Index");
        }
    }
}
