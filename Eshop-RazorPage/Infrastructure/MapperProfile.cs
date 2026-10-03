using AutoMapper;
using Eshop_RazorPage.Models.UserAddress;
using Eshop_RazorPage.ViewModel.Users;

namespace Eshop_RazorPage.Infrastructure
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<AddressDto,EditUserAddressViewModel>().ReverseMap();
            CreateMap<EditUserAddressViewModel, EditUserAddressCommand>().ReverseMap();
        }
    }
}
