using System.Reflection.PortableExecutable;

namespace Eshop_RazorPage.Models.ShippingMethod
{
    public class CreateShippingMethodCommand
    {
        public string Title { get; set; }
        public int Cost { get; set; }
    }
    public class EditShippingMethodCommand
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public int Cost { get; set; }
    }
}
