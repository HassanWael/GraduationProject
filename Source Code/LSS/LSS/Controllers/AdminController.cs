using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LSS.Models;
using LSS.Models.arc;

namespace LSS.Controllers
{
   //[Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        readonly LSS_databaseEntities _DatabaseEntities = new LSS_databaseEntities();
        // GET: Admin
        //ToDO :Create Index View for Admin.
        readonly YearAndSemester YAS = SemesterSingelton.getCurrentYearAndSemester();

        public ActionResult AddCourseToSemester( string CourseID)
        {
            CourseCoordinator cc;
            if (_DatabaseEntities.CourseCoordinators.Find(CourseID, YAS.Year, YAS.Semester) == null)
            {
                cc = new CourseCoordinator
                {
                    CourseID = CourseID,
                    Year = YAS.Year,
                    Semseter = YAS.Semester
                };
            }
            else
            {
                cc = _DatabaseEntities.CourseCoordinators.Find(CourseID, YAS.Year, YAS.Semester);
            }
            int deptID = _DatabaseEntities.Courses.Where(x => x.ID.Equals(CourseID)).Select(x => x.dptid).FirstOrDefault();
            List<Lecturer> CC = _DatabaseEntities.Lecturers.Where(x => x.dptId.Equals(deptID)).ToList();
            ViewBag.Lecturers = new SelectList(CC, "ID", "Name");

            return View(cc);
        }

        [HttpPost]
        public ActionResult AddCourseToSemester(CourseCoordinator cc )
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (_DatabaseEntities.CourseCoordinators.Find(cc.CourseID, cc.Year, cc.Semseter) == null)
                    {
                        _DatabaseEntities.CourseCoordinators.Add(cc);
                        _DatabaseEntities.SaveChanges();
                    }
                    else
                    {
                        _DatabaseEntities.Entry(cc).State = EntityState.Modified;
                        _DatabaseEntities.SaveChanges();

                    }
                }
                catch(Exception e )
                {
                    ModelState.AddModelError("An errorr Has Acoured please try again later", e );
                    Console.WriteLine("Error at the Line 48 of AdminController : "+e.Message);
                    return View();
                }
                return RedirectToAction("Index");
            }
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
            try {
                if (_DatabaseEntities.Courses.Find(course.ID) != null)
                {
                    _DatabaseEntities.Courses.Add(course);
                    _DatabaseEntities.SaveChanges();
                    return RedirectToAction("Index");
                }
                else {
                    ModelState.AddModelError("Dublicate Value", "This Course is already in the database");
                    return View();
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Error", "An errorr Has Acoured please try again later");
                Console.WriteLine("Error at the Line 82 of AdminController : " + e.Message);
                return View();
            }
        }


        public ActionResult CreatUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreatUser(Lecturer lecturer)
        {
            try
            {
                if (_DatabaseEntities.Lecturers.Find(lecturer.ID)==null) {
                    if (ModelState.IsValid)
                    {
                        _DatabaseEntities.Lecturers.Add(lecturer);
                        _DatabaseEntities.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    return View();
                }
                else{

                    ModelState.AddModelError("Dublicate Value", " lecturer ID is already in the database");
                    return View();
                }
            }
            catch(Exception e )
            {
                ModelState.AddModelError("Error", "An errorr Has Acoured please try again later");
                Console.WriteLine("Error at the Line 82 of AdminController : " + e.Message);
                return View();
            }
        }

        public ActionResult CreatDpt()
        {
           
            return View();
        }
        [HttpPost]

        public ActionResult CreatDpt(Department department)
        {
            try
            {
                if (_DatabaseEntities.Departments.Find(department.ID) == null)
                {
                    if (ModelState.IsValid)
                    {
                        _DatabaseEntities.Departments.Add(department);
                        _DatabaseEntities.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    return View();

                }
                else
                {
                    ModelState.AddModelError("Dublicate Value", " department ID is already in the database");
                    return View();
                }

            }
            catch(Exception e)
            {
                ModelState.AddModelError("Error", "An errorr Has Acoured please try again later");
                Console.WriteLine("Error at the Line 82 of AdminController : " + e.Message);
                return View();
            }
      
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
            Dictionary<string, string> semester = new Dictionary<string, string>
            {
                { "1", "First semester" },
                { "2", "Second semester" },
                { "3", "Third semester" }
            };

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