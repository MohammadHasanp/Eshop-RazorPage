using Eshop_RazorPage.Models;

namespace Eshop_RazorPage.Models.Banner
{
    public class BannerDto : BaseDto
    {
        public string Link { get; set; }
        public BannerPosition Position { get; set; }
        public string ImageName { get; set; }

    }
    public enum BannerPosition
    {
        زیر_اسلایدر,
        سمت_چپ_اسلایدر,
        بالای_اسلایدر,
        سمت_راست_شگفت_انگیز,
        وسط_صفحه
    }
}
