using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Task.BLL.Managers;
using Task.BLL.Tools;
using Task.DAL;
using Task.DAL.Models;

namespace Task.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private readonly IRepository<User> _user;
        public LoginController(IRepository<User> _User)
        {
            _user = _User;
        }
        public ActionResult Welcome()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public JsonResult login(string email, string pwd)
        {
            var response = Request["g-recaptcha-response"];
            if (!validateCaptcha.validateC(response))
            {
                return Json(responseBLL.create(false, "Captca Hatası", null), JsonRequestBehavior.AllowGet);
            }
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pwd))
            {
                ViewBag.Mesaj = "Lütfen gerekli alanları doldurunuz.";
                return Json(responseBLL.create(false, "Lütfen gerekli alanları doldurunuz.", null), JsonRequestBehavior.AllowGet);
            }
            else
            {
                string Sifre = hashwithsha.ComputeHash(pwd, "SHA512", Encoding.ASCII.GetBytes(pwd));
                var control = _user.GetFirstOrDefault(g => g.Password == Sifre && g.Email == email && g.Flag == status.active);
                if (control == null)
                {
                    ViewBag.Mesaj = "Kullanıcı bilgilerine ulaşılamadı.";
                    return Json(responseBLL.create(false, "Kullanıcı bilgilerine ulaşılamadı.", null), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    AuthBLL.LoadUser(control);
                    return Json(responseBLL.create(true, "Giriş yapıldı.", null), JsonRequestBehavior.AllowGet);
                }
            }
        }
        public ActionResult logout()
        {
            User login = AuthBLL.GetUser();
            if (login != null)
            {
                Session.Abandon();
                HttpCookie ck = Response.Cookies["usr"];
                if (ck != null)
                    ck.Expires = DateTime.Now.AddDays(-1);

                Session["GuvenlikKodu"] = null;
                Session["kullanici"] = null;
            }
            return RedirectToAction("Welcome", "Login", new { area = "admin" });
        }
    }
}