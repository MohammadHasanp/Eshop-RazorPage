using Common.Application;
using Eshop_RazorPage.Models;
using Eshop_RazorPage.Models.Category;

namespace Eshop_RazorPage.Services.Category
{
    public interface ICategoryServices
    {
        Task<ApiResult> Addchilld(AddChildCategoryCommand command);
        Task<ApiResult> CreateCategory(CreateCategoryCommand command);
        Task<ApiResult> Edit(EditCategoeyCommand command);
        Task<ApiResult> Delete(long categoryId);

        Task<CategoryDto?> GetCategoryById(long Id);
        Task<List<SubCategoryDto>?> GetCategoryByParentId(long parentId);

        Task<List<CategoryDto>?> GetAllCategory();
    }
}
