using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LSS.Models;


namespace LSS.Controllers
{
    public class SectionsController : Controller
    {
        // GET: Sections
        public ActionResult ListSections()
        {



            return View();
        }


        public ActionResult CreatSection()
        {

            return View();

        }

        [HttpPost]
        public ActionResult CreatSection(OtherLecturer otherLecturer)
        {

            return View();

        }
    }
}