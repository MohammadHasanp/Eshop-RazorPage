namespace Eshop_RazorPage.Models.Order
{
    public class AddOrderItemCommand
    {
        public long inventoryId { get; set; }
        public int count { get; set; }
        public long userId { get; set; }
    }
}
