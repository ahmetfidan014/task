using System.Web.Mvc;

namespace Task.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{url}",
                new { controller = "Home", action = "Welcome", url = UrlParameter.Optional }
            );
        }
    }
}