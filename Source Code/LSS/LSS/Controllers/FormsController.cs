using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LSS.Controllers
{
    public class FormsController : Controller
    {
        // GET: Forms
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CourseAssessmentSurvey()
        {
            return View();
        }

        public ActionResult CourseFileChecklist()
        {
            return View();
        }
       
        public ActionResult CourseInformationForm()
        {
            return View();
        }

        public ActionResult CourseReport()
        {
            return View();
        }

        public ActionResult CourseSyllabus()
        {
            return View();
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
    }
}