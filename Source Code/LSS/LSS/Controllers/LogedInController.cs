using LSS.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LSS.Controllers
{
    public class LogedInController : Controller
    {
       private readonly LSS_databaseEntities _databaseEntities = new LSS_databaseEntities();
        
        public ActionResult CouresPage()
        {

            ViewBag.Message = "Coures view Page";
            return View();
        }
        /// <summary>
        /// Once the User login this is the home page where he will use to navigate 
        /// </summary>
        /// <returns>
        /// the View Index with the Dynamic model that contain all the coureses that user should have access to 
        /// </returns>
       // [Authorize]
        // GET: LogedIn
        public ActionResult Index()
        {
            String userID = Session["ID"].ToString();
            String role = Session["Role"].ToString();
            String dpt = Session["Dpt"].ToString();

            //dynamic model (Workaround the fact that I cant pass more than one Model or list)
            dynamic myModel = new ExpandoObject();
            // after initializing the dynamic object we create a lists 
            myModel.coordinator = myModel.DptCourses = myModel.FacultyCourses = null;
           // grap all the coursess that the user is a coordinator of 
            var courses = _databaseEntities.Courses.Join(_databaseEntities.CourseCoordinators,
                course => course.ID,
                courseCoordinator => courseCoordinator.CourseID,
                (course, courseCoordinator) => new
                {
                    Course = course,
                    CourseCoordinator = courseCoordinator
                }).Where(x => x.CourseCoordinator.Coordinator == userID).Select(x => x.Course).OrderBy(course => course.Title);
            myModel.coordinator = courses.ToList();
            if (role == "HeadOfDepartment")
            {
                var courses1 = _databaseEntities.Courses.Where(course => course.DptID.Equals(dpt));
                myModel.DptCourses = courses1.ToList();
            }
            else if(role.Equals("Dean") || role.Equals("Vice Dean"))
            {
                var courses2 = _databaseEntities.Courses.Join(_databaseEntities.Departments,
                    course => course.DptID,
                    department => department.ID,
                    (course, department) => new
                    {
                        Course = course,
                        Department = department
                    }).Where(o => o.Department.FacultyID.Equals(_databaseEntities.Departments.Where(
                        s => s.ID.Equals(dpt)).Select(a => a.FacultyID).FirstOrDefault())).Select(x => x.Course).OrderBy(course => course.Title);
                myModel.FacultyCourses = courses2.ToList();
            }
            return View(myModel);
        }
    }
}