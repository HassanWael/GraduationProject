using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LSS.Models;

namespace LSS.Controllers
{
    public class HomeController : Controller
    {
        private readonly LSS_databaseEntities _databaseEntities = new LSS_databaseEntities();
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(Lecturer user, String ReturnUrl)
        {
           
                Lecturer ValidUser = _databaseEntities.Lecturers.SingleOrDefault(lecturer => lecturer.ID.Equals(user.ID) && lecturer.Password.Equals(user.Password));
                if (ValidUser != null)
                {
         
                    Session["Name"] = ValidUser.Name;
                    Session["Role"] = ValidUser.Role;
                    Session["Dpt"] = ValidUser.dptId;
                    var identity = (ClaimsIdentity)User.Identity;
                    IEnumerable<Claim> claims = identity.Claims;
                    identity.AddClaim(new Claim("DeptID", ValidUser.dptId.ToString()));
                    FormsAuthentication.SetAuthCookie(ValidUser.ID, false);
                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "LogedIn");
                    }
                }
                else
                {
                    ViewBag.Error = "You have entered an invalid username or password";
                    return View();
                }
            

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}
