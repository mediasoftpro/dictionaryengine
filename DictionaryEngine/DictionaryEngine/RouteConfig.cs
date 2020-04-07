using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Jugnoon.Core
{
    public static class RouteConfig
    {
        public static IEndpointRouteBuilder Use(IEndpointRouteBuilder routeBuilder)
        {
           


            #region User Profile
            routeBuilder.MapControllerRoute(
                    null,
                    "user/profile/{username}",
                    defaults: new { controller = "user", action = "profile" }

                );
          
            routeBuilder.MapControllerRoute(
                null,
                "user/{username}",
                defaults: new { controller = "user", action = "Index" }

             );
            #endregion

            #region Wiki

            routeBuilder.MapControllerRoute(
               null,
               pattern: "glossary/term/{term}/{id?}",
               defaults: new { controller = "glossary", action = "term" });

            routeBuilder.MapControllerRoute(
               null,
               pattern: "glossary/character/{term}/{id?}",
               defaults: new { controller = "glossary", action = "character" });

            routeBuilder.MapControllerRoute(
               null,
               pattern: "wiki/{title}",
               defaults: new { controller = "wiki", action = "Index" });

            #endregion

            #region Glossary

            routeBuilder.MapControllerRoute(
                  null,
                  "glossary/{id}",
                  defaults: new { controller = "glossary", action = "Index" }
                  
              );

            #endregion

            routeBuilder.MapControllerRoute(
                name: "ActionApi",
                pattern: "api/{controller}/{action}/{name?}"
            );
            routeBuilder.MapControllerRoute(
                null,
                "/account/{*url}",
                defaults: new { controller = "account", action = "Index" }
             );

            routeBuilder.MapControllerRoute(
              null,
              "/admin/{*url}",
              defaults: new { controller = "admin", action = "Index" }
            );

            routeBuilder.MapFallbackToController("account/", "Index", "account");
            routeBuilder.MapFallbackToController("admin/", "Index", "admin");

            // default root
            routeBuilder.MapControllerRoute(
                 null,
                 "/{page}",
                 defaults: new { controller = "Home", action = "Index" }
            );

            routeBuilder.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Home}/{action=Index}/{id?}");


            return routeBuilder;
        }
    }
    
    public static class InitRouteControllerConfig
    {
        public static IEndpointRouteBuilder Use(IEndpointRouteBuilder routeBuilder)
        {
            routeBuilder.MapControllerRoute(
                            name: "default",
                            pattern: "{controller=Installation}/{action=Index}/{id?}");

            return routeBuilder;
        }
    }
}
