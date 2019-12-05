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
            name: "DirectCalendrier",
            url: "{name}",
            defaults: new { controller = "Calendrier", action = "Index", name = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "DirectModifier",
            url: "Modifier/{name}",
            defaults: new { controller = "Calendrier", action = "Modifier"/*, name = UrlParameter.Optional*/ }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{name}",
                defaults: new { controller = "Home", action = "Index", name = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Calendar",
                url: "Calendrier/{name}",
                defaults: new { controller = "Calendrier", action = "Index", name = UrlParameter.Optional }
            );

        }
    }
}
