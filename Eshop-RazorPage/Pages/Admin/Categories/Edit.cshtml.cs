using Eshop_RazorPage.Infrastructure.RazorUtil;
using Eshop_RazorPage.Models.Category;
using Eshop_RazorPage.Services.Category;
using Eshop_RazorPage.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Eshop_RazorPage.Pages.Admin.Categories
{
    [BindProperties]
    public class EditModel : BaseRazorPage
    {
        private readonly ICategoryServices _service;
        public EditModel(ICategoryServices service)
        {
            _service = service;
        }

        #region Properties

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "{0} را وارد کنید ")]
        public string Title { get; set; }
        [Display(Name = "Slug")]
        [Required(ErrorMessage = "{0}را وارد کنید ")]
        public string Slug { get; set; }

        public SeoDataViewModel? SeoData { get; set; }
        #endregion
        public async Task<IActionResult> OnGet(long Id)
        {
            var category = await _service.GetCategoryById(Id);
            if (category == null)
                return RedirectToPage("Index");

            Title = category.Title;
            Slug = category.Slug;
            SeoData = SeoDataViewModel.MapToSeoDataViewmodel(category.SeoData);
            return Page();
        }

        public async Task<IActionResult> OnPost(long Id)
        {
            var model = new EditCategoeyCommand() 
            {
              Id = Id,
              seoData = SeoData.MapToSeoData(),
              slug = Slug,
              title = Title,
            };
            var result = await _service.Edit(model);
            return RedirectAndShowAlert(result,RedirectToPage("Index"));
        }
    }
}
