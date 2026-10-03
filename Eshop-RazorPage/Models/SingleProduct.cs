using Eshop_RazorPage.Models.Product;
using Eshop_RazorPage.Models.Seller;

namespace Eshop_RazorPage.Models
{
    public class SingleProduct
    {
        public ProductDto Product { get; set; }
        public List<InventoryDto?> Inventories { get; set; } = new();
    }
}
