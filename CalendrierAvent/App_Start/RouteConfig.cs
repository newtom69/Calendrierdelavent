using System.Web.Mvc;
using System.Web.Routing;

namespace AdventCalendar
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                name: "DirectCalendar",
                url: "{name}",
                defaults: new { controller = "Calendrier", action = "Index", name = UrlParameter.Optional },
                namespaces: new[] { "AdventCalendar.Controllers" }
            );

            routes.MapRoute(
                name: "Calendar",
                url: "Calendrier/{name}",
                defaults: new { controller = "Calendrier", action = "Index", name = UrlParameter.Optional },
                namespaces: new[] { "AdventCalendar.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{name}",
                defaults: new { controller = "Home", action = "Index", name = UrlParameter.Optional },
                namespaces: new[] { "AdventCalendar.Controllers" }
            );

        }
    }
}
