using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapp.App_Start
{
    public partial class Startup
    {
        public static class MyAuthentication
        {
            public const String ApplicationCookie = "ADAuthCookie";
        }
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = MyAuthentication.ApplicationCookie, //DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Login/Login"),
                LogoutPath = new PathString("/Login/Logoff"),
                //CookieName = "LoginCookie",
                CookieHttpOnly = true,
                ExpireTimeSpan = TimeSpan.FromMinutes(15), //TimeSpan.FromHours(12), 
                SlidingExpiration = true
            });
        }
    }
}