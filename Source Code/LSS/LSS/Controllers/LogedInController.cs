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


        // GET: LogedIn
        [Authorize]
        public ActionResult Index()
        {
            String userID = Session["ID"].ToString();
            HomeCourseListViewModel CLVM= new HomeCourseListViewModel(userID);
            return View(CLVM);
        }
    }
}
