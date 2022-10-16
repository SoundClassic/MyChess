using System.Web.Http;

namespace ChessAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Конфигурация и службы Web API

            // Маршруты Web API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "chess/{controller}/{id}/{move}",
                defaults: new 
                { 
                    id = RouteParameter.Optional,
                    move = RouteParameter.Optional
                }
            );
        }
    }
}
