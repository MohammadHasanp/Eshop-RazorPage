using Common.Application.Validation.CustomValidation.IFormFile;
using Eshop_RazorPage.Infrastructure.RazorUtil;
using Eshop_RazorPage.Models.Product;
using Eshop_RazorPage.Services.Product;
using Eshop_RazorPage.ViewModel;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Eshop_RazorPage.Pages.Admin.Products
{
    [BindProperties]
    public class EditModel : BaseRazorPage
    {
        private readonly IProductServices _service;

        public EditModel(IProductServices service)
        {
            _service = service;
        }

        #region Properties
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "{0} را وارد کنید ")]
        public string Title { get; set; }
        [Display(Name = "تصویر")]
        [FileImage(ErrorMessage = "تصویر نامعتبر است")]
        public IFormFile? ImageFile { get; set; }
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
        //
        public SeoDataViewModel? SeoData { get; set; }
        public Dictionary<string, string> Specifications { get; set; }

        public List<string> Keys { get; set; } = new();
        public List<string> Values { get; set; } = new();
        public string? ImageName { get; set; }
        #endregion

        public async Task<IActionResult> OnGet(long productId)
        {
            var product = await _service.GetProductById(productId);
            if (product == null)
                return RedirectToPage("Index");

            Title = product.Title;
            Slug = product.Slug;
            ImageName = product.ImageName;
            Description = product.Description;
            CategoryId = product.Category.Id;
            SubCategoryId = product.SubCategory.Id;
            SecondarySubCategory = product.SecondarySubCategory.Id;
            SeoData = SeoDataViewModel.MapToSeoDataViewmodel(product.SeoData);

            product.Specifications.ForEach(s =>
            {
                Keys.Add(s.Key);
            });
            product.Specifications.ForEach(s =>
            {
                Values.Add(s.Value);
            });
            return Page();
        }
        public async Task<IActionResult> OnPost(long productId)
        {
            var model = new EditProductCommand()
            {
                CategoryId = CategoryId,
                Description = Description,
                ImageFile = ImageFile,
                ProductId = productId,
                SecondarySubCategoryId = SecondarySubCategory,
                SeoData = SeoData.MapToSeoData(),
                Slug = Slug,
                SubCategoryId = SubCategoryId,
                Title = Title,
                Specifications = ConvertSpecifications()
            };

            var result = await _service.EditProduct(model);
            return RedirectAndShowAlert(result, RedirectToPage("Index"));
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
