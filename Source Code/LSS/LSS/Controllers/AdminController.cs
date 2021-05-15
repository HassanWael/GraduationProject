using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LSS.Models;
namespace LSS.Controllers
{
   //[Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        LSS_databaseEntities _DatabaseEntities = new LSS_databaseEntities();
        // GET: Admin
        //ToDO :Create Index View for Admin.


        public ActionResult AddCourseToSemster( string CourseID)
        {
            CourseCoordinator cc = new CourseCoordinator();
            cc.CourseID = CourseID;
            return View(cc);
        }

        [HttpPost]
        public ActionResult AddCourseToSemster(CourseCoordinator cc )
        {
            try { 
            _DatabaseEntities.CourseCoordinators.Add(cc);
            _DatabaseEntities.SaveChanges();
            }
            catch
            {
                return View();
            }
            return RedirectToAction("Index");
        }
        public ActionResult AddCourseToSemester()
        {
            List<Course> courses = _DatabaseEntities.Courses.ToList();

            ViewBag.Courses = new SelectList(courses, "ID", "Title");
            return View();
        }
       
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateCourse()
        {
            return View();
        } 

        [HttpPost]
        public ActionResult CreateCourse(Course course )
        {
            _DatabaseEntities.Courses.Add(course);
            _DatabaseEntities.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CreatUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreatUser(Lecturer lecturer)
        {
            _DatabaseEntities.Lecturers.Add(lecturer);
            _DatabaseEntities.SaveChanges();

            return View();
        }

        public ActionResult CreatDpt()
        {
           
            return View();
        }
        [HttpPost]

        public ActionResult CreatDpt(Department department)
        {
            _DatabaseEntities.Departments.Add(department);
            _DatabaseEntities.SaveChanges();
            return View();
        }
        public ActionResult CreatFaculty()
        {
            return View();
        }
        [HttpPost]

        public ActionResult CreatFaculty(Faculty faculty)
        {
            _DatabaseEntities.Faculties.Add(faculty);
            _DatabaseEntities.SaveChanges();
            return View();
        }

        public ActionResult EditDpt(String id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(404);
                }
                Department d = _DatabaseEntities.Departments.Find(id);
                if (d == null)
                {
                    return new HttpStatusCodeResult(404);
                }
                else
                {
                    return View(d);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new HttpStatusCodeResult(404);
            }
        }
        [HttpPost]
        public ActionResult EditDpt(Department department)
        {
            _DatabaseEntities.Entry(department).State = EntityState.Modified;
            _DatabaseEntities.SaveChanges();
            return RedirectToAction("Index");
            
        }
        public ActionResult EditFaculty(String id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }
            Faculty faculty = _DatabaseEntities.Faculties.Find(id);
            if (faculty == null)
            {
                return new HttpStatusCodeResult(404);
            }
            else
            {
                return View(faculty);
            }
        }
        [HttpPost]
        public ActionResult EditFaculty(Faculty faculty)
        {
            _DatabaseEntities.Entry(faculty).State = EntityState.Modified;
            _DatabaseEntities.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CreateNewSemster()
        {
            Dictionary<string, string> semester = new Dictionary<string, string>();
            semester.Add("1", "First semester");
            semester.Add("2", "Second semester");
            semester.Add("3", "Third semester");

            ViewBag.semester = new SelectList(semester, "Key", "Value");
            return View();
        }
        [HttpGet]
        public ActionResult ListCourses(string? Search, int? Department)
        {
            if ((Search == null || Search=="" )&& Department == null)
            {
                List<Course> Courses = _DatabaseEntities.Courses.ToList();
                List<Department> departments = _DatabaseEntities.Departments.ToList();
                ViewBag.Department = new SelectList(departments, "ID", "Name");
               
            return View(Courses);
            }
            else if (Search == null || Search=="")
            {
                List<Course> Courses = _DatabaseEntities.Courses.Where(x=>x.dptid==Department).ToList();
                List<Department> departments = _DatabaseEntities.Departments.ToList();
                ViewBag.Department = new SelectList(departments, "ID", "Name");
                return View(Courses);
             
            }
            else if(Department == null)
            {
                List<Course> Courses = _DatabaseEntities.Courses.Where(x=> x.Title.ToLower().Contains(Search.ToLower()) || x.ID.ToLower().Contains(Search.ToLower())).ToList();
                List<Department> departments = _DatabaseEntities.Departments.ToList();
                ViewBag.Department = new SelectList(departments, "ID", "Name");
                return View(Courses);
            }
            else
            {
                List<Course> Courses = _DatabaseEntities.Courses.Where(x=> x.dptid.Equals(Department)&& (x.Title.ToLower().Contains(Search.ToLower())||x.ID.ToLower().Contains(Search.ToLower()))).ToList();
                List<Department> departments = _DatabaseEntities.Departments.ToList();
                ViewBag.Department = new SelectList(departments, "ID", "Name");
                return View(Courses);
            }
        }

    }
}