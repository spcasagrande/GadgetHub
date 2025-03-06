using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GadgetHub.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			// list first page from all categories
			routes.MapRoute(null, "", new
			{
				Controller = "Product",
				action = "List",
				category = (string)null,
				page = 1
			});

			// url = /Page/2
			routes.MapRoute(null, "Page/{page}", new
			{
				Controller = "Product",
				action = "List",
				category = (string)null
			},
			new { page = @"\d+" }); // Regex; d=digits, + = one or more

			// first page of items from specific category, url = /Category/Phones
			routes.MapRoute(null, "Category/{Category}", new
			{
				Controller = "Product",
				action = "List",
				page = 1
			});

			// url = /Category/Phones/Page/2
			routes.MapRoute(null, "Category/{Category}/Page/{page}", new
			{
				Controller = "Product",
				action = "List",
			},
			new { page = @"\d+" });

			routes.MapRoute(null, "{controller}/{action}");
		}
    }
}
