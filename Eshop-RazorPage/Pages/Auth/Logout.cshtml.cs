using Eshop_RazorPage.Infrastructure.RazorUtil;
using Eshop_RazorPage.Services.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eshop_RazorPage.Pages.Auth
{
    public class LogoutModel : BaseRazorPage
    {
        private readonly IAuthServices _service;
        public LogoutModel(IAuthServices service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnGet()
        {
            var result = await _service.Logout();
            HttpContext.Response.Cookies.Delete("token");
            HttpContext.Response.Cookies.Delete("refresh_Token");
            return RedirectAndShowAlert(result, RedirectToPage("../Index"));
        }
    }
}
