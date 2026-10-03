using Eshop_RazorPage.Infrastructure.RazorUtil;
using Eshop_RazorPage.Models.Seller;
using Eshop_RazorPage.Models.User;
using Eshop_RazorPage.Services.Seller;
using Eshop_RazorPage.Services.User;
using Eshop_RazorPage.ViewModel.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Eshop_RazorPage.Pages.Admin.UserRoles
{
    
    public class IndexModel : BaseRazorPage
    {
        private readonly IUserServices _service;
        public IndexModel(IUserServices service)
        {
            _service = service;
        }

        //public List<string> Phones { get; set; }
        //public List<long> Ids { get; set; }
        //public List<UserRoleDto> UserRoles { get; set; }

        public List<UserDto> users { get; set; }

        public async Task OnGet()
        {
            users = await _service.GetAllUser();







            //var users = await _service.GetAllUser();

            //Phones = new List<string>();
            //var id = new List<long>();
            //var userRole = new List<UserRoleDto>();

            //users.ForEach(u =>
            //{
            //    UserRoles.AddRange(u.roles);
            //    Ids.Add(u.Id);
            //    Phones.Add(u.phoneNumber);
            //});

        }
    }
}
