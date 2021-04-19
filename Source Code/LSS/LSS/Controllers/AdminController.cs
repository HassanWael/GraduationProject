using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LSS.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        //ToDO :Create Views For the Action Results
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