using Microsoft.AspNetCore.Components.Web;

namespace Eshop_RazorPage.Models.User
{
    public class SetActiveCommand
    {
        public long UserId { get; set; }
        public bool IsActive{ get; set; }
    }
}
