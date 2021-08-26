using System.Web.Mvc;

namespace FreeCy.Areas.Admin
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
                "Admin_404",
                "Admin/Login/Login/",
                new { action = "Login", controller = "Login", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "Admin_Feedback",
                "Admin/Feedback/Index/",
                new { action = "Index", controller = "Feedback", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "Admin_Logout",
                "Admin/Login/Logout/",
                new { action = "Logout", controller = "Login", id = UrlParameter.Optional }
            );
            context.MapRoute(
               "Admin_Home",
               "Admin/Home/Index/",
               new { action = "Index", controller = "Home", id = UrlParameter.Optional }
           );
            context.MapRoute(
               "List_User",
               "Admin/Users/Index/",
               new { action = "Index", controller = "Users", id = UrlParameter.Optional }
           );
            context.MapRoute(
               "Delete_User",
               "Admin/Users/Delete/{id}",
               new { action = "Delete", controller = "Users", id = UrlParameter.Optional }
           );
           
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index",controller="Home" ,id = UrlParameter.Optional }
            );
        }
    }
}