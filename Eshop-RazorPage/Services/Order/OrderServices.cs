using Eshop_RazorPage.Infrastructure;
using Eshop_RazorPage.Models;
using Eshop_RazorPage.Models.Order;

namespace Eshop_RazorPage.Services.Order
{
    public class OrderServices: IOrderServices
    {
        private readonly HttpClient _client;
        public OrderServices(HttpClient client)
        {
            _client = client;
        }

        public async Task<ApiResult> AddOrderItem(AddOrderItemCommand command)
        {
            var result = await _client.PostAsJsonAsync("Order",command);
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        }

        public async Task<ApiResult> CheckoutOrderItem(CheckoutOrderItemCommand command)
        {
            var result = await _client.PutAsJsonAsync("Order/Checkout",command);
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        }

        public async Task<ApiResult> DecreasCountOrderItem(DecreaseCountOrderItem item)
        {
            var result = await _client.PutAsJsonAsync("Order/OrderItem/DecreaseCount",item);
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        }

        public async Task<ApiResult> DeleteOrderItem(long itemId)
        {
            var result = await _client.DeleteAsync($"Order/OrderItem{itemId}");
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        }

        public async Task<OrderDto?> GetCurrentUserOrder()
        {
            var result = await _client.GetFromJsonAsync<ApiResult<OrderDto>>("Order/Current");
            return result?.Data;
        }

        public async Task<OrderFilterResult?> GetOrderByFilter(OrderFilterParams filterParams)
        {
             var url = filterParams.GenerateBaseFilterUrl("Order");
            if (filterParams.UserId != null)
                url += $"&UserId={filterParams.UserId}";

            if (filterParams.CommentStatus != null)
                url += $"&CommentStatus={filterParams.CommentStatus}";

            if (filterParams.StartDate != null)
                url += $"&StartDate{filterParams.StartDate}";

            if (filterParams.EndDate != null)
                url += $"&EndDate{filterParams.EndDate}";
            var result = await _client.GetFromJsonAsync<ApiResult<OrderFilterResult>>(url);
            return result?.Data;
        }

        public async Task<OrderDto?> GetOrderById(long orderId)
        {
            var result = await _client.GetFromJsonAsync<ApiResult<OrderDto>>($"Order/{orderId}");
            return result?.Data;
        }

        public async Task<ApiResult> IncreasecountOrderItem(IncreaseCountOrderItem item)
        {
            var result = await _client.PutAsJsonAsync("Order/OrderItem/IncreaseCount", item);
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        }
    }
}
