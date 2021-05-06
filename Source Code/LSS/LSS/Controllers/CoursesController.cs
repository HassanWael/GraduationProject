using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LSS.Controllers
{
    public class CoursesController : Controller
    {
        // GET: Courses
        public ActionResult CouresPage()
        {

            ViewBag.Message = "Coures view Page";
            return View();
        }


        public ActionResult CreateCourseInformationForm()
        {
            return View();
        }

    }
}