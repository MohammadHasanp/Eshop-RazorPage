using Eshop_RazorPage.Infrastructure.Util;
using Eshop_RazorPage.Models.Order;
using Eshop_RazorPage.Services.Order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eshop_RazorPage.Pages.Profile.Orders;

public class ShowModel(IOrderServices orderServices) : PageModel
{
    private readonly IOrderServices _orderServices = orderServices;

    public OrderDto Order { get; set; }
    public async Task<IActionResult> OnGet(long orderId)
    {
        var order = await _orderServices.GetOrderById(orderId);
        if (order == null || order.UserId != User.GetUserId())
            return RedirectToPage("/");

        Order = order;
        return Page();
    }
}
