using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Task.DAL.Models;

namespace Task.BLL.Managers
{
    public class AuthBLL
    {
        public static User GetUser()
        {
            try
            {
                User CurrUser = (User)HttpContext.Current.Session["User"];
                UserBLL _User = new UserBLL();

                if (CurrUser == null)
                {
                    HttpCookie ck = HttpContext.Current.Request.Cookies["usr"];
                    if (ck != null && !string.IsNullOrEmpty(ck.Value))
                    {
                        int UserID = Convert.ToInt32(ck.Value);
                        User NewUser = _User.GetFirstOrDefault(d => d.Id == UserID);
                        if (NewUser != null)
                            HttpContext.Current.Session["User"] = NewUser;
                        return NewUser;
                    }
                }
                else
                    LoadUser(CurrUser);
                return CurrUser;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static bool LoadUser(User NewUser)
        {
            try
            {
                HttpContext.Current.Session["User"] = NewUser;
                HttpCookie ck = HttpContext.Current.Request.Cookies["usr"];
                if (ck == null)
                    ck = new HttpCookie("usr", NewUser.Id + "");
                if (ck.Expires < DateTime.Now)
                {
                    ck.Value = NewUser.Id + "";
                    ck.Expires = DateTime.Now.AddDays(3);
                    HttpContext.Current.Response.Cookies.Add(ck);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
