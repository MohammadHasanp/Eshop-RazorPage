using Eshop_RazorPage.Infrastructure.RazorUtil;
using Eshop_RazorPage.Models;
using Eshop_RazorPage.Models.Category;
using Eshop_RazorPage.Services.Category;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Threading.Tasks;

namespace Eshop_RazorPage.Pages.Admin.Categories
{
    public class IndexModel : BaseRazorPage
    {
        private readonly ICategoryServices _service;
        public IndexModel(ICategoryServices service)
        {
            _service = service;
        }

        public List<CategoryDto> Categories { get; set; }
        public async Task OnGet()
        {
            Categories =await _service.GetAllCategory();
        }
        public async Task<IActionResult>OnPostDelete(long Id)
        {
            return await AjaxTryCatch(async () =>
            {
                var result = await _service.Delete(Id);
                return result;
            });
        }
    }
}
