using Common.Application.Validation.CustomValidation.IFormFile;
using Eshop_RazorPage.Infrastructure.RazorUtil;
using Eshop_RazorPage.Models;
using Eshop_RazorPage.Models.Product;
using Eshop_RazorPage.Services.Product;
using Eshop_RazorPage.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Eshop_RazorPage.Pages.Admin.Products
{
    [BindProperties]
    public class AddModel : BaseRazorPage
    {
        private readonly IProductServices _service;

        public AddModel(IProductServices service)
        {
            _service = service;
        }

        #region Properties
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "{0} را وارد کنید ")]
        public string Title { get; set; }
        [Display(Name = "تصویر")]
        [Required(ErrorMessage = "{0} را وارد کنید ")]
        [FileImage(ErrorMessage = "تصویر نامعتبر است")]
        public IFormFile ImageFile { get; set; }
        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "{0} را وارد کنید ")]
        [UIHint("CKeditor4")]
        public string Description { get; set; }

        [Display(Name = "دسته بندی")]
        [Required(ErrorMessage = "{0} را وارد کنید ")]
        [Range(1, long.MaxValue, ErrorMessage = "دسته بندی را وارد کنید")]
        public long CategoryId { get; set; }
        [Display(Name = "زیر دسته بندی")]
        [Required(ErrorMessage = "{0} را وارد کنید ")]
        [Range(1, long.MaxValue, ErrorMessage = "زیر دسته بندی را وارد کنید")]
        public long SubCategoryId { get; set; }

        [Display(Name = "دسته بندی سوم")]
        [Required(ErrorMessage = "{0} را وارد کنید ")]
        public long? SecondarySubCategory { get; set; }
        [Display(Name = "Slug")]
        [Required(ErrorMessage = "{0} را وارد کنید ")]
        public string Slug { get; set; }



        public SeoDataViewModel SeoData { get; set; }
        public Dictionary<string, string> Specifications { get; set; }

        public List<string> Keys { get; set; } = new();
        public List<string> Values { get; set; } = new();
        #endregion

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            if (SecondarySubCategory == 0)
                SecondarySubCategory = null;

            var model = new CreateProductCommand()
            {
                CategoryId = CategoryId,
                Description = Description,
                ImageFile = ImageFile,
                SecondarySubCategory = SecondarySubCategory,
                SeoData = SeoData.MapToSeoData(),
                Slug = Slug,
                SubCategoryId = SubCategoryId,
                Title = Title,
                Specifications = ConvertSpecifications(),
            };
            var result = await _service.CreateProduct(model);
            return RedirectAndShowAlert(result,RedirectToPage("Index"));
        }
        private Dictionary<string, string> ConvertSpecifications()
        {
            var specifications = new Dictionary<string, string>();
            Keys.RemoveAll(r => r == null || string.IsNullOrWhiteSpace(r));
            Values.RemoveAll(r => r == null || string.IsNullOrWhiteSpace(r));
            for (var i = 0; i < Keys.Count; i++)
            {
                specifications.Add(Keys[i], Values[i]);
            }

            return specifications;
        }
    }

}
