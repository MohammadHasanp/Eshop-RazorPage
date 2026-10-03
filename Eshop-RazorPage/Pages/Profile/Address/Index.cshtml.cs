using AutoMapper;
using Common.Application;
using Eshop_RazorPage.Infrastructure.RazorUtil;
using Eshop_RazorPage.Models;
using Eshop_RazorPage.Models.UserAddress;
using Eshop_RazorPage.Services.UserAddress;
using Eshop_RazorPage.ViewModel.Users;
using Microsoft.AspNetCore.Mvc;

namespace Eshop_RazorPage.Pages.Profile.Address
{
    public class IndexModel : BaseRazorPage
    {
        private readonly IUserAddressservices _service;
        private readonly IRenderViewToString _renderView;
        private readonly IMapper _mapper;
        public IndexModel(IUserAddressservices service, IRenderViewToString renderView, IMapper mapper)
        {
            _service = service;
            _renderView = renderView;
            _mapper = mapper;
        }

        public List<AddressDto> Addresses{ get; set; }

        public async Task OnGet()
        {
            Addresses = await _service.GetAllUserAddress();
        }
        public async Task<IActionResult> OnGetShowAddPage()
        {
            return await AjaxTryCatch(async () =>
            {
                var view = await _renderView.RenderToStringAsync("_Add",new AddUserAddressViewModel(),PageContext);
                return ApiResult<string>.Success(view);
            });
        }
        public async Task<IActionResult> OnGetShowEditPage(long addressId)
        {
         
            return await AjaxTryCatch(async () =>
            {
                var address = await _service.GetUserAddressById(addressId);
                var model = _mapper.Map<EditUserAddressViewModel>(address);
                var view = await _renderView.RenderToStringAsync("_Edit", model, PageContext);
                return ApiResult<string>.Success(view);
            });
        }
        public async Task<IActionResult> OnPostAddAddress(AddUserAddressViewModel viewModel)
        {
            return await AjaxTryCatch(async () =>
            {
                var model = new AddUserAddressCommand()
                {
                    city = viewModel.City,
                    shire = viewModel.Shire,
                    family = viewModel.Family,
                    name = viewModel.Name,
                    nationalCode = viewModel.NationalCode,
                    postalAddress = viewModel.PostalAddress,
                    postalCode = viewModel.PostalCode,
                    phone = viewModel.PhoneNumber,
                };
                var result = await _service.AddUserAddress(model);
                return result;
            },true);
        }

        public async Task<IActionResult> OnPostEditAddress(EditUserAddressViewModel viewModel)
        {
            return await AjaxTryCatch(async () =>
            {
                var model = _mapper.Map<EditUserAddressCommand>(viewModel);
                var result = await _service.EditUserAddress(model);
                return result;
            });
        }
        public async Task<IActionResult> OnPost(long addressId)
        {
            var result = await _service.DeleteUserAddress(addressId);
            return RedirectAndShowAlert(result,RedirectToPage("Index"));
        }
        public async Task<IActionResult> OnGetActivateAddress(long addressId)
        {
            return await AjaxTryCatch(async () =>
            {
                var result = await _service.ActivateAddress(addressId);
                return result;
            },true);
        }
    }
}
