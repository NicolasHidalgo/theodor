using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;

namespace webapp.App_Start
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Login/Login"),
                LogoutPath = new PathString("/Login/Logoff"),
                //CookieName = "LoginCookie",
                CookieHttpOnly = true,
                ExpireTimeSpan = TimeSpan.FromMinutes(15), 
                SlidingExpiration = true,
            });
        }
    }
}