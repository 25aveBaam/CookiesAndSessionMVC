using CookiesAndSession.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CookiesAndSession.Controllers
{
    public class UsersController : Controller
    {
        // GET: Index
        public ActionResult Index()
        {
            return View();
        }

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public ActionResult Login(String UserName, String Password)
        {
            User user = new User();
            var model = user.ReturnList().Where(z => z.UserName == UserName && z.Password == Password).SingleOrDefault();
            if (model != null)
            {
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, UserName, DateTime.Now, DateTime.Now.AddMinutes(10), true, UserName);
                String Encrypt = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie("TIKECTCOOKIE", Encrypt);

                Response.Cookies.Add(cookie);

                return RedirectToAction("Index", "Users");
            }
            else
            {
                ViewBag.Message = "UserName o Password incorrecto";
            }

            return View();
        }

        // GET: Logout
        public ActionResult Logout()
        {
            HttpContext.User = new GenericPrincipal(new GenericIdentity(""), null);
            FormsAuthentication.SignOut();
            HttpCookie cookie = Request.Cookies["TIKECTCOOKIE"];
            cookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie);

            return RedirectToAction("Login", "Users");
        }
    }
}