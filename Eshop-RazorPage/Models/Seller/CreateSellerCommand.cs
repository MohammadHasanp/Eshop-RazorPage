using Eshop_RazorPage.Models;
using System.ComponentModel.DataAnnotations;

namespace Eshop_RazorPage.Models.Seller
{
    public class CreateSellerCommand
    {
        public string ShopName { get; set; }
        public string NationalCode { get; set; }
    }
    public class EditSellerCommand
    {
        public long  Id{ get; set; }
        public string shopName { get; set; }
        public string nationalCode { get; set; }
    }
    public class AddSellerInventoryCommand
    {
        public long sellerId { get; set; }
        public long productId { get; set; }
        public int price { get; set; }
        public int count { get; set; }
        public int DiscountPercentage { get; set; }
    }
    public class EditSellerInventoryCommand
    {
        public long inventoryId { get; set; }
        public long sellerId { get; set; }
        public int price { get; set; }
        public int count { get; set; }
        [Range(0,50,ErrorMessage ="مقدار تخفیف باید بین 0 تا 50 باشد")]
        public int DiscountPercentage { get; set; }
    }
    public class InventoryDto:BaseDto
    {
        public int sellerId { get; set; }
        public string shopName { get; set; }
        public int productId { get; set; }
        public string productTitle { get; set; }
        public string productImage { get; set; }
        public int count { get; set; }
        public int price { get; set; }
        public int DiscountPercentage { get; set; }
    }
}
