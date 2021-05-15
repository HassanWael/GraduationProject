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
            //if (courseID == null)
            //{
            //    RedirectToAction("Index", "LogedIN");
            //}

            YearAndSemester y = SemesterSingelton.getCurrentYearAndSemester();
            //String userID = Session["ID"].ToString();
            CourseCoordinator cc = _DatabaseEntities.CourseCoordinators.Find("A0334501", yas.Year, yas.Semester);
            CouresModelView course = new CouresModelView(cc);

            if (cc == null)
            {
                RedirectToAction("Index", "LogedIN");
            }
            ViewBag.Message = "Coures view Page";

            return View(course);
        }

        public ActionResult EditCourse()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCLO(AddCLOMV addCLO,string [] PI_ID )
        {
            if (ModelState.IsValid)
            {
                CLO clo = addCLO.CLO;
                PI piToAdd;
                _DatabaseEntities.CLOes.Add(clo);
                if (PI_ID.Length > 0)
                {
                    var selected_PI = _DatabaseEntities.PIs.Where(
                        m => PI_ID.Contains(m.ID)).ToList();

                    foreach (string pi in PI_ID)
                    {
                        piToAdd = _DatabaseEntities.PIs.Where(x => x.ID == pi && x.DeptID == addCLO.DpetID).FirstOrDefault();
                        clo.PIs.Add(piToAdd);
                    }
                }
                _DatabaseEntities.SaveChanges();
            }
            return RedirectToAction("CouresPage", new { courseID = addCLO.CLO.courseId });
        }


        public ActionResult CreateCourseInformationForm(string id)
        {
            return View();
        }
        public ActionResult CreateCLO() {
            return View();
        }

    }

}