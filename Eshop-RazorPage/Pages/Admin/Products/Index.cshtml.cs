using Eshop_RazorPage.Infrastructure.RazorUtil;
using Eshop_RazorPage.Models.Category;
using Eshop_RazorPage.Models.Product;
using Eshop_RazorPage.Services.Category;
using Eshop_RazorPage.Services.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Eshop_RazorPage.Pages.Admin.Products
{
    [BindProperties]
    public class IndexModel : BaseRazorFilter<ProductFilterParams>
    {
        private readonly ICategoryServices _categoryServices;
        private readonly IProductServices _service;
        public IndexModel(IProductServices service, ICategoryServices categoryServices)
        {
            _service = service;
            _categoryServices = categoryServices;
        }

        public ProductFilterResult ProductResult { get; set; }

        public async Task OnGet()
        {
            ProductResult = await _service.GetProductByFilter(FilterParams);
        }
        public async Task<IActionResult> OnGetLoadSubCategories(long parentId)
        {
            var options = "<option value='0'>انتخاب کنید</option>";
            var child = await _categoryServices.GetCategoryByParentId(parentId);
            child.ForEach(f =>
            {
                options += $"<option value='{f.Id}'>{f.Title}</option>";
            });
            return Content(options.Trim());
        }
    }
}
