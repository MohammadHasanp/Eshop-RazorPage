using Eshop_RazorPage.Models;
using Eshop_RazorPage.Models.Order;
using Eshop_RazorPage.Services.Product;
using Eshop_RazorPage.Services.Seller.Inventory;
using System.Threading.Tasks;

namespace Eshop_RazorPage.Infrastructure.CookieUtil
{
    public class ShopCartCo cookieManager,
        ISellerInventoryService service,
        IProductServices productService)
    {
        private readonly ICookieManager _cookieManager = cookieManager;
        private const string CookieShopCartName = "Shop-Cart";

        public OrderDto? GetShopCart()
        {
            return _cookieManager.Get<OrderDto>(CookieShopCartName);
        }
        public void DeleteShopCart()
        {
            _cookieManager.Remove(CookieShopCartName);
        }
        public async Task<ApiResult> DeleteItem(long itemId)
        {
            var shopCart = GetShopCart();
            if (shopCart == null)
                return ApiResult.Error();

            var item = shopCart.Items.FirstOrDefault(f => f.Id == itemId);
            if (item == null)
                return ApiResult.Error();

            shopCart.Items.Remove(item);
            SetCookie(shopCart);
            return ApiResult.Success();
        }
        public async Task<ApiResult> AddItem(long inventoryId, int count)
        {
            var shopCart = GetShopCart();
            var inventory = await service.GetSellerInventoryById(inventoryId);
            if (inventory == null)
                return ApiResult.Error();

            var product = await productService.GetProductById(inventory!.productId);

            if (shopCart == null)
            {
                var order = new OrderDto()
                {
                    Address = null,
                    CreationDate = DateTime.Now,
                    Discount = null,
                    Id = 1,
                    Status = OrderStatus.Pennding,
                    UserId = 1,
                    UserFullName = "",
                    Items = new List<OrderItemDto>()
                    {
                       new OrderItemDto()
                       {
                        Id = GenerateId(),
                        Count = count,
                        CreationDate = DateTime.Now,
                        InventoryId = inventoryId,
                        OrderId = 1,
                        Price = inventory.price,
                        ShopName = inventory.shopName,
                        ProductImageName = inventory.productImage,
                        ProductTitle = inventory.productTitle,
                        ProductSlug = product!.Slug,
                       }
                    }
                };
                SetCookie(order);
                return ApiResult.Success();
            }
            else
            {
                if (shopCart.Items.Any(i => i.InventoryId == inventoryId))
                {
                    var item = shopCart.Items.First(i => i.InventoryId == inventoryId);
                    if (inventory.count >= item.Count + count)
                    {
                        item.Count += count;
                    }
                    else
                    {
                        return ApiResult.Error("تعداد محصول کمتر از مقدار درخواستی شما است");
                    }
                }
                else
                {
                    var order = new OrderItemDto()
                    {
                        Count = count,
                        CreationDate = DateTime.Now,
                        Id = GenerateId(),
                        InventoryId = inventoryId,
                        OrderId = 1,
                        Price = inventory.price,
                        ProductImageName = inventory.productImage,
                        ProductSlug = product!.Slug,
                        ProductTitle = inventory.productTitle,
                        ShopName = inventory.shopName,
                    };
                    shopCart.Items.Add(order);
                }
                SetCookie(shopCart);
                return ApiResult.Success();
            }
        }

        public async Task<ApiResult> Increase(long itemId)
        {
            var shopCart = GetShopCart();
            if (shopCart == null)
                return ApiResult.Error();

            var item = shopCart.Items.FirstOrDefault(i => i.Id == itemId);
            if (item == null)
                return ApiResult.Error();

            item.Count += 1;
            SetCookie(shopCart);
            return ApiResult.Success();
        }
        public async Task<ApiResult> Decrease(long itemId)
        {
            var shopCart = GetShopCart();
            if (shopCart == null)
                return ApiResult.Error();

            var item = shopCart.Items.FirstOrDefault(i => i.Id == itemId);
            if (item == null)
                return ApiResult.Error();

            item.Count -= 1;
            SetCookie(shopCart);
            return ApiResult.Success();
        }

        private long GenerateId()
        {
            var random = new Random();
            var number = random.Next(0, 100000) * 6 ^ 2 + random.Next(6, 100000);
            return number;
        }
        private void SetCookie(OrderDto order)
        {
            _cookieManager.Set(CookieShopCartName, order, new CookieOptions()
            {
                Expires = DateTimeOffset.Now.AddDays(7),
                HttpOnly = true,
                Secure = true,

            });
        }





    }
}
