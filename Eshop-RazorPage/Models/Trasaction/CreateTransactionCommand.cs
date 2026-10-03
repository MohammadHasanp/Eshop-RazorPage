namespace Eshop_RazorPage.Models.Trasaction
{
    public class CreateTransactionCommand
    {
        public long  OrderId { get; set; }
        public string successCallBackUrl { get; set; }
        public string errorCallBackUrl { get; set; }
    }
}
