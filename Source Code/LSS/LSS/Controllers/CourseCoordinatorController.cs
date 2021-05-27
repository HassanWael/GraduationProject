using LSS.Models;
using LSS.Models.arc;
using LSS.Models.CoursesModelView;
using OfficeOpenXml;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LSS.Controllers
{
    public class CourseCoordinatorController : Controller
    {
        private readonly LSS_databaseEntities _DatabaseEntities = new LSS_databaseEntities();

        private readonly YearAndSemester yas = SemesterSingelton.getCurrentYearAndSemester();

        // GET: Courses
        public ActionResult CouresPage(string? courseID = "A0334501")
        {
            if (courseID == null)
            {
                RedirectToAction("Index", "LogedIN");
            }

            //String userID = Session["ID"].ToString();
            CourseCoordinator cc = _DatabaseEntities.CourseCoordinators.Find(courseID, yas.Year, yas.Semester);
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
        public ActionResult AddCLO(AddCLOMV addCLO, string[] PI_ID)
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


        public ActionResult CreateCourseInformationForm()
        {

            return View();
        }
        public ActionResult DeleteCLO(int id)
        {
            try
            {
                CLO CLO = _DatabaseEntities.CLOes.Find(id);
                if (CLO != null)
                {
                    string courseID = CLO.courseId;
                    _DatabaseEntities.CLOes.Remove(CLO);
                    _DatabaseEntities.SaveChanges();

                    return RedirectToAction("CouresPage", "CourseCoordinator", courseID);
                }
                else
                {
                    return HttpNotFound();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error at line 72 of CourseCoordinator" + e);
                return View();
            }
        }


        public ActionResult ActionsForImproving(string? courseId)
        {
            List<ActionsForImprovingTheCourse> AFITC = _DatabaseEntities.ActionsForImprovingTheCourses.Where(x => x.CourseID.Equals(courseId)).ToList();
            ViewBag.courseId = courseId;
            return View(AFITC);
        }
        public ActionResult ActionsForImprovingDetails(string CourseID, System.DateTime Year, string Semseter)
        {
            ActionsForImprovingTheCourse AFITC = _DatabaseEntities.ActionsForImprovingTheCourses.Find(CourseID, Year, Semseter);
            return View(AFITC);
        }

        public ActionResult AddActionsForImproving(string courseId)
        {
            ActionsForImprovingTheCourse ActionsForImprovingTheCourse = _DatabaseEntities.ActionsForImprovingTheCourses.Find(courseId, yas.Year, yas.Semester);
            if (ActionsForImprovingTheCourse == null)
            {
                ActionsForImprovingTheCourse = new ActionsForImprovingTheCourse()
                {
                    CourseID = courseId,
                    Year = yas.Year,
                    Semseter = yas.Semester
                };
            }
            return View(ActionsForImprovingTheCourse);
        }

        [HttpPost]
        public ActionResult AddActionsForImproving(ActionsForImprovingTheCourse ActionsForImprovingTheCourse)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_DatabaseEntities.ActionsForImprovingTheCourses.Find(ActionsForImprovingTheCourse.CourseID, yas.Year, yas.Semester) == null)
                    {
                        _DatabaseEntities.ActionsForImprovingTheCourses.Add(ActionsForImprovingTheCourse);
                        _DatabaseEntities.SaveChanges();
                    }
                    else
                    {
                        _DatabaseEntities.Entry(ActionsForImprovingTheCourse).State = EntityState.Modified;
                        _DatabaseEntities.SaveChanges();

                    }
                    return RedirectToAction("CouresPage", "CourseCoordinator", ActionsForImprovingTheCourse.CourseID);
                }
                return View(ActionsForImprovingTheCourse);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(e.Message, "an error has accoured please try again later ");
                Console.WriteLine("Error at line 123 Course Coordinator");
                return View(ActionsForImprovingTheCourse);
            }
        }

        [HttpPost]
        public ActionResult IsAssessedPartialView(isAssessed isAssessed)
        {
            isAssessed a = _DatabaseEntities.isAssesseds.Find(isAssessed.CourseID, isAssessed.Year, isAssessed.Semseter);
            try
            {
                if (a == null)
                {
                    _DatabaseEntities.isAssesseds.Add(isAssessed);
                    _DatabaseEntities.SaveChanges();

                }
                else
                {
                    a.isAssessed1 = isAssessed.isAssessed1;
                    a.WhyNot = isAssessed.WhyNot;
                    _DatabaseEntities.Entry(a).State = EntityState.Modified;
                    _DatabaseEntities.SaveChanges();

                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(e.Message, "an error has accoured please try again later ");
                Console.WriteLine("Error at line 123 Course Coordinator");
                return View("Index", "LogedIn");
            }
            return RedirectToAction("CouresPage", "CourseCoordinator", isAssessed.CourseID);
        }

        [HttpPost]
        public ActionResult AssessmentPlan(AssessmentPlanforTheStudentLearningOutcomeTechnique A)
        {
            AssessmentPlanforTheStudentLearningOutcomeTechnique a = _DatabaseEntities.AssessmentPlanforTheStudentLearningOutcomeTechniques.Find(A.CourseID, A.Year, A.Semester);
            if (a == null)
            {
                _DatabaseEntities.AssessmentPlanforTheStudentLearningOutcomeTechniques.Add(A);
                _DatabaseEntities.SaveChanges();
            }
            else
            {
                a.Midterm_Test = A.Midterm_Test;

                a.Final_Exam = A.Final_Exam;

                a.Quiz = A.Quiz;

                a.Assignment = A.Assignment;

                a.project = A.project;

                a.Written_Report = A.Written_Report;

                a.Oral_Presenation = A.Oral_Presenation;

                a.Practice_In_The_Lab = A.Practice_In_The_Lab;

                a.Case_Studdy = A.Case_Studdy;

                a.Gropu_Discussions = A.Gropu_Discussions;

                a.Students_Interviews = A.Students_Interviews;

                a.Other = A.Other;

                _DatabaseEntities.Entry(a).State = EntityState.Modified;
                _DatabaseEntities.SaveChanges();
            }
            return RedirectToAction("CouresPage", new { courseID = A.CourseID });
        }


        public ActionResult CourseAssessmentSurvey()
        {
            CourseCoordinator cc = _DatabaseEntities.CourseCoordinators.Find("A0334501", yas.Year, yas.Semester);

            CourseAssessmentID a = new CourseAssessmentID()
            {
                CC = cc,
            };
            return View(a);
        }

        //todo: creat col itterator and configure the xlsx file that woul be uploaded
        public ActionResult UploadSurveyAnswers(FormCollection formCollection, string CourseID, DateTime Year, string Semseter)
        {
            List<int> QID = _DatabaseEntities.CourseAssessmentSurvays.Where(x => x.CourseID.Equals(CourseID) && x.Year.Equals(Year) && x.Semseter
            .Equals(Semseter)).Select(x => x.ID).ToList();

            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["Select Excel file"];
                if ((file != null) && (file.ContentLength != 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        var currentSheet = package.Workbook.Worksheets;
                        var workSheet = currentSheet.First();
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;

                        for (int colIterator = 1; colIterator <= QID.Count(); colIterator++)
                        {
                            int rowIterator = 2;
                            while (workSheet.Cells[rowIterator, (colIterator)].Value != null && !workSheet.Cells[rowIterator, (colIterator)].Value.ToString().Equals(""))
                            {
                                var answer = new AssessmentSurveyAnswer
                                {
                                    Answer = workSheet.Cells[rowIterator, colIterator].Value.ToString(),
                                    QID = QID[colIterator - 1]
                                };
                                try
                                {
                                    _DatabaseEntities.AssessmentSurveyAnswers.Add(answer);
                                    _DatabaseEntities.SaveChanges();
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Error at 235 CourseCoorddinatorController" + e);
                                }
                                rowIterator++;
                            }

                        }
                    }
                }
            }
            return RedirectToAction("CourseAssessmentSurvey", "CourseCoordinator", CourseID);
        }

        [HttpPost]
        public ActionResult AddSurveyQustion(CourseAssessmentSurvay CAS)
        {
            try
            {
                String SLOID = _DatabaseEntities.PIs.Where(x => x.ID.Equals(CAS.PI_ID) && x.DeptID.Equals(CAS.DeptID)).Select(x => x.SLOID).FirstOrDefault();
                CAS.SLOID = SLOID;
                _DatabaseEntities.CourseAssessmentSurvays.Add(CAS);
                _DatabaseEntities.SaveChanges();

            }
            catch (Exception e)
            {
                Console.WriteLine("Error at 255 CourseCoorddinatorController" + e);
            }
            return View();
        }


        //todo: create a Model for CourseStudent List
        public ActionResult CourseStudentList(string? CourseID, string? updateMassege, DateTime? Year, string? Semester, int? Department, string? Search, int page = 1, int pageSize = 10)
        {
            if (Year == null && Semester == null)
            {
                Year = yas.Year;
                Semester = yas.Semester;
            }
            //if (CourseID == null)
            //{
            //    return RedirectToAction("Index", "LogedIn");
            //}
            CourseCoordinator cc = _DatabaseEntities.CourseCoordinators.Find("A0334501", Year, Semester);

            if (cc == null)
            {
                return RedirectToAction("Index", "LogedIn");

            }
            PagedList<EnroledStudent> studentsPaged;
            List<EnroledStudent> CourseStudents;
            ViewBag.Search = Search;
            ViewBag.CourseID = CourseID;
            ViewBag.Year = Year;
            ViewBag.Semester = Semester;


            if ((Search == null || Search.Equals("")))
            {
                CourseStudents = cc.EnroledStudents.Where(x => x.Student.DptID.Equals(Department)).ToList();
                studentsPaged = new PagedList<EnroledStudent>(CourseStudents, page, pageSize);

            }
            else
            {
                CourseStudents = cc.EnroledStudents.Where(x => x.Student.ID.Equals(Search) || x.Student.Name.Contains(Search)).ToList();
                studentsPaged = new PagedList<EnroledStudent>(CourseStudents, page, pageSize);
            }
            return View(studentsPaged);
        }


        public ActionResult AddStudentListToCourse(FormCollection formCollection, string CourseID, DateTime Year, string Semseter)
        {
            int added = 0;
            int dicarded = 0;

            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["Select Excel file"];
                if ((file != null) && (file.ContentLength != 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        var currentSheet = package.Workbook.Worksheets;
                        var workSheet = currentSheet.First();
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;
                        CourseCoordinator cc = _DatabaseEntities.CourseCoordinators.Find(CourseID, Year, Semseter);
                        if (cc != null)
                        {
                            for (int rowIterator = 2; rowIterator < noOfRow; rowIterator++)
                            {
                                if (workSheet.Cells[rowIterator, 1].Value != null && workSheet.Cells[rowIterator, 2].Value != null && workSheet.Cells[rowIterator, 3].Value != null)
                                    try
                                    {
                                        var Student = _DatabaseEntities.Students.Find(workSheet.Cells[rowIterator, 1].Value.ToString());

                                        if (Student != null)
                                        {
                                            EnroledStudent e = new EnroledStudent()
                                            {
                                                StudentID = Student.ID,
                                                CourseID = cc.CourseID,
                                                Year = cc.Year,
                                                Semseter = cc.Semseter
                                            };
                                            _DatabaseEntities.EnroledStudents.Add(e);
                                            _DatabaseEntities.SaveChanges();

                                            added++;
                                        }
                                        else
                                        {
                                            dicarded++;

                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine("Error at 340 CourseCoorddinatorController" + e);
                                    }
                                rowIterator++;
                            }
                        }
                        else
                        {
                            return RedirectToAction("Index", "LogedIn");
                        }
                    }
                }
            }
            string updateMassege = (added + " student has been added to the Course.");
            return RedirectToAction("CourseStudentList", new { CourseID, updateMassege, Year, Semseter });
        }

        [HttpPost]
        public ActionResult RemoveStudentFromCourse(string? StudetnID, string? CourseID, DateTime? Year, string Semseter)
        {
            try
            {
                EnroledStudent s = _DatabaseEntities.EnroledStudents.Find(StudetnID, CourseID, Year, Semseter);
                if (s != null)
                {
                    _DatabaseEntities.EnroledStudents.Remove(s);
                    _DatabaseEntities.SaveChanges();
                }
                else
                {
                    return RedirectToAction("Index", "LogedIn");
                }
            }
            catch
            {
                return RedirectToAction("CourseStudentList", new { CourseID, Year, Semseter });
            }

            return RedirectToAction("CourseStudentList", new { CourseID, Year, Semseter });
        }


        // to do: read uploaded Files
        public ActionResult UploadGrades()
        {

            return RedirectToAction("CourseStudentList");
        }



        [HttpPost]
        public ActionResult AddBook(CourseTextBook courseTextBook)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _DatabaseEntities.CourseTextBooks.Add(courseTextBook);
                    _DatabaseEntities.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error at line 413 CourseCoordinator" + e.Message);

                    return RedirectToAction("CoursePage", courseTextBook.Course);
                }

            }
            if (courseTextBook.Course != null && !courseTextBook.Course.Equals(""))
                return RedirectToAction("CoursePage", courseTextBook.Course);
            return RedirectToAction("Index", "LogedIn");

        }


    }

}
