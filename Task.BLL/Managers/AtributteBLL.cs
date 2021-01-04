using Task.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Task.BLL.Managers
{
    public class AuthControl : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            User user = AuthBLL.GetUser();
            if (user == null)
            {
                filterContext.Result = new RedirectResult("/Admin/Login/");
            }
        }

    }
}