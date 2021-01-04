using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task.BLL.Managers;

namespace Task.Areas.Admin.Controllers
{
    [AuthControl]
    public class HomeController : Controller
    {
        public ActionResult Welcome()
        {
            return View();
        }
    }
}