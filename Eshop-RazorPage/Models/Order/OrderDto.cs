using Eshop_RazorPage.Models;

namespace Eshop_RazorPage.Models.Order
{
    public class OrderDto : BaseDto
    {
        public long UserId { get; set; }
        public string UserFullName { get; set; }
        public OrderStatus Status { get; set; }
        public OrderDiscount? Discount { get; set; }
        public OrderAddress? Address { get; set; }
        public OrderShippingMethod? ShippingMethod { get; set; }
        public List<OrderItemDto> Items { get; set; }
        public DateTime? LastUpdate { get; set; }
        public int TotalPrice{ get; set; }
    }
























}
