using Eshop_RazorPage.Infrastructure.RazorUtil;
using Eshop_RazorPage.Models;
using Eshop_RazorPage.Models.Seller;
using Eshop_RazorPage.Services.Seller;
using Eshop_RazorPage.Services.Seller.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eshop_RazorPage.Pages.SellerPanel.Inventories
{
    public class IndexModel : BaseRazorPage
    {
        private readonly ISellerInventoryService _service;
        private readonly ISellerServices _sellerServices;
        private readonly IRenderViewToString _render;

        public IndexModel(ISellerInventoryService service, ISellerServices sellerServices, IRenderViewToString render)
        {
            _service = service;
            _sellerServices = sellerServices;
            _render = render;
        }
        public List<InventoryDto> Inventories { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var seller = await _sellerServices.GetCurrentUserSeller();
            if (seller == null)
                return Redirect("/");

            Inventories = await _service.GetAllSellerInventory();
            return Page();
        }

        public async Task<IActionResult> OnGetEditPage(long id)
        {
            var inventory = await _service.GetSellerInventoryById(id);
            if (inventory == null)
                return null;

            return await AjaxTryCatch(async () =>
            {
                var model = new EditSellerInventoryCommand()
                {
                    count = inventory.count,
                    DiscountPercentage = inventory.DiscountPercentage,
                    inventoryId = id,
                    price = inventory.price,
                    sellerId = inventory.sellerId
                };
                var view = await _render.RenderToStringAsync("_Edit",model,PageContext);
                return ApiResult<string>.Success(view);
            });
        }
        public async Task<IActionResult> OnPostEditSellerInventory(EditSellerInventoryCommand command)
        {
            return await AjaxTryCatch(async () =>
            {
                var result = await _service.EditSellerInventory(command);
                return result;
            });
        }
    }
}
