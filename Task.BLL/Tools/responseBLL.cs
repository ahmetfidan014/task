using Task.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task.BLL.Tools
{
    public class responseBLL
    {
        public static Response create(bool result, string message, object obj)
        {
            Response response = new Response
            {
                result = result,
                message = message,
                obj = obj
            };
            return response;
        }
    }
}