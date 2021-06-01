using LSS.Models;
using LSS.Models.arc;
using LSS.Models.CoursesModelView;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
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



        public ActionResult DirectAssessmentPI(string? CourseID, DateTime? Year, string? Semester)
        {
            if (CourseID == null || Year == null || Semester == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            PIAssessmentMV assessment = new PIAssessmentMV
            {
                CourseCoordinator = _DatabaseEntities.CourseCoordinators.Find(CourseID, Year, Semester)
            };
            if (assessment == null)
            {
                return HttpNotFound();
            }
            return View(assessment);
        }


        public ActionResult DirectAssessmentPI(PIAssessmentMV? pIAssessment, List<int> QID)
        {
            CourseCoordinator cc = pIAssessment.CourseCoordinator;
            List<int> everyCourseQ = pIAssessment.AllQustions;


            if (ModelState.IsValid)
            {
                if (QID != null)
                {

                }
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRCDA(ResultOfCourseDirectAssessment RCDA)
        {
            if(_DatabaseEntities.ResultOfCourseDirectAssessments.Find(RCDA.CourseID,RCDA.Year, RCDA.Semseter)!=null)
            {
                _DatabaseEntities.Entry(RCDA).State = EntityState.Modified;
                _DatabaseEntities.SaveChanges();
            }
            else
            {
                _DatabaseEntities.ResultOfCourseDirectAssessments.Add(RCDA);
                _DatabaseEntities.SaveChanges();

            }
            return View();
        }





        public ActionResult DirectAssessment(string? CourseID, DateTime? Year, string? Semester)
        {
            CourseCoordinator cc = _DatabaseEntities.CourseCoordinators.Find(CourseID, Year, Semester);

            return View(cc);
        }


        public ActionResult CourseSchedule(string? CourseID, DateTime? Year, string? Semester)
        {
            if (CourseID == null || Year == null || Semester == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<Schedule> schedules = _DatabaseEntities.Schedules.Where(x => x.CourseID.Equals(CourseID) &&
              x.Year.Equals((DateTime)Year) && x.Semseter.Equals(Semester)).ToList();
            if (!schedules.Any())
            {
                for (int i = 0; i < 17; i++)
                {
                    Schedule schedule = new Schedule();
                    schedule.CourseID = CourseID;
                    schedule.Year = (DateTime)Year;
                    schedule.Semseter = Semester;
                    schedule.WeekNumber = i+1;
                    _DatabaseEntities.Schedules.Add(schedule);
                    _DatabaseEntities.SaveChanges();
                    schedules.Add(schedule);
                }
            }
            return View(schedules);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditWeek(Schedule s,int[] CLO_ID)
        {
            Schedule schedule = _DatabaseEntities.Schedules.Find(s.CourseID, s.Year, s.Semseter,s.WeekNumber);
            if(schedule == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            schedule.Assignments = s.Assignments;
            schedule.Topic = s.Topic;
            schedule.Reference = s.Reference;

            List<CLO> allCLOes = _DatabaseEntities.CLOes.Where(x => x.courseId.Equals(s.CourseID)).ToList();

            foreach (CLO cloid in allCLOes)
            {
                if (schedule.CLOes.Select(x => x.ID).Contains(cloid.ID))
                {
                    if (!CLO_ID.Contains(cloid.ID))
                    {
                        schedule.CLOes.Remove(cloid);
                    }
                }
                else
                {
                    if (CLO_ID.Contains(cloid.ID))
                    {
                        schedule.CLOes.Add(cloid);

                    }
                }
                
            }

            _DatabaseEntities.Entry(schedule).State = EntityState.Modified;
            _DatabaseEntities.SaveChanges();


            return RedirectToAction("CourseSchedule", new { s.CourseID, s.Year, Semester = s.Semseter });
        }


    }
}