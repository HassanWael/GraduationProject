using System;
using System.Collections.Generic;
using System.Linq;
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
            if (ModelState.IsValid)
            {
                Lecturer ValidUser = _databaseEntities.Lecturers.SingleOrDefault(lecturer => lecturer.ID.Equals(user.ID) && lecturer.Password.Equals(user.Password));
                if (ValidUser != null)
                {        
                    FormsAuthentication.SetAuthCookie(ValidUser.ID, false);
                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        Session["ID"] = ValidUser.ID;
                        Session["Role"] = ValidUser.Role;
                        Session["Dpt"] = ValidUser.dptId;
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        Session["ID"] = ValidUser.ID;
                        Session["Role"] = ValidUser.Role;
                        Session["Dpt"] = ValidUser.dptId;
                        return RedirectToAction("Index", "LogedIn");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid ID or Password, try Again ");
                    return View();
                }
            }
            else
            {
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

    }
}
