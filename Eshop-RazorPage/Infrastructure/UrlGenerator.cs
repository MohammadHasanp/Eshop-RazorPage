using Eshop_RazorPage.Models;

namespace Eshop_RazorPage.Infrastructure
{
    public static class UrlGenerator
    {
        public static string GenerateBaseFilterUrl(this BaseFilterParam filterParam, string moduleName)
        {
            return $"{moduleName}?pageId={filterParam.PageId}&take={filterParam.Take}";
        }
    }
}
