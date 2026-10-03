using Eshop_RazorPage.Models;
using Eshop_RazorPage.Models.Category;
using System.Net.Http.Json;

namespace Eshop_RazorPage.Services.Category
{
    public class CategoryServices: ICategoryServices
    {
       private readonly HttpClient _client;
        public CategoryServices(HttpClient client)
        {
            _client = client;
        }
        public async Task<ApiResult> Addchilld(AddChildCategoryCommand command)
        {
            var result = await _client.PostAsJsonAsync("Category/AddChild",command);
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        }

        public async Task<ApiResult> CreateCategory(CreateCategoryCommand command)
        {
            var result = await _client.PostAsJsonAsync("Category", command);
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        }

        public async Task<ApiResult> Delete(long categoryId)
        {
            var result = await _client.DeleteAsync($"Category/{categoryId}");
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        }

        public async Task<ApiResult> Edit(EditCategoeyCommand command)
        {
            var result = await _client.PutAsJsonAsync("Category",command);
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        }

        public async Task<List<CategoryDto>?> GetAllCategory()
        {
            var result = await _client.GetFromJsonAsync<ApiResult<List<CategoryDto>>>("Category");
            return result?.Data;
        }

        public async Task<CategoryDto?> GetCategoryById(long Id)
        {
            var result = await _client.GetFromJsonAsync<ApiResult<CategoryDto>>($"Category/{Id}");
            return result?.Data;
        }

        public async Task<List<SubCategoryDto>?> GetCategoryByParentId(long parentId)
        {
            var result = await _client.GetFromJsonAsync<ApiResult<List<SubCategoryDto>>>($"Category/GetChild/{parentId}");
            return result?.Data;
        }
    }
}
