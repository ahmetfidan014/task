using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Task.BLL.Tools
{
    public class controlfiles
    {
        public static void directory(string path)
        {
            if (!Directory.Exists(HttpContext.Current.Server.MapPath(path)))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(path));
            }
        }
        
    }
}