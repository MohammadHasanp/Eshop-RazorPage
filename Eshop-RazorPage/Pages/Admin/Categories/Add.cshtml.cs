using Eshop_RazorPage.Infrastructure.RazorUtil;
using Eshop_RazorPage.Models;
using Eshop_RazorPage.Models.Category;
using Eshop_RazorPage.Pages.Admin.EditorTemplates;
using Eshop_RazorPage.Services.Category;
using Eshop_RazorPage.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;

namespace Eshop_RazorPage.Pages.Admin.Categories
{
    [BindProperties]
    public class AddModel : BaseRazorPage
    {
        private readonly ICategoryServices _service;
        public AddModel(ICategoryServices service)
        {
            _service = service;
        }

        #region Properties
        
        [Display(Name = "عنوان")]
        [Required(ErrorMessage ="{0} را وارد کنید ")]
        public string Title { get; set; }
        [Display(Name ="Slug")]
        [Required(ErrorMessage ="{0}را وارد کنید ")]
        public string Slug { get; set; }

        public SeoDataViewModel? SeoData { get; set; }
        #endregion


        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(long? parentId)
        {
            if(parentId == null)
            {
                var model = new CreateCategoryCommand()
                {
                    seoData = SeoData.MapToSeoData(),
                    slug = Slug,
                    title = Title
                };
                var result =await _service.CreateCategory(model);
                return RedirectAndShowAlert(result, RedirectToPage("Index"));
            }
            var modelChild = new AddChildCategoryCommand()
            {
                parentId = (long) parentId,
                seoData = SeoData.MapToSeoData(),
                slug = Slug,
                title =Title,
            };
            var resultChild = await _service.Addchilld(modelChild);
            return RedirectAndShowAlert(resultChild, RedirectToPage("Index"));
        }
    }
}
