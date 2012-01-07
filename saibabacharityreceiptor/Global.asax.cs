﻿using System.Web.Mvc;
using System.Web.Routing;

namespace saibabacharityreceiptor
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode,
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "LogOn", // Route name
                "LogOn", // URL with parameters
                new { controller = "Account", action = "Logon" }
            );

            routes.MapRoute(
                "LogOff", // Route name
                "LogOff", // URL with parameters
                new { controller = "Account", action = "LogOff" }
            );

            routes.MapRoute(
                "LogIn", // Route name
                "LogIn", // URL with parameters
                new { controller = "Account", action = "Logon" }
            );

            routes.MapRoute(
                "LogOut", // Route name
                "LogOut", // URL with parameters
                new { controller = "Account", action = "LogOff" }
            );

            routes.MapRoute(
                "Controlpanel", // Route name
                "Controlpanel", // URL with parameters
                new { controller = "Controlpanel", action = "home" }
            );

            routes.MapRoute(
                "Users", // Route name
                "controlpanel/EditUser/{uid}", // URL with parameters
                new { controller = "Controlpanel", action = "edituser", uid = UrlParameter.Optional }
            );

            routes.MapRoute(
                "RegularReceipt", // Route name
                "RegularReceipt", // URL with parameters
                new { controller = "Home", action = "RegularReceipt" }
            );

            routes.MapRoute(
                "MerchandiseReceipt", // Route name
                "MerchandiseReceipt", // URL with parameters
                new { controller = "Home", action = "MerchandiseReceipt" }
            );

            routes.MapRoute(
                "RecurringReceipt", // Route name
                "RecurringReceipt", // URL with parameters
                new { controller = "Home", action = "RecurringReceipt" }
            );

            routes.MapRoute(
                "ServicesReceipt", // Route name
                "ServicesReceipt", // URL with parameters
                new { controller = "Home", action = "ServicesReceipt" }
            );

            routes.MapRoute(
                "Userinfo", // Route name
                "controlpanel/Viewuserinfo/{userid}", // URL with parameters
                new { controller = "Controlpanel", action = "Viewuserinfo", userid = UrlParameter.Optional }
            );

            routes.MapRoute(
                "PrintReceipt", // Route name
                "PrintReceipt/{recpId}", // URL with parameters
                new { controller = "Home", action = "PrintReceipt", recpId = UrlParameter.Optional }
            );

            routes.MapRoute(
                "DownloadReceipt", // Route name
                "DownloadReceipt/{recpId}", // URL with parameters
                new { controller = "Home", action = "DownloadReceipt", recpId = UrlParameter.Optional }
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "controlpanel", action = "home", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}