using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DMeServicesInternal.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {

            // Add ignore route for RAD PDF HTTP Handler
            // (NOTE: MVC's default axd ignore route doesn't work for sub-directories) 
            // (NOTE: This must be added above "MapRoute")
            // (NOTE: If using an MVC Area, you must also ignore this route in its RegisterArea() method of its AreaRegistration class)
            routes.IgnoreRoute("{*radpdfaxd}", new { radpdfaxd = @".*/RadPdf\.axd(\?.*)?" });


            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
