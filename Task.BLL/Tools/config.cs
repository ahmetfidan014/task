using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task.BLL.Tools
{
    public class config
    {
        #if DEBUG
            public static string url = "http://localhost:55815/";
#else
            public static string url = "http://TaskInveon.com/";
#endif

        public static string imgUrl = url + "public/img/";
    }
}