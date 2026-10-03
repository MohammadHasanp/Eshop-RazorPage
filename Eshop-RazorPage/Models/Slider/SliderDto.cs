using Eshop_RazorPage.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eshop_RazorPage.Models.Slider
{
    public class SliderDto:BaseDto
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string ImageName { get; set; }
    }
    public class CreateSliderCommand
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public IFormFile ImageFile { get; set; }
    }
    public class EditSliderCommand
    {
        public long  SliderId { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
