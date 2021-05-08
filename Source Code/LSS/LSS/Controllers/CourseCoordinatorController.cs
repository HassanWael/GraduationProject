using LSS.Models;
using LSS.Models.arc;
using LSS.Models.CoursesModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LSS.Controllers
{
    public class CourseCoordinatorController : Controller
    {
        LSS_databaseEntities _DatabaseEntities = new LSS_databaseEntities();
        
        private YearAndSemester yas = SemesterSingelton.getCurrentYearAndSemester();
        
        // GET: Courses
        public ActionResult CouresPage(string? courseID)
        {
            if (courseID == null)
            {
                RedirectToAction("Index", "LogedIN");
            }

            YearAndSemester y = SemesterSingelton.getCurrentYearAndSemester(); 
            String userID = Session["ID"].ToString();
            CourseCoordinator cc = _DatabaseEntities.CourseCoordinators.Find(courseID, yas.Year, yas.Semester);
            CouresModelView course = new CouresModelView(cc);
            ViewBag.Message = "Coures view Page";
            return View(cc);
        }

        public ActionResult EditCourse()
        {
            return View();
        }

        public ActionResult CreateCourseInformationForm(string id)
        {
            return View();
        }

    }

}