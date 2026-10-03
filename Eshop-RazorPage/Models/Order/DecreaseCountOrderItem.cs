namespace Eshop_RazorPage.Models.Order
{
    public class DecreaseCountOrderItem
    {
        public int ItemID { get; set; }
        public int userId { get; set; }
        public int count { get; set; }
    }
    public class IncreaseCountOrderItem
    {
        public int ItemID { get; set; }
        public int userId { get; set; }
        public int count { get; set; }
    }
}
