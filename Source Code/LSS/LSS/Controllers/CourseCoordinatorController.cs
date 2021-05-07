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

        // GET: Courses
        public ActionResult CouresPage(string? courseID)
        {
            YearAndSemester y = SemesterSingelton.getCurrentYearAndSemester(); 
            String userID = Session["ID"].ToString();
            CourseCoordinator c = _DatabaseEntities.CourseCoordinators.Where(x => x.CourseID.Equals(courseID)
                                  && x.Year.Equals(y.Year) && x.Semseter.Equals(y.Semester)).FirstOrDefault();

            ViewBag.Message = "Coures view Page";
            return View();
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