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
        public ActionResult CouresPage(String courseID)
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
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var CIFModelView = new CIFModelView
            {
                Course = _DatabaseEntities.Courses.Where(i => i.ID.Equals(id)).FirstOrDefault(),
            };
            if (CIFModelView.Course == null)
            {
                return HttpNotFound();
            }
            
            return View(CIFModelView);
        }

    }

}