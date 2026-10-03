using Common.Application.Validation.CustomValidation.IFormFile;
using Eshop_RazorPage.Infrastructure.RazorUtil;
using Eshop_RazorPage.Models.Product;
using Eshop_RazorPage.Services.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Eshop_RazorPage.Pages.Admin.Products.Galleries
{
    [BindProperties]
    public class IndexModel : BaseRazorPage
    {
        private readonly IProductServices _service;
        public IndexModel(IProductServices service)
        {
            _service = service;
        }
        public List<ProductImageDto> Images { get; set; }
        
        [Display(Name ="تصویر")]
        [Required(ErrorMessage ="{0} را وارد کنید ")]
        [FileImage(ErrorMessage ="تصویر نا معتبر است ")]
        public IFormFile ImageFile { get; set; }

        [Display(Name ="ترتیب نمایش")]
        [Required(ErrorMessage ="{0} را وارد کنید ")]
        public int Sequence { get; set; }

        public async Task<IActionResult> OnGet(long productId)
        {
            var product = await _service.GetProductById(productId);
            if (product == null)
                return RedirectToPage("Index");

            Images = product.Images;
            return Page();
        }
        public async Task<IActionResult> OnPost(long productId)
        {
            return await AjaxTryCatch(async () =>
            {
                var model = new AddImageProductCommand()
                {
                    ImageFile = ImageFile,
                    ProductId = productId,
                    Sequence = Sequence,
                };
                var result = await _service.AddImageProduct(model);
                return result;
            });
        }
        public async Task<IActionResult> OnPostDeleteItem(long productId,long id)
        {
            return await AjaxTryCatch(() =>
            {
                var model = new RemoveProductCommand()
                {
                    ProductId = productId,
                    ImageId = id,
                };
                return _service.DeleteProduct(model);
            },checkModelState:false);
        }
    }
}
