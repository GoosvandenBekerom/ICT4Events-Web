using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace ICT4Events_Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        void Application_Error(object sender, EventArgs e)
        {
            // Get last error from the server
            var exc = Server.GetLastError();

            if (!(exc is HttpUnhandledException)) return;
            if (exc.InnerException == null) return;

            new Exception(exc.InnerException.Message);
            Response.Redirect("~/ErrorPage.aspx?handler=Application_Error%20-%20Global.asax", true);
        }
    }
}