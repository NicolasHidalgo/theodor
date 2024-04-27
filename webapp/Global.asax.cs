using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using webapp.Controllers;

namespace webapp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            log4net.Config.XmlConfigurator.Configure();
        }

        protected void ApplicationEnd()
        {
        }

        protected void Application_BeginRequest()
        {
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-PE");
            //MiniProfiler.Start();
        }

        protected void Application_EndRequest()
        {
            //MiniProfiler.Stop();
            // con esto controlo la autenticacion si via por ajax
            if (!this.Request.IsAuthenticated && Context.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                Context.Response.Clear();
                Context.Response.StatusCode = 401;
            }


            /*
            var context = new HttpContextWrapper(Context);
            // MVC retuns a 302 for unauthorized ajax requests so alter to request status to be a 401
            if (context.Response.StatusCode == 302 && context.Request.IsAjaxRequest() && !context.Request.IsAuthenticated)
            {
                context.Response.Clear();
                context.Response.StatusCode = 401;
            }
            */

            /*
            var context = new HttpContextWrapper(Context);
            // MVC returns a 302 for unauthorized ajax requests so alter to request status to be a 401

            if (context.Response.StatusCode == 302 && context.Request.IsAjaxRequest())
            {
                //Unfortunately the redirect also clears the results of any authentication
                //Try to manually authenticate the user...
                var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null)
                {
                    var authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                    if (authTicket != null && !authTicket.Expired)
                    {
                        var roles = authTicket.UserData.Split(',');
                        HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(new FormsIdentity(authTicket), roles);
                    }
                }

                if (!context.Request.IsAuthenticated)
                {
                    context.Response.Clear();
                    context.Response.StatusCode = 401;
                }

            }
            */
        }
        public void Application_Error(Object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Server.ClearError();

            var routeData = new RouteData();
            routeData.Values.Add("controller", "Error");
            routeData.Values.Add("action", "Error");
            routeData.Values.Add("exception", exception.Message);

            if (exception.GetType() == typeof(HttpException))
            {
                routeData.Values.Add("statusCode", ((HttpException)exception).GetHttpCode());
            }
            else
            {
                routeData.Values.Add("statusCode", 500);
            }

            Response.TrySkipIisCustomErrors = true;
            IController controller = new ErrorController();
            controller.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
            Response.End();
        }
    }
}
