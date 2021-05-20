﻿using LSS.Models;
using LSS.Models.arc;
using LSS.Models.CoursesModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LSS.Controllers
{

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
           

            CourseCoordinator cc = _DatabaseEntities.CourseCoordinators.Find("A0334501", yearAndSemester.Year, yearAndSemester.Semester);
            CouresModelView cmv = new CouresModelView(cc);
            return View(cmv);
        }

        public ActionResult CourseReport()
        {
            return View();
        }

        public ActionResult CourseSyllabus(string? id)
        {
            CourseCoordinator cc = _DatabaseEntities.CourseCoordinators.Find(id, yearAndSemester.Year, yearAndSemester.Semester);

            return View(cc);
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
    }
}
