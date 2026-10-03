using Eshop_RazorPage.Services.Shop;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;

namespace Eshop_RazorPage.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMemoryCache _memoryCache;
        private readonly IShopServices _service;

        public IndexModel(ILogger<IndexModel> logger, IMemoryCache memoryCache, IShopServices service)
        {
            _logger = logger;
            _memoryCache = memoryCache;
            _service = service;
        }
        public MainPageDto? MainPageData{ get; set; }
        public async Task OnGet()
        {
            //MainPageData = await _memoryCache.GetOrCreateAsync("main-page", (entry) =>
            //{
            //    entry.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(15);
            //    entry.SlidingExpiration = TimeSpan.FromMinutes(5);
            //    return _service.GetMainPageData();
            //});
            MainPageData = await _service.GetMainPageData();
        }
    }
}
