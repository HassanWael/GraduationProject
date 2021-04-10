using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LSS.Controllers
{
    public class HomeController : Controller
    {
    
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult CouresPage() {

            ViewBag.Message = " Coures view Page";
            return View();
        }
    }
}