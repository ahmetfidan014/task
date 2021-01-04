using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Task.BLL.Tools
{
    public class stringformatter
    {
        public static string OnlyEnglishChar(string item)
        {
            if (item != null)
            {
                string result = item;
                Encoding iso = Encoding.GetEncoding("Cyrillic");
                Encoding utf8 = Encoding.UTF8;
                byte[] utfBytes = utf8.GetBytes(result);
                byte[] isoBytes = Encoding.Convert(utf8, iso, utfBytes);
                string msg = iso.GetString(isoBytes);
                result = msg;
                
                // make it all lower case
                result = result.ToLower();
                // remove entities
                result = Regex.Replace(result, @"&\w+;", "");
                // remove anything that is not letters, numbers, dash, or space
                result = Regex.Replace(result, @"[^a-z0-9\-\s]", "");
                // replace spaces
                result = result.Replace(' ', '-');
                // collapse dashes
                result = Regex.Replace(result, @"-{2,}", "-");
                // trim excessive dashes at the beginning
                result = result.TrimStart(new[] { '-' });
                // if it's too long, clip it
                if (result.Length > 80)
                    result = result.Substring(0, 79);
                // remove trailing dashes
                result = result.TrimEnd(new[] { '-' });

                //result = replaced;

                return result;
            }
            else
            {
                return "";
            }
            
        }
    }
}