using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FreeCy
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
  
            routes.MapRoute(
                name: "JobList",
                url: "Home/ListProduct/",
                defaults: new { controller = "Home", action = "ListProduct" },
                namespaces: new[] { "FreeCy.Controllers" }
            );
            routes.MapRoute(
                name: "Product Detail",
                url: "Detail/ProductDetail/{productID}",
                defaults: new { controller = "Detail", action = "ProductDetail", productID = UrlParameter.Optional }
              
            );
            routes.MapRoute(
                name: "ReceivedOrders",
                url: "order-da-nhan/",
                defaults: new { controller = "Order", action = "ReceivedOrders" },
                namespaces: new[] { "FreeCy.Controllers" }
            );
            routes.MapRoute(
                name: "SetQuantity",
                url: "Cart/SetQuantity/{qty}",
                defaults: new { controller = "Cart", action = "SetQuantity", qty = UrlParameter.Optional },
                namespaces: new[] { "FreeCy.Controllers" }
            );
            routes.MapRoute(
                name: "Add-to-cart",
                url: "Cart/AddItem/{product_id}",
                defaults: new { controller = "Cart", action = "AddItem", product_id = UrlParameter.Optional }

            );
            routes.MapRoute(
                name: "Product2 Detail",
                url: "Product/Detail/{productID}",
                defaults: new { controller = "Product", action = "Detail", productID = UrlParameter.Optional }

            );
           
            routes.MapRoute(
               name: "Search",
               url: "Search/Index/",
               defaults: new { controller = "Search", action = "Index" },
               namespaces: new[] { "FreeCy.Controllers" }
           );
            routes.MapRoute(
               name: "Create Job",
               url: "Product/Create/",
               defaults: new { controller = "Product", action = "Create" },
               namespaces: new[] { "FreeCy.Controllers" }
           );
            routes.MapRoute(
                name: "ListProductByCategory",
                url: "Home/ListProductByCategory/{cateId}",
                defaults: new { controller = "Home", action = "ListProductByCategory", cateId = UrlParameter.Optional },
                namespaces: new[] { "FreeCy.Controllers" }
            );
            routes.MapRoute(
               name: "Logout",
               url: "User/Logout/",
               defaults: new { controller = "User", action = "Logout" },
               namespaces: new[] { "FreeCy.Controllers" }
           );
            routes.MapRoute(
                name: "JobDetail",
                url: "Home/Detail/{productID}",
                defaults: new { controller = "Home", action = "Detail", productID = UrlParameter.Optional },
                namespaces: new[] { "FreeCy.Controllers" }
            );
            routes.MapRoute(
                name: "Edit Job",
                url: "Product/Edit/{id}",
                defaults: new { controller = "Product", action = "Edit", id = UrlParameter.Optional },
                namespaces: new[] { "FreeCy.Controllers" }
            );
            routes.MapRoute(
                 name: "Login",
                 url: "User/Login/",
                 defaults: new { controller = "User", action = "Login" },
                 namespaces: new[] { "FreeCy.Controllers" }
             );
            routes.MapRoute(
                 name: "Register",
                 url: "User/Register/",
                 defaults: new { controller = "User", action = "Register" },
                 namespaces: new[] { "FreeCy.Controllers" }
             );
            routes.MapRoute(
                name: "ForgotPassword",
                url: "User/ForgotPassword/",
                defaults: new { controller = "User", action = "ForgotPassword" },
                namespaces: new[] { "FreeCy.Controllers" }
            );
            routes.MapRoute(
               name: "About",
               url: "About/Index/",
               defaults: new { controller = "About", action = "Index" },
               namespaces: new[] { "FreeCy.Controllers" }
           );
            routes.MapRoute(
              name: "ListUser",
              url: "User/ListUser/",
              defaults: new { controller = "User", action = "ListUser" },
              namespaces: new[] { "FreeCy.Controllers" }
          );
            routes.MapRoute(
              name: "List Product",
              url: "Home/ListProduct2/",
              defaults: new { controller = "Home", action = "ListProduct2" },
              namespaces: new[] { "FreeCy.Controllers" }
          );
            routes.MapRoute(
              name: "Contact",
              url: "Contact/Index/",
              defaults: new { controller = "Contact", action = "Index" },
              namespaces: new[] { "FreeCy.Controllers" }
          );
            routes.MapRoute(
                name: "ChatDetail",
                url: "Chat/Details/{iduser}",
                defaults: new { controller = "Chat", action = "Details", iduser = UrlParameter.Optional },
                namespaces: new[] { "FreeCy.Controllers" }
            );
            routes.MapRoute(
                name: "ListCate",
                url: "Home/ListCategory/partID",
                defaults: new { controller = "Home", action = "ListCategory", partID = UrlParameter.Optional },
                namespaces: new[] { "FreeCy.Controllers" }
            );
            routes.MapRoute(
                name: "ViewProfile",
                url: "User/ViewProfile/{iduser}",
                defaults: new { controller = "User", action = "ViewProfile", iduser = UrlParameter.Optional },
                namespaces: new[] { "FreeCy.Controllers" }
            );
            routes.MapRoute(
                name: "Edit User",
                url: "User/Edit/{iduser}",
                defaults: new { controller = "User", action = "Edit", iduser = UrlParameter.Optional },
                namespaces: new[] { "FreeCy.Controllers" }
            );
            
            routes.MapRoute(
                name: "Edit Profile",
                url: "EditProfile/Edit/{id}",
                defaults: new { controller = "EditProfile", action = "Edit", id = UrlParameter.Optional },
                namespaces: new[] { "FreeCy.Controllers" }
            );
            routes.MapRoute(
               name: "View Candidate",
               url: "User/ViewProfileCandidate/{iduser}",
               defaults: new { controller = "User", action = "ViewProfileCandidate", iduser = UrlParameter.Optional },
               namespaces: new[] { "FreeCy.Controllers" }
           );
            routes.MapRoute(
               name: "Addfile",
               url: "File/UploadFile/{order_id}",
               defaults: new { controller = "File", action = "UploadFile", order_id = UrlParameter.Optional },
               namespaces: new[] { "FreeCy.Controllers" }
           );
            routes.MapRoute(
               name: "Tim kiem",
               url: "Home/Search/",
               defaults: new { controller = "Home", action = "Search", order_id = UrlParameter.Optional },
               namespaces: new[] { "FreeCy.Controllers" }
           );
            routes.MapRoute(
               name: "Thanh toan",
               url: "Order/Payment/",
               defaults: new { controller = "Order", action = "Payment" },
               namespaces: new[] { "FreeCy.Controllers" }
           );
            routes.MapRoute(
               name: "tinh tien",
               url: "Cart/Payment/",
               defaults: new { controller = "Cart", action = "Payment" },
               namespaces: new[] { "FreeCy.Controllers" }
           );
            routes.MapRoute(
               name: "Thanh toan thanh cong",
               url: "Cart/Success/",
               defaults: new { controller = "Cart", action = "Success", order_id = UrlParameter.Optional },
               namespaces: new[] { "FreeCy.Controllers" }
           );
            routes.MapRoute(
              name: "Thanh toan khong thanh cong",
              url: "Cart/Error/",
              defaults: new { controller = "Cart", action = "Error", order_id = UrlParameter.Optional },
              namespaces: new[] { "FreeCy.Controllers" }
          );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "FreeCy.Controllers" }
            );

            
            //LoginFacebook
        }
    }
}
