using LSS.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LSS.Models.Exams;

namespace LSS.Controllers
{
    public class ExamController : Controller
    {
        private readonly LSS_databaseEntities _DatabaseEntities = new LSS_databaseEntities();
        // GET: Exam
        public ActionResult CreateCourseExam(string? CourseID, DateTime? Year, string? Semester)
        {

            CourseCoordinator CC = _DatabaseEntities.CourseCoordinators.Find(CourseID, Year, Semester);
            if (CC == null)
            {
                return RedirectToAction("Index", "LogedIn");
            }
            CourseExam courseExam = new CourseExam()
            {
                CourseID=CC.CourseID,
                Year=CC.Year,
                Semseter=CC.Semseter,
                ExamDate = DateTime.Now,
                ModerationDate= DateTime.Now
            };
            return View(courseExam);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCourseExam(CourseExam courseExam)
        {
            
            if (ModelState.IsValid&& courseExam != null)
            {
                try
                {
                    _DatabaseEntities.CourseExams.Add(courseExam);
                    _DatabaseEntities.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Errorr e " + e.Message);
                }
            }
            else
            {
                return View();
            }

            return RedirectToAction("CourseExamDetails", new { ExamID = courseExam.ID });
}
        public ActionResult ListCourseExamsAndTasks(string? CourseID, DateTime? Year, string? Semester, int page = 1, int pageSize = 10)
        {
            ViewBag.CourseID = CourseID;
            ViewBag.Year = Year;
            ViewBag.Semester = Semester;
            CourseCoordinator courseCoordinator = _DatabaseEntities.CourseCoordinators.Find(CourseID, Year, Semester);
            List<CourseExam> Exams = courseCoordinator.CourseExams.ToList();
            PagedList<CourseExam> CoursesPaged = new PagedList<CourseExam>(Exams , page, pageSize);
            return View(CoursesPaged);
        }
        public ActionResult CourseExamDetails(int? ExamID)
        {
            CourseExam Exam = _DatabaseEntities.CourseExams.Find(ExamID);
            if (Exam== null)
                return new HttpStatusCodeResult(404);
            return View(Exam);
        }
                
        public ActionResult PIAssessments(string CourseID, DateTime Year, string Semester)
        {

            return View();
        }
        
        public ActionResult CreateQustion(int? ExamID)
        {
            //CourseExam exam = _DatabaseEntities.CourseExams.Find(ExamID);

            //QustionsVM q = new QustionsVM()
            //{
            //    CourseExam = exam
            //};

            //return View(q);
            return View();
        
        }

    }
}