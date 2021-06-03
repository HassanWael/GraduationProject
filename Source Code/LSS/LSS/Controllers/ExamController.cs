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
                CourseID= CourseID,
                Year= (DateTime)Year,
                semester= Semester,
                ExamDate = (DateTime)Year,
                ModerationDate = (DateTime)Year,
            };
            return View(courseExam);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCourseExam(CourseExam courseExam)
        {
            int sum = 0;
            List<int> weight = _DatabaseEntities.CourseExams.Where(x => x.CourseID.Equals(courseExam.CourseID) && x.Year.Equals(courseExam.Year) && x.semester.Equals(courseExam.semester)).Select(x => x.ExamWeight).ToList();
            foreach (int num in weight) {
                sum += num;

            }

            CourseCoordinator CC = _DatabaseEntities.CourseCoordinators.Find(courseExam.CourseID, courseExam.Year, courseExam.semester);

            if (courseExam.ExamWeight > (100 - sum)) {

                ModelState.AddModelError("ExamWeight", "The Total of  Exam Weight can't be more than 100");
            } else if (courseExam.ExamWeight <= 0)
            {

                ModelState.AddModelError("ExamWeight", " Exam Weight can't be less or equal than 0");
            }
            

            if (ModelState.IsValid&& _DatabaseEntities.CourseExams.Find(courseExam.ID)==null)
            {
              
                    _DatabaseEntities.CourseExams.Add(courseExam);
                    _DatabaseEntities.SaveChanges();
            
            }
            else
            {
                return View();
            }

            return RedirectToAction("CourseExamDetails", new { ExamID = courseExam.CourseID });
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
            CourseExam exam = _DatabaseEntities.CourseExams.Find(ExamID);
            if (exam == null)
            {
                return RedirectToAction("Index", "LogedIn");
            }

            QustionsVM q = new QustionsVM(exam);
            return View(q);
        
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateQustion(QustionsVM Model, int[]? PI_ID)
        {
            CourseExamQuestion qustion = Model.Question;
            CourseExam exam = _DatabaseEntities.CourseExams.Find(qustion.ExamID);
            Model.CourseExam = exam;
            int sum = 0;
            List<float> weight = _DatabaseEntities.CourseExamQuestions.Where(x => x.ExamID.Equals(qustion.ExamID)).Select(x => x.Weight).ToList();
            foreach (int num in weight)
            {
                sum += num;

            }
            if (qustion.Weight > (exam.ExamWeight - sum))
            {

                ModelState.AddModelError("ExamWeight", "The Total of  Exam Weight can't be more than 100");
            }
            else if (qustion.Weight <= 0)
            {

                ModelState.AddModelError("ExamWeight", " Exam Weight can't be less or equal than 0");
            }

            if (ModelState.IsValid)
            {
                try
                {

                  
                    _DatabaseEntities.CourseExamQuestions.Add(qustion); //error Here 
                    _DatabaseEntities.SaveChanges();

                    if (PI_ID != null)
                    {

                        foreach (int id in PI_ID)
                        {
                            PI pi = _DatabaseEntities.PIs.Find(id);
                            if (!qustion.PIs.Contains(pi))
                                qustion.PIs.Add(pi);
                        }
                        _DatabaseEntities.Entry(qustion).State = System.Data.Entity.EntityState.Modified;
                        _DatabaseEntities.SaveChanges();

                    }

                    return RedirectToAction("CourseExamDetails", "Exam", new { ExamID = qustion.ExamID });

                }
                catch (Exception e )
                {
                    ModelState.AddModelError("","Error has Acoured Please contact Support \n error Message " + e.Message);
                    return View(Model);
                }

            }
            else
            {
                return View(Model);
            }         
        }

    }
}