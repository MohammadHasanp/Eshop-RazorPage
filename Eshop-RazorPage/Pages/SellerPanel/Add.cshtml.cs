using Eshop_RazorPage.Infrastructure.RazorUtil;
using Eshop_RazorPage.Models.Seller;
using Eshop_RazorPage.Services.Role;
using Eshop_RazorPage.Services.Seller;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Eshop_RazorPage.Pages.SellerPanel
{
    [BindProperties]
    public class AddModel : BaseRazorPage
   {
        private readonly ISellerServices _service;
        private readonly IRoleServices _roleService;

        public AddModel(ISellerServices service, IRoleServices roleService)
        {
            _service = service;
            _roleService = roleService;
        }

        [Display(Name ="نام فروشگاه")]
        [Required(ErrorMessage ="{0} را وارد کنید ")]
        public string shopName { get; set; }
        [Display(Name = "کدملی")]
        [Required(ErrorMessage = "{0} را وارد کنید ")]
        public string NationalCode { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var model = new CreateSellerCommand()
            {
                NationalCode = NationalCode,
                ShopName = shopName,
            };
            var result = await _service.CreateSeller(model);
            return RedirectAndShowAlert(result, RedirectToPage("Index"));
        }
    }
}
