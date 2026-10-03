using Eshop_RazorPage.Infrastructure.CookieUtil;
using Eshop_RazorPage.Infrastructure.Util;
using Eshop_RazorPage.Infrastructure.Util.RazorUtil;
using Eshop_RazorPage.Models.Order;
using Eshop_RazorPage.Services.Order;
using Microsoft.AspNetCore.Mvc;

namespace Eshop_RazorPage.Pages
{
    public class ShopCartModel : BaseRazorPage
    {
        private readonly IOrderServices _service;
        private readonly ShopCartCookieManager _cookieManager;
        public ShopCartModel(IOrderServices service, ShopCartCookieManager cookieManager)
        {
            _service = service;
            _cookieManager = cookieManager;
        }
        public OrderDto? Orders { get; set; }

        public async Task OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                Orders = await _service.GetCurrentUserOrder();
            }
            else
            {
                Orders = _cookieManager.GetShopCart();
            }
        }
        public async Task<IActionResult> OnPostDeleteItem(long Id)
        {
            if (User.Identity.IsAuthenticated)
            {
                return await AjaxTryCatch(async () =>
                {
                    var result = await _service.DeleteOrderItem(Id);
                    return result;
                });
            }
            else
            {
                return await AjaxTryCatch(() =>
                {
                    var result = _cookieManager.DeleteItem(Id);
                    return result;
                });
            }
        }
        public async Task<IActionResult> OnPostAddItem(long inventoryId, int count)
        {
            if (User.Identity.IsAuthenticated)
            {
                return await AjaxTryCatch(async () =>
                {
                    return await _service.AddOrderItem(new AddOrderItemCommand()
                    {
                        userId = User.GetUserId(),
                        count = count,
                        inventoryId = inventoryId
                    });
                });
            }
            else
            {
                return await AjaxTryCatch(async () =>
                {
                    var result = await _cookieManager.AddItem(inventoryId, count);
                    return result;
                });
            }
        }
        public async Task<IActionResult> OnPostIncreaseItemCount(int id)
        {

            if (User.Identity.IsAuthenticated)
            {
                return await AjaxTryCatch(() =>
                {
                    return _service.IncreasecountOrderItem(new IncreaseCountOrderItem()
                    {
                        Count = 1,
                        ItemID = id,
                        UserId = User.GetUserId(),
                    });
                });
            }
            else
            {
                return await AjaxTryCatch(async () =>
                {
                    var result = await _cookieManager.Increase(id);
                    return result;
                });
            }
        }
        public async Task<IActionResult> OnPostDecreaseItemCount(long id)
        {
            if (User.Identity.IsAuthenticated)
            {
                return await AjaxTryCatch(() =>
                {
                    return _service.DecreasCountOrderItem(new DecreaseCountOrderItem()
                    {
                        Count = 1,
                        ItemID = id,
                        UserId = User.GetUserId(),
                    });
                });
            }
            else
            {
                return await AjaxTryCatch(async () =>
                {
                    var result = await _cookieManager.Decrease(id);
                    return result;
                });
            }
        }

        public async Task<IActionResult> OnGetShopCartDetail()
        {
            OrderDto? order = new();
            if (User.Identity!.IsAuthenticated)
            {
                order = await _service.GetCurrentUserOrder();
            }
            else
            {
                order = _cookieManager.GetShopCart();
            }
            return new ObjectResult(new
            {
                items = order?.Items,
                count = order?.Items.Sum(o => o.Count),
                price = order?.Items.Sum(o => o.TotalPrice)
            });
        }
    }
}
