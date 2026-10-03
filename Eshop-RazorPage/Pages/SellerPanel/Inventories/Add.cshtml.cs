using Eshop_RazorPage.Infrastructure.CustomValidation.IFormFile;
using Eshop_RazorPage.Infrastructure.RazorUtil;
using Eshop_RazorPage.Models.Seller;
using Eshop_RazorPage.Services.Seller;
using Eshop_RazorPage.Services.Seller.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Eshop_RazorPage.Pages.SellerPanel.Inventories
{
    [BindProperties]
    public class AddModel : BaseRazorPage
    {
        private readonly ISellerInventoryService _service;
        private readonly ISellerServices _sellerService;
        public AddModel(ISellerInventoryService service, ISellerServices sellerService)
        {
            _service = service;
            _sellerService = sellerService;
        }

        //[Display(Name ="تصویر محصول")]
        //[Required(ErrorMessage ="{0} را وارد کنید ")]
        //[FileImage(ErrorMessage ="تصویر نامعتبر است")]
        //public IFormFile ImageFile { get; set; }

        //[Display(Name ="عنوان")]
        //[Required(ErrorMessage ="{0} را وارد کنید ")]
        //public string Title { get; set; }
        public long ProductId { get; set; }
        [Display(Name ="قیمت")]
        [Required(ErrorMessage ="{0} را وارد کنید ")]
        public int Price { get; set; }
        [Display(Name ="تعداد موجود")]
        [Required(ErrorMessage ="{0} را وارد کنید ")]
        public int Count { get; set; }
        [Display(Name = "درصد تخفیف")]
        [Required(ErrorMessage ="{0} را وارد کنید ")]
        [Range(0,50,ErrorMessage ="درصد تخفیف باید بین 0 تا 50 باشد")] 
        public int DiscountPercentage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var seller = await _sellerService.GetCurrentUserSeller();
            if (seller == null)
                return Redirect("/");

            var model = new AddSellerInventoryCommand()
            {
                count = Count,
                DiscountPercentage = DiscountPercentage,
                price = Price,
                productId = ProductId,
                sellerId = seller.Id
            };
            var result = await _service.AddSellerInventory(model);
            return RedirectAndShowAlert(result,RedirectToPage("Index"));
        }
    }
}
