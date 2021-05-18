using LSS.Models;
using LSS.Models.arc;
using LSS.Models.CoursesModelView;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

    }

}