using LSS.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LSS.Controllers
{
    public class ExamController : Controller
    {
        private readonly LSS_databaseEntities _DatabaseEntities = new LSS_databaseEntities();
        // GET: Exam
        public ActionResult CreateCourseExam(int CourseID, int Year, string Semester)
        {
            CourseExam name = _DatabaseEntities.CourseExams.Find(CourseID, Year, Semester);
            if (name == null)
            {
                return RedirectToAction("Index", "LogedIn");
            }
            return View(name);
            
        }
        [HttpPost]
        public ActionResult CreatExam(CourseExam courseExam)
        {
            if (courseExam != null)
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

            return View();
}
        public ActionResult ListCourseExam(string CourseID, int Year, string Semester, int page = 1, int pageSize = 10)
        {
            List<CourseExam> Exams = _DatabaseEntities.CourseExams.Where(x => x.CourseID.Equals(CourseID) && x.Year.Equals(Year) && x.Semseter.Equals(Semester)).ToList();
            PagedList<CourseExam> CoursesPaged = new PagedList<CourseExam>(Exams , page, pageSize);
            return View(CoursesPaged);

        }
        public ActionResult CourseExamDetails(int ExamID)
        {
            CourseExam Exam = _DatabaseEntities.CourseExams.Find(ExamID);

            if (Exam != null)
                return new HttpStatusCodeResult(404);
            return View(Exam);
           
        }

    }
}