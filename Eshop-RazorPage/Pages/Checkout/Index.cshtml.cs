using Eshop_RazorPage.Infrastructure.Util;
using Eshop_RazorPage.Infrastructure.Util.RazorUtil;
using Eshop_RazorPage.Models.Order;
using Eshop_RazorPage.Models.ShippingMethod;
using Eshop_RazorPage.Models.Trasaction;
using Eshop_RazorPage.Models.UserAddress;
using Eshop_RazorPage.Services.Order;
using Eshop_RazorPage.Services.ShippingMethods;
using Eshop_RazorPage.Services.Transaction;
using Eshop_RazorPage.Services.UserAddress;
using Microsoft.AspNetCore.Mvc;

namespace Eshop_RazorPage.Pages.Checkout
{
    public class IndexModel : BaseRazorPage
    {
        private readonly IOrderServices _orderServices;
        private readonly IUserAddressservices _userAddress;
        private readonly IShippingMethodService _shippingMethodService;
        private readonly ITransactionService _transactionService;
        public IndexModel(IOrderServices orderServices, IUserAddressservices userAddress, IShippingMethodService shippingMethodService, ITransactionService transactionService)
        {
            _orderServices = orderServices;
            _userAddress = userAddress;
            _shippingMethodService = shippingMethodService;
            _transactionService = transactionService;
        }
        public List<AddressDto> Addresses { get; set; }
        public OrderDto Order { get; set; }
        public List<ShippingMethodDto> ShippingMethods { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var order = await _orderServices.GetCurrentUserOrder();
            if (order == null)
                return RedirectToPage("../Index");

            Order = order;
            Addresses = await _userAddress.GetAllUserAddress();
            ShippingMethods = await _shippingMethodService.GetAllShippingMethod();
            if (ShippingMethods.Any() == false)
                return RedirectToPage("../Index");

            return Page();
        }


        public async Task<IActionResult> OnPost(long shippingMethodId)
        {
            var address = await _userAddress.GetAllUserAddress();
            var currentAddress = address.FirstOrDefault(a => a.IsActive);
            if (currentAddress == null)
                return RedirectToPage("Index");

            var result = await _orderServices.CheckoutOrderItem(new CheckoutOrderItemCommand()
            {
                city = currentAddress.City,
                family = currentAddress.Family,
                name = currentAddress.Name,
                nationalCode = currentAddress.NationalCode,
                phoneNumber = currentAddress.PhoneNumber,
                postalAddress = currentAddress.PostalAddress,
                postalCode = currentAddress.NationalCode,
                shire = currentAddress.Shire,
                userId = User.GetUserId(),
                ShippingMethodId = shippingMethodId
            });
            if (result.IsSuccess)
            {
                var currentOrder = await _orderServices.GetCurrentUserOrder();
                var res = await _transactionService.CreateTRansaction(new CreateTransactionCommand()
                {
                    OrderId = currentOrder!.Id,

                    errorCallBackUrl =
                       $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/checkout/finallyOrder/{currentOrder.Id}",

                    successCallBackUrl =
                    $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/Checkout/finallyOrder/{currentOrder.Id}",
                });
                if (res.IsSuccess)
                {
                    return Redirect(res.Data);
                }
            }
            ErrorAlert(result.MetaData.Message);
            return RedirectToPage("Index");
        }
    }
}
