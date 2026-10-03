using Eshop_RazorPage.Infrastructure.Util;
using Eshop_RazorPage.Models.Order;
using Eshop_RazorPage.Services.Order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eshop_RazorPage.Pages.Checkout
{
    public class FinallyOrderModel : PageModel
    {
        private readonly IOrderServices _orderService;
        public FinallyOrderModel(IOrderServices orderService)
        {
            _orderService = orderService;
        }

        public OrderDto Order { get; set; }
        public async Task<IActionResult> OnGet(long orderId)
        {
            var order = await _orderService.GetOrderById(orderId);
            if(order == null || order.UserId != User.GetUserId())
            {
                return Redirect("/");
            }

            Order = order;
            return Page();
        }
    }
}
