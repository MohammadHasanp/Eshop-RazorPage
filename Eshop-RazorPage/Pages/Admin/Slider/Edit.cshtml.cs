using Common.Application.Validation.CustomValidation.IFormFile;
using Eshop_RazorPage.Infrastructure.RazorUtil;
using Eshop_RazorPage.Models.Slider;
using Eshop_RazorPage.Services.Slider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Eshop_RazorPage.Pages.Admin.Slider
{
    [BindProperties]
    public class EditModel : BaseRazorPage
    {
        private readonly ISliderServices _service;
        public EditModel(ISliderServices service)
        {
            _service = service;
        }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "{0} را پر کنید ")]
        public string Title { get; set; }
        [Display(Name = "لینک")]
        [Required(ErrorMessage = "{0} را وارد کنید ")]
        public string Link { get; set; }

        [Display(Name = "تصویر")]
        [FileImage(ErrorMessage = "تصویر نامعتبر است")]
        public IFormFile? ImageFile { get; set; }
        public string  ImageName { get; set; }


        public async Task OnGet(long id)
        {
            var slider = await _service.GetSliderById(id);
            if (slider == null)
                RedirectToPage("Index");

            ImageName = slider.ImageName;
            Title = slider.Title;
            Link = slider.Link;
        }
        public async Task<IActionResult> OnPost(long id)
        {
            var model = new EditSliderCommand()
            {
                SliderId = id,
                ImageFile = ImageFile,
                Link = Link,
                Title = Title
            };
            var result =await _service.Editslider(model);
            return RedirectAndShowAlert(result,RedirectToPage("Index"));
        }
    }
}
