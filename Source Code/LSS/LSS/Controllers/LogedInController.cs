using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LSS.Models.LSS_DB_Model;
namespace LSS.Controllers
{
    
    public class LogedInController : Controller
    {
        public LSS_databaseEntities _dbEntities = new LSS_databaseEntities();
      
        [HttpPost]
        public ActionResult Index()
        {
            String userID = Session["ID"].ToString();
            String role = Session["Role"].ToString();
            String dpt = Session["Dpt"].ToString();
            // dynamic model {is a workaround the restriction of having to bass only one model}
              dynamic mymodel = new ExpandoObject();
            // need to creat list of things I want to pass to the model 
            // First we pass all the Courses that the user teach 
            //mymodel.Courses = GetCourses();

            var courses = _dbEntities.Courses.Join(_dbEntities.CourseCoordinators,
                c => c.ID,
                cc => cc.CourseID,
                (c, CC) => new
                {
                    Course = c,
                    CourseCoordinator = CC
                }).Where(O => O.CourseCoordinator.Cordenator ==userID).Select(t => t.Course).OrderBy(x => x.Title);
            mymodel.Coordinator = courses.ToList();

            
            if (role == "Head of Department")
            {
                var courses1 = _dbEntities.Courses
                     .Where(c => c.DptID
                     .Equals(dpt));
                mymodel.DptCourses = courses1.ToList();

            }
            else if (role == "Dean" || role == "Vice Dean")
            {
                var Courses2 = _dbEntities.Courses.Join(_dbEntities.Departments,
                    c => c.DptID,
                    d => d.ID,
                    (c, d) => new
                    {
                        Course = c,
                        Department = d
                    }).Where(o => o.Department.FacultyID == (_dbEntities.Departments.Where
                    (s=>s.ID.Equals(dpt)).Select(a=>a.FacultyID).FirstOrDefault()))
                    .Select(t => t.Course).OrderBy(x => x.Title);
                mymodel.FacultyCourses = Courses2.ToList();
            }
            return View(mymodel);
        }
    }
}