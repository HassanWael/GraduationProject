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
   // [Authorize]
    public class FormsController : Controller
    {
        private readonly LSS_databaseEntities _DatabaseEntities = new LSS_databaseEntities();
        // GET: Forms

        public ActionResult CourseAssessmentSurvey(string? CourseID, DateTime? Year, string? Semester)
        {
            if (CourseID == null || Year == null || Semester==null)
            {
                return new HttpStatusCodeResult(404);
            }
            CourseCoordinator cc = _DatabaseEntities.CourseCoordinators.Find(CourseID, Year, Semester);
            if (cc == null)
            {
                return new HttpStatusCodeResult(404);

            }
            return View(cc);
        }

        public ActionResult CourseFileChecklist(string? CourseID, DateTime? Year, string? Semester)
        {
            if (CourseID == null || Year == null || Semester == null)
            {
                return new HttpStatusCodeResult(404);
            }
            CourseCoordinator cc = _DatabaseEntities.CourseCoordinators.Find(CourseID, Year, Semester);
            if (cc == null)
            {
                return new HttpStatusCodeResult(404);

            }
            return View(cc);
        }

        public ActionResult CourseInformationForm(string? CourseID, DateTime? Year, string? Semester)
        {
            if (CourseID == null || Year == null || Semester == null)
            {
              return  new HttpStatusCodeResult(404);
            }
            CourseCoordinator cc = _DatabaseEntities.CourseCoordinators.Find(CourseID, Year, Semester);
            if (cc == null)
            {
                return new HttpStatusCodeResult(404);

            }
            CouresModelView cmv = new CouresModelView()
            {
                CourseCoordinator = cc
            };
            return View(cmv);
        }

        public ActionResult CourseReport(string? CourseID, DateTime? Year, string? Semester)
        {
            if (CourseID == null || Year == null || Semester == null)
            {
                return new HttpStatusCodeResult(404);
            }
            CourseCoordinator cc = _DatabaseEntities.CourseCoordinators.Find(CourseID, Year, Semester);
            if (cc == null)
            {
                return new HttpStatusCodeResult(404);

            }
            CouresReportModelView cmv = new CouresReportModelView(cc);
            return View(cmv);
        }

        public ActionResult CourseSyllabus(string? CourseID, DateTime? Year, string? Semester)
        {
            if (CourseID == null || Year == null || Semester == null)
            {
                new HttpStatusCodeResult(404);
            }
            CourseCoordinator cc = _DatabaseEntities.CourseCoordinators.Find(CourseID, Year, Semester);
            if (cc == null)
            {
                new HttpStatusCodeResult(404);

            }
            CouresReportModelView cmv = new CouresReportModelView(cc);
            return View(cmv);
        }

        public ActionResult ExamEvaluation(string? CourseID, DateTime? Year, string? Semester)
        {
            if (CourseID == null || Year == null || Semester == null)
            {
                new HttpStatusCodeResult(404);
            }
            CourseCoordinator cc = _DatabaseEntities.CourseCoordinators.Find(CourseID, Year, Semester);
            if (cc == null)
            {
                new HttpStatusCodeResult(404);

            }
            return View();
        }

        public ActionResult ExamModerationChecklist(string? CourseID, DateTime? Year, string? Semester)
        {
            if (CourseID == null || Year == null || Semester == null)
            {
                new HttpStatusCodeResult(404);
            }
            CourseCoordinator cc = _DatabaseEntities.CourseCoordinators.Find(CourseID, Year, Semester);
            if (cc == null)
            {
                new HttpStatusCodeResult(404);

            }

            return View();
        }

        public ActionResult QuestionsAnswersSheet(string? CourseID, DateTime? Year, string? Semester)
        {
            if (CourseID == null || Year == null || Semester == null)
            {
                new HttpStatusCodeResult(404);
            }
            CourseCoordinator cc = _DatabaseEntities.CourseCoordinators.Find(CourseID, Year, Semester);
            if (cc == null)
            {
                new HttpStatusCodeResult(404);

            }
            return View();
        }

    }
}
