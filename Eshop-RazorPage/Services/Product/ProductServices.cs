using Eshop_RazorPage.Models;
using Eshop_RazorPage.Models.Product;
using Newtonsoft.Json;
using System.Text;

namespace Eshop_RazorPage.Services.Product
{
    public class ProductServices : IProductServices
    {
        private readonly HttpClient _client;
        public ProductServices(HttpClient client)
        {
            _client = client;
        }

        public async Task<ApiResult> AddImageProduct(AddImageProductCommand command)
        {
            var fromData = new MultipartFormDataContent();
            fromData.Add(new StringContent(command.ProductId.ToString()), "ProductId");
            fromData.Add(new StringContent(command.Sequence.ToString()), "Sequence");
            fromData.Add(new StreamContent(command.ImageFile.OpenReadStream()), "ImageFile", command.ImageFile.FileName);

            var result = await _client.PostAsync("Product/Image", fromData);
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        }

        public async Task<ApiResult> CreateProduct(CreateProductCommand command)
        {
            var formData = new MultipartFormDataContent();
            formData.Add(new StreamContent(command.ImageFile.OpenReadStream()), "ImageFile", command.ImageFile.FileName);
            formData.Add(new StringContent(command.Title), "Title");
            formData.Add(new StringContent(command.Description), "Description");
            formData.Add(new StringContent(command.CategoryId.ToString()), "CategoryId");
            formData.Add(new StringContent(command.SubCategoryId.ToString()), "SubCategoryId");

            if (command.SecondarySubCategory != null)
                formData.Add(new StringContent(command.SecondarySubCategory.ToString() ?? string.Empty), "SecondarySubCategory");

            formData.Add(new StringContent(command.Slug), "Slug");
            formData.Add(new StringContent(command.SeoData.MetaTitle), "SeoData.MetaTitle");
            formData.Add(new StringContent(command.SeoData.Canonical), "SeoData.Canonical");
            formData.Add(new StringContent(command.SeoData.MetaKeyWords), "SeoData.MetaKeyWords");
            formData.Add(new StringContent(command.SeoData.MetaDescription), "SeoData.MetaDescription");
            formData.Add(new StringContent(command.SeoData.IndexPage.ToString()), "SeoData.IndexPage");
            formData.Add(new StringContent(command.SeoData.Schema), "SeoData.Schema");
            //
            var specifications = JsonConvert.SerializeObject(command.Specifications);
            formData.Add(new StringContent(specifications, Encoding.UTF8, "application/json"), "Specifications");

            var result = await _client.PostAsync("Product", formData);
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        }

        public async Task<ApiResult> DeleteProduct(RemoveProductCommand command)
        {
            var json = JsonConvert.SerializeObject(command);
            var message = new HttpRequestMessage(HttpMethod.Delete, "Product/Image")
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
            var result = await _client.SendAsync(message);
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        }

        public async Task<ApiResult> EditProduct(EditProductCommand command)
        {
            var formData = new MultipartFormDataContent();

            formData.Add(new StringContent(command.Slug), "Slug");
            formData.Add(new StringContent(command.ProductId.ToString()), "ProductId");
            if (command.ImageFile != null)
                formData.Add(new StreamContent(command.ImageFile.OpenReadStream()), "ImageFile", command.ImageFile.FileName);
            formData.Add(new StringContent(command.Title), "Title");
            formData.Add(new StringContent(command.Description), "Description");
            formData.Add(new StringContent(command.CategoryId.ToString()), "CategoryId");
            formData.Add(new StringContent(command.SubCategoryId.ToString()), "SubCategoryId");
            formData.Add(new StringContent(command.SecondarySubCategoryId.ToString() ?? string.Empty), "SecondarySubCategory");
            formData.Add(new StringContent(command.SeoData.MetaTitle), "SeoData.MetaTitle");
            formData.Add(new StringContent(command.SeoData.Canonical), "SeoData.Canonical");
            formData.Add(new StringContent(command.SeoData.MetaKeyWords), "SeoData.MetaKeyWords");
            formData.Add(new StringContent(command.SeoData.MetaDescription), "SeoData.MetaDescription ");
            formData.Add(new StringContent(command.SeoData.IndexPage.ToString()), "SeoData.IndexPage");
            formData.Add(new StringContent(command.SeoData.Schema), "SeoData.Schema");

            var specifications = JsonConvert.SerializeObject(command.Specifications);
            formData.Add(new StringContent(specifications, Encoding.UTF8, "application/json"), "Specifications");

            var result = await _client.PutAsync("Product", formData);
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        }

        public async Task<ProductFilterResult> GetProductByFilter(ProductFilterParams filterParams)
        {
            //  var url = $"Product?pageId={filterParams.PageId}&take={filterParams.Take}" +
            //$"&slug={filterParams.Slug}&title={filterParams.Title}";
            var url ="Product?";
            if (filterParams.PageId != null)
                 url += $"pageId={filterParams.PageId}";

            if (filterParams.Take!= null)
                 url += $"&take={filterParams.Take}";

            if (filterParams.Slug != null)
                 url += $"&slug={filterParams.Slug}";

            if (filterParams.Title != null)
                 url += $"&title={filterParams.Title}";

            var result = await _client.GetFromJsonAsync<ApiResult<ProductFilterResult>>(url);
            return result?.Data;
        }

        public async Task<ProductDto?> GetProductById(long productId)
        {
            var result = await _client.GetFromJsonAsync<ApiResult<ProductDto>>($"Product/ById/{productId}");
            return result?.Data;
        }

        public async Task<ProductDto?> GetProductBySlug(string slug)
        {
            var result = await _client.GetFromJsonAsync<ApiResult<ProductDto>>($"Product/{slug}");
            return result?.Data;
        }

        public async Task<ProductShopResult> GetShopProduct(ProductShopFilterParam filterParam)
        {
            var url = $"Product?pageId={filterParam.PageId}&take={filterParam.Take}" +
                 $"&categorySlug={filterParam.CategorySlug}&onlyAvailableProducts={filterParam.OnlyAvailableProducts}" +
                 $"&search={filterParam.Search}&SearchOrderBy={filterParam.SearchOrderBy}";

            if (filterParam.JustHasDiscount != null)
                url += $"&JustHasDiscount={filterParam.JustHasDiscount}";

            var result = await _client.GetFromJsonAsync<ApiResult<ProductShopResult>>(url);
            return result?.Data;
        }
    }
}
