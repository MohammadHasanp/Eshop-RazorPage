using Eshop_RazorPage.Models;
using Eshop_RazorPage.Models.Seller;

namespace Eshop_RazorPage.Services.Seller.Inventory
{
    public interface ISellerInventoryService
    {
        Task<ApiResult> AddSellerInventory(AddSellerInventoryCommand command);
        Task<ApiResult> EditSellerInventory(EditSellerInventoryCommand command);


        Task<List<InventoryDto>> GetAllSellerInventory();
        Task<InventoryDto?> GetSellerInventoryById(long inventoryId);
    }
}
