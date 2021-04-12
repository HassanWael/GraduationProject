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
        public ActionResult Index()
        {
            //String UserID = Session["ID"].ToString();

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
                }).Where(O => O.CourseCoordinator.Cordenator == "0001").Select(t => t.Course).OrderBy(x => x.Title);
            mymodel.Coordinator = courses.ToList();
            return View(mymodel);
        }
    }
}