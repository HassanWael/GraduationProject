using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using LSS.Models.LSS_DB_Model;

namespace LSS.Controllers
{
    public class HomeController : Controller
    {
        public LSS_databaseEntities _dbEntities = new LSS_databaseEntities(); 

        public ActionResult Login()
        {
            if (Session["ID"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "LogedInController");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(String ID , String password)
        {
            if (ModelState.IsValid)
            {
                var data = _dbEntities.Lecturers.Where(s => s.ID.Equals(ID) && s.Password.Equals(password)).ToList();
                if (data.Count > 0)
                {
                    
                    Session["Name"] = data.FirstOrDefault().Name;
                    Session["ID"] = data.FirstOrDefault().ID;
                    Session["Role"]=data.FirstOrDefault().Role;
                    Session["Dpt"] = data.FirstOrDefault().DptID;


                    return RedirectToAction("Index", "LogedIn");
                }
                else
                {
                    ViewBag.error = "Login Failed";
                    return RedirectToAction("Login");
                }
            }


            return View();
        }
    }
}