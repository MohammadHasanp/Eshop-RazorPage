using Common.Application.Validation.CustomValidation.IFormFile;
using Eshop_RazorPage.Infrastructure.RazorUtil;
using Eshop_RazorPage.Models.Slider;
using Eshop_RazorPage.Services.Slider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Eshop_RazorPage.Pages.Admin.Slider
{
    [BindProperties]
    public class AddModel : BaseRazorPage
    {
        private readonly ISliderServices _services;
        public AddModel(ISliderServices services)
        {
            _services = services;
        }

        [Display(Name ="عنوان")]
        [Required(ErrorMessage ="{0} را پر کنید ")]
        public string Title { get; set; }
        [Display(Name ="لینک")]
        [Required(ErrorMessage = "{0} را وارد کنید ")]
        public string Link { get; set; }

        [Display(Name ="تصویر")]
        [Required(ErrorMessage = "{0}را وارد کنید ")]
        [FileImage(ErrorMessage ="تصویر نامعتبر است")]
        public IFormFile ImageFile { get; set; }


        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            var model = new CreateSliderCommand()
            {
                ImageFile = ImageFile,
                Link = Link,
                Title = Title
            };
            var result = await _services.CreateSlider(model);
            return RedirectAndShowAlert(result,RedirectToPage("Index"));
        }
    }
}
