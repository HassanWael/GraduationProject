using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LSS.Models;
namespace LSS.Controllers
{
    public class HeadOfDepartmentController : Controller
    {
        // GET: HeadOfDepartment
        
        public ActionResult Index(Department d )
        {
            return View( d );
        }

    }
}