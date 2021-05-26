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
    [Authorize]
    public class LogedInController : Controller
    {
       private readonly LSS_databaseEntities _databaseEntities = new LSS_databaseEntities();

      
        // GET: LogedIn
        [Authorize]
        public ActionResult Index()
        {

            String userID = User.Identity.Name;
          
            HomeCourseListViewModel CLVM= new HomeCourseListViewModel(userID);

            return View(CLVM);
        }
    }
}