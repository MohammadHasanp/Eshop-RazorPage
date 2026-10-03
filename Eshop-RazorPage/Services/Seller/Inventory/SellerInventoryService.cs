using Eshop_RazorPage.Models;
using Eshop_RazorPage.Models.Seller;

namespace Eshop_RazorPage.Services.Seller.Inventory
{
    public class SellerInventoryService : ISellerInventoryService
    {
        private readonly HttpClient _client;
        public SellerInventoryService(HttpClient client)
        {
            _client = client;
        }

        public async Task<ApiResult> AddSellerInventory(AddSellerInventoryCommand command)
        {
            var result = await _client.PostAsJsonAsync("Seller/SellerInvantory", command);
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        }

        public async Task<ApiResult> EditSellerInventory(EditSellerInventoryCommand command)
        {
            var result = await _client.PutAsJsonAsync("Seller/SellerInvantory", command);
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        }

        public async Task<List<InventoryDto>?> GetAllSellerInventory()
        {
            var result = await _client.GetFromJsonAsync <ApiResult<List<InventoryDto>>>("Seller/Inventory");
            return result?.Data;
        }

        public async Task<InventoryDto?> GetSellerInventoryById(long inventoryId)
        {
            var result = await _client.GetFromJsonAsync<ApiResult<InventoryDto>>($"Seller/Inventory/{inventoryId}");
            return result?.Data;
        }
    }
}
