using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace CookiesAndSession
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        public void Application_PostAuthenticateRequest()
        {
            HttpCookie cookie = Request.Cookies["TIKECTCOOKIE"];            
            
            AuthenticationCookie(cookie);
        }

        public void AuthenticationCookie(HttpCookie cookie)
        {
            if (cookie != null)
            {
                String data = cookie.Value;
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(data);

                String username = ticket.Name;
                String role = ticket.UserData;

                GenericIdentity identity = new GenericIdentity(username);
                GenericPrincipal user = new GenericPrincipal(identity, new String[] { role });

                HttpContext.Current.User = user;
            }
        }
    }
}
