using LSS.Models;
using LSS.Models.CoursesModelView;
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

      
        // GET: LogedIn
        [Authorize]
        public ActionResult Index()
        {
            String userID = Session["ID"].ToString();
            String role = Session["Role"].ToString();
            String dpts = Session["Dpt"].ToString();
            int dpt = int.Parse(Session["Dpt"].ToString());
            HomeCourseListViewModel CLVM= new HomeCourseListViewModel(userID);

            return View(CLVM);
        }
    }
}