using LSS.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LSS.Models.Exams;
using OfficeOpenXml;

namespace LSS.Controllers
{
    [Authorize]
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
                Semester= Semester,
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
            List<int> weight = _DatabaseEntities.CourseExams.Where(x => x.CourseID.Equals(courseExam.CourseID) && x.Year.Equals(courseExam.Year) && x.Semester.Equals(courseExam.Semester)).Select(x => x.ExamWeight).ToList();
            foreach (int num in weight) {
                sum += num;

            }

            CourseCoordinator CC = _DatabaseEntities.CourseCoordinators.Find(courseExam.CourseID, courseExam.Year, courseExam.Semester);

            if (courseExam.ExamWeight > (100 - sum)) {

                ModelState.AddModelError("ExamWeight", "The Total of  Exam Weight can't be more than 100");
            } else if (courseExam.ExamWeight <= 0)
            {

                ModelState.AddModelError("ExamWeight", " Exam Weight can't be less or equal than 0");
            }
            

            if (ModelState.IsValid)
            {
                    _DatabaseEntities.CourseExams.Add(courseExam);
                    _DatabaseEntities.SaveChanges();
            
            }
            else
            {
                return View();
            }

            return RedirectToAction("CourseExamDetails", new { ExamID = courseExam.CourseID , Year = courseExam.Year, Semester = courseExam.Semester });
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
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditQustions(QustionsVM Model, int[]? PI_ID)
        {
            if (ModelState.IsValid)
            {
                CourseExamQuestion qustion = Model.Question;
                CourseExamQuestion q = _DatabaseEntities.CourseExamQuestions.Find(qustion.ID);
                q.Question = qustion.Question;
                q.QuestionNumber = qustion.QuestionNumber;
                q.Weight = qustion.Weight;
                foreach (PI pi in Model.PIs)
                {

                    if (PI_ID.Contains(pi.PIUniqueID))
                    {
                        if (!q.PIs.Contains(pi))
                        {
                            q.PIs.Add(pi);
                        }
                    }
                    else
                    {
                        if (q.PIs.Contains(pi))
                        {
                            q.PIs.Remove(pi);
                        }
                    }
                }
                return RedirectToAction("ListCourseExamsAndTasks", new { CourseID = Model.CourseExam.CourseID, Year = Model.CourseExam.Year, Semester = Model.CourseExam.Semester });
            }
            return View();
        }

        public ActionResult DeleteQustions(int? id, string? CourseID , DateTime Year , string? Semester )
        {
            CourseExamQuestion q = _DatabaseEntities.CourseExamQuestions.Find(id);
            if (q != null)
            {
                _DatabaseEntities.CourseExamQuestions.Remove(q);
                _DatabaseEntities.SaveChanges();
            }

            return RedirectToAction("ListCourseExamsAndTasks", new { CourseID = CourseID, Year = Year, Semester = Semester });
        }


        public ActionResult UploadMarks(FormCollection formCollection,int? ExamID)
        {
            int failedToAddGraddeTo = 0;
             
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["Select Grades File"];

                if ((file != null) && (file.ContentLength != 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                }

                using (var package = new ExcelPackage(file.InputStream))
                {
                    var currentSheet = package.Workbook.Worksheets;
                    var workSheet = currentSheet.First();
                    var noOfCol = workSheet.Dimension.End.Column;
                    var noOfRow = workSheet.Dimension.End.Row;
                    CourseExam exam = _DatabaseEntities.CourseExams.Find(ExamID);
                   List< CourseExamQuestion> questions = exam.CourseExamQuestions.ToList();
                    if (exam != null)
                    {
                        for (int rowIterator = 2; rowIterator < noOfRow; rowIterator++)
                        {
                            string id = workSheet.Cells[rowIterator, 1].Value.ToString();
                            EnroledStudent enroledStudent = _DatabaseEntities.EnroledStudents.Where(x => x.StudentID.Equals(id)
                                                && x.CourseID.Equals(exam.CourseID) && x.Year.Equals(exam.Year) && x.Semester.Equals(exam.Semester)).FirstOrDefault();
                            if (enroledStudent == null) continue;
                            int count = 0; 
                            int colIterator = 2;
                            while (colIterator <= exam.CourseExamQuestions.Count()+1)
                            {

                                if (workSheet.Cells[rowIterator, 1].Value != null    && workSheet.Cells[rowIterator, colIterator].Value != null)
                                    try
                                    {
                                       
                                        CourseExamEval examEval = new CourseExamEval();
                                        examEval.QID = questions[count].ID;
                                        examEval.StudentID = enroledStudent.ID;
                                        examEval.Mark =float.Parse(workSheet.Cells[rowIterator, colIterator].Value.ToString());
                                        _DatabaseEntities.CourseExamEvals.Add(examEval);
                                        _DatabaseEntities.SaveChanges();
                                      
                                    }
                                    catch (Exception e)
                                    {
                                        failedToAddGraddeTo++;
                                        Console.WriteLine("Error at 340 CourseCoorddinatorController" + e);
                                    }
                                colIterator++;
                                count++;
                            }
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index", "LogedIn");
                    }
                }
            }

            return RedirectToAction("CourseExamDetails", "Exam", new { ExamID= ExamID });

        }




    }
}