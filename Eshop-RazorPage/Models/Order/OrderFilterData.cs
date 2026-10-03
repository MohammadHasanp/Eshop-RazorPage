using Eshop_RazorPage.Models;
using static Eshop_RazorPage.Models.Order.OrderDto;

namespace Eshop_RazorPage.Models.Order
{
    public class OrderFilterData : BaseDto
    {
        public long UserId { get; set; }
        public string UserFullName { get; set; }
        public OrderStatus Status { get; set; }
        public string? Shire { get; set; }
        public string? City { get; set; }
        public string? ShippingType { get; set; }
        public int TotalPrice { get; set; }
        public int TotalItemCount { get; set; }
    }
}
