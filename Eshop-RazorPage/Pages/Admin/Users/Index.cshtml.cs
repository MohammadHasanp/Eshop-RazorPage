using Eshop_RazorPage.Infrastructure.RazorUtil;
using Eshop_RazorPage.Models.User;
using Eshop_RazorPage.Services.User;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Eshop_RazorPage.Pages.Admin.Users
{
    public class IndexModel : BaseRazorFilter<UserFilterParams>
    {
        private readonly IUserServices _service;

        public IndexModel(IUserServices service)
        {
            _service = service;
        }

        public UserFilterResult UserFilterResult{ get; set; }
        public async Task OnGet()
        {
            UserFilterResult = await _service.GetUserByFilter(FilterParams);
        }
    }
}
