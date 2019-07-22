using System;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Haushaltsbuch.UI.Web.TagHelpers
{
    [HtmlTargetElement(tag: "currency")]
    public class CurrencyTagHelper : TagHelper
    {
        public string CurrencyName { get; set; }

        public string FallbackSymbol { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "i";
            if (!new[] { "US-Dollar", "EUR" }.Contains(value: CurrencyName))
            {
                output.Content.SetContent(unencoded: FallbackSymbol);
            }
            else
            {
                output.AddClass(classValue: "fas", htmlEncoder: HtmlEncoder.Default);

                switch (CurrencyName)
                {
                    case "US-Dollar":
                        output.AddClass(classValue: "fa-dollar-sign", htmlEncoder: HtmlEncoder.Default);
                        break;
                    case "EUR":
                        output.AddClass(classValue: "fa-euro-sign", htmlEncoder: HtmlEncoder.Default);
                        break;
                }
            }
        }
    }
}