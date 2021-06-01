using LSS.Models;
using LSS.Models.arc;
using LSS.Models.CoursesModelView;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LSS.Controllers
{
    [Authorize]
    public class FormsController : Controller
    {
        private readonly YearAndSemester yearAndSemester = SemesterSingelton.getCurrentYearAndSemester();
        private readonly LSS_databaseEntities _DatabaseEntities = new LSS_databaseEntities();
        // GET: Forms
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CourseAssessmentSurvey()
        {
            return View();
        }

        public ActionResult CourseFileChecklist(string? CourseID)
        {
            CourseCoordinator cc = _DatabaseEntities.CourseCoordinators.Find(CourseID, yearAndSemester.Year, yearAndSemester.Semester);
            return View();
        }

        public ActionResult CourseInformationForm(string? CourseID)
        {
            if (CourseID is null)
            {
                    RedirectToAction("Index","Home");
            }

            CourseCoordinator cc = _DatabaseEntities.CourseCoordinators.Find(CourseID, yearAndSemester.Year, yearAndSemester.Semester);
            CouresModelView cmv = new CouresModelView(cc);
            return View(cmv);
        }

        public ActionResult CourseReport(string? CourseID)
        {
            if (CourseID is null)
            {
                RedirectToAction("Index", "Home");
            }

            CourseCoordinator cc = _DatabaseEntities.CourseCoordinators.Find(CourseID, yearAndSemester.Year, yearAndSemester.Semester);
            CouresReportModelView cmv = new CouresReportModelView(cc);
            return View(cmv);
        }

        public ActionResult CourseSyllabus(string? CourseID)
        {
            if (CourseID is null)
            {
                RedirectToAction("Index", "Home");
            }

            CourseCoordinator cc = _DatabaseEntities.CourseCoordinators.Find(CourseID, yearAndSemester.Year, yearAndSemester.Semester);
            CouresReportModelView cmv = new CouresReportModelView(cc);
            return View(cmv);
        }

        public ActionResult ExamEvaluation()
        {
            return View();
        }

        public ActionResult ExamModerationChecklist()
        {   
            return View();
        }

        public ActionResult QuestionsAnswersSheet()
        {
            return View();
        }
        public ActionResult CouresSyllabusAddData() {
            return View();
        }
        public ActionResult addCourseSyllabus(string id,DateTime year,string semester)
        {
            CourseCoordinator cc = _DatabaseEntities.CourseCoordinators.Find(id, year, semester);
            return View(cc);
        }
        [HttpPost]
        public ActionResult addCourseSyllabus(CourseSyllabu cs)

        {
            try
            {

                _DatabaseEntities.CourseSyllabus.Add(cs);
                _DatabaseEntities.SaveChanges();
            }
            catch {
                _DatabaseEntities.Entry(cs).State = EntityState.Modified;
                _DatabaseEntities.SaveChanges();
            }
                return RedirectToAction("CoursePage", "CourseCoordinator", new { couresId = cs.CourseID });
            
        }
    }
}
