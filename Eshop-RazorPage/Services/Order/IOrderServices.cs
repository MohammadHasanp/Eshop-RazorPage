using Eshop_RazorPage.Models;
using Eshop_RazorPage.Models.Order;

namespace Eshop_RazorPage.Services.Order
{
    public interface IOrderServices
    {
        Task<OrderFilterResult?> GetOrderByFilter(OrderFilterParams filterParams);
        Task<OrderDto?> GetOrderById(long orderId);
        Task<OrderDto?> GetCurrentUserOrder();

        Task<ApiResult> AddOrderItem(AddOrderItemCommand command);
        Task<ApiResult> CheckoutOrderItem(CheckoutOrderItemCommand command);
        Task<ApiResult> DecreasCountOrderItem(DecreaseCountOrderItem item);
        Task<ApiResult> IncreasecountOrderItem(IncreaseCountOrderItem item);
        Task<ApiResult> DeleteOrderItem(long itemId);
    }
}
