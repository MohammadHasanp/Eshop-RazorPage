using Eshop_RazorPage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Serialization;

namespace Eshop_RazorPage.Infrastructure.Util.RazorUtil
{
    public class BaseRazorFilter<TFilterParam>:PageModel where TFilterParam : BaseFilterParam
    {
        [BindProperty(SupportsGet =true)]
        public TFilterParam FilterParams { get; set; }
    }
}
