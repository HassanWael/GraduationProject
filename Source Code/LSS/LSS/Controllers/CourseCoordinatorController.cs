using LSS.Models;
using LSS.Models.arc;
using LSS.Models.CoursesModelView;
using OfficeOpenXml;
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
        public ActionResult CouresPage(string? courseID)
        {
            if (courseID == null)
            {
                RedirectToAction("Index", "LogedIN");
            }
             
            //String userID = Session["ID"].ToString();
            CourseCoordinator cc = _DatabaseEntities.CourseCoordinators.Find("A0334501", yas.Year, yas.Semester);
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


        public ActionResult ActionsForImproving(string courseId)
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
            try {
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
                    return RedirectToAction("CouresPage","CourseCoordinator", ActionsForImprovingTheCourse.CourseID);
                }
                return View(ActionsForImprovingTheCourse);
            }
            catch (Exception e )
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
                }
                else
                {
                    _DatabaseEntities.Entry(isAssessed).State = EntityState.Modified;
                }
                _DatabaseEntities.SaveChanges();
            }
            catch (Exception e)
            {
                ModelState.AddModelError(e.Message, "an error has accoured please try again later ");
                Console.WriteLine("Error at line 123 Course Coordinator");
                return View(isAssessed);
            }
            return RedirectToAction("CouresPage", "CourseCoordinator", isAssessed.CourseID);
        }

        [HttpPost]
        public ActionResult AssessmentPlan()
        {
            return View();

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
        public ActionResult UploadSurveyAnswers( FormCollection formCollection ,string CourseID , DateTime Year , string Semseter)
        {
            List<int> QID = _DatabaseEntities.CourseAssessmentSurvays.Where(x => x.CourseID.Equals(CourseID)&&x.Year.Equals(Year)&&x.Semseter
            .Equals(Semseter)).Select(x => x.ID).ToList();

            if (Request != null)
            {               
                HttpPostedFileBase file = Request.Files["Select Excel file"];
                if ((file != null) && (file.ContentLength != 0) && !string.IsNullOrEmpty(file.FileName))   
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    var AssessmentSurveyAnswers = new List<AssessmentSurveyAnswer>();
                    
                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        var currentSheet = package.Workbook.Worksheets;
                        var workSheet = currentSheet.First();
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;

                        for (int colIterator = 1; colIterator <= QID.Count(); colIterator++)
                        {
                            int rowIterator = 2; 
                            while (workSheet.Cells[rowIterator, (colIterator )].Value != null && !workSheet.Cells[rowIterator, (colIterator)].Value.ToString().Equals("")  )
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
                String SLOID = _DatabaseEntities.PIs.Where(x => x.ID.Equals(CAS.PI_ID) && x.DeptID.Equals(CAS.DeptID)).Select(x=>x.SLOID).FirstOrDefault();
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


    }


}