using Eshop_RazorPage.Models;
using static Eshop_RazorPage.Models.Order.OrderDto;

namespace Eshop_RazorPage.Models.Order
{
    public class OrderFilterParams : BaseFilterParam
    {
        public long? UserId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public OrderStatus? CommentStatus { get; set; }
    }
}
