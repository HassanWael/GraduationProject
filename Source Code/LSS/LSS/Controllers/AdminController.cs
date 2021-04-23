using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LSS.Models;
namespace LSS.Controllers
{
   // [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        LSS_databaseEntities _DatabaseEntities = new LSS_databaseEntities();
        // GET: Admin
        //ToDO :Create Index View for Admin.


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

    }
}