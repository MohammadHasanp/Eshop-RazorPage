using Eshop_RazorPage.Models.Product;
using Eshop_RazorPage.Services.Product;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eshop_RazorPage.Pages
{
    public class SearchModel : PageModel
    {
        private readonly IProductServices _service;
        public SearchModel(IProductServices service)
        {
            _service = service;
        }
        public ProductShopResult FilterResult{ get; set; }

        public async Task OnGet(string q = "", int pageId = 1, string category = "", bool? haveDiscount = null, bool justAvailableProducts = false)
        {
            FilterResult = await _service.GetShopProduct(new ProductShopFilterParam()
            {
                Take = 18,
                CategorySlug = category.Trim(),
                JustHasDiscount = haveDiscount,
                OnlyAvailableProducts = justAvailableProducts,
                PageId = pageId,
                Search = q,
                SearchOrderBy = ProductSearchOrderBy.Latest,
            });
        }
    }
}
