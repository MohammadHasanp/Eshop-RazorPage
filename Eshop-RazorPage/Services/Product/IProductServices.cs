using Eshop_RazorPage.Models;
using Eshop_RazorPage.Models.Product;

namespace Eshop_RazorPage.Services.Product
{
    public interface IProductServices
    {
        Task<ApiResult> CreateProduct(CreateProductCommand command);
        Task<ApiResult> EditProduct(EditProductCommand command);
        Task<ApiResult> DeleteProduct(RemoveProductCommand command);
        Task<ApiResult> AddImageProduct(AddImageProductCommand command);


        Task<ProductFilterResult> GetProductByFilter(ProductFilterParams filterParams);
        Task<ProductShopResult?> GetShopProduct(ProductShopFilterParam filterParam);
        Task<ProductDto?> GetProductById(long productId);
        Task<ProductDto?> GetProductBySlug(string slug);
    }
}
