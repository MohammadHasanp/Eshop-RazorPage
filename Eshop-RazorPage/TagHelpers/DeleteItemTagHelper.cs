using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Diagnostics;

namespace Eshop_RazorPage.TagHelpers
{
    [HtmlTargetElement("delete-item")]
    public class DeleteItemTagHelper:TagHelper
    {
        public string Url { get; set; }
        public string Description { get; set; } = "";
        public string Class { get; set; } = "btn btn-danger btn-sm";
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "button";
            output.Attributes.Add("onClick", $"DeleteItem('{Url}','{Description}')");
            output.Attributes.Add("class", Class);
            base.Process(context, output);
        }
    }
}
