using Eshop_RazorPage.Models.Order;
using Eshop_RazorPage.Services.Order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Eshop_RazorPage.Pages.Profile.Orders;

public class IndexModel : PageModel
{
    private readonly IOrderServices _orderService;

    public IndexModel(IOrderServices orderService)
    {
        _orderService = orderService;
    }

    public OrderFilterResult FilterResult { get; set; }
    public async Task OnGet(int pageId = 1, OrderStatus status = OrderStatus.None)
    {
        FilterResult =await _orderService.GetUserOrders(pageId, 10, status);
    }
}
