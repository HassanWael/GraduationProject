using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LSS.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        /// <summary>
        /// Create all the Views
        /// </summary>
        /// <returns></returns>
       public ActionResult CreateCourse()
        {
            return View();
        }
        public ActionResult CreatUser()
        {
            return View();
        }
        public ActionResult CreatDpt()
        {
            return View();
        }
        public ActionResult CreatFaculty()
        {
            return View();
        }
        public ActionResult EditDpt()
        {
            return View();
        }
        public ActionResult EditFaculty()
        {
            return View();
        }

    }
}