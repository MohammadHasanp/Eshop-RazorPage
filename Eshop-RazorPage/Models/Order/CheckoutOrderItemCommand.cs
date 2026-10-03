namespace Eshop_RazorPage.Models.Order
{
    public class CheckoutOrderItemCommand
    {
        public int userId { get; set; }
        public string shire { get; set; }
        public string city { get; set; }
        public string postalCode { get; set; }
        public string postalAddress { get; set; }
        public string phoneNumber { get; set; }
        public string name { get; set; }
        public string family { get; set; }
        public string nationalCode { get; set; }
    }
}
