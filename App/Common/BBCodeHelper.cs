using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Gleisbelegung.App.Common
{
    public static class BBCodeHelper
    {
        public static string ConvertMarkdownToBBCode(this string value)
        {
            var r = new Regex("https://.*/(.*)");
            var matches = r.Matches(value);

            foreach (Match match in matches)
            {
                var urlValue = match.Value;
                var newValue = $"[url={urlValue}]#{match.Groups[1].Value}[/url]";
                value = value.Replace(urlValue, newValue);
            }

            return value;
        }
    }
}