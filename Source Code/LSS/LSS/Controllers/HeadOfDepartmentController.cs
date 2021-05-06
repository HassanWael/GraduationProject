using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LSS.Models;
using LSS.Models.DepartmentViewModel;

namespace LSS.Controllers
{
    public class HeadOfDepartmentController : Controller
    {
        private String DptID;
        private readonly LSS_databaseEntities _databaseEntities = new LSS_databaseEntities();
        // GET: HeadOfDepartment
        public ActionResult Index(String dptID )
        {
            DptID = dptID;
            Department d = _databaseEntities.Departments.Find(dptID);
            return View();
        }
      //  [Authorize(Roles = "HOD,Dean,ViceDean")]
        public ActionResult AddPEO(String dptID)
        {
            DptID = dptID;

            ViewBag.DeptID = dptID;
            return View();
        }
        [HttpPost]
       // [Authorize(Roles = "HOD,Dean,ViceDean")]
        public ActionResult AddPEO(PEO peo)
        {
            _databaseEntities.PEOs.Add(peo);
            _databaseEntities.SaveChanges();
            return RedirectToAction("index", new { d = peo.DeptID });
        }
//        [Authorize(Roles = "HOD,Dean,ViceDean")]
        public ActionResult AddSLO(String dptID)
        {
            DptID = dptID;
            ViewBag.DeptID = dptID;
            return View();
        }
        [HttpPost]
      //  [Authorize(Roles = "HOD,Dean,ViceDean")]
        public ActionResult AddSLO(SLO slo)
        {
            slo.DeptID = DptID;
            _databaseEntities.SLOes.Add(slo);
            _databaseEntities.SaveChanges();
            return RedirectToAction("index", new { d = slo.DeptID });
        }
        public ActionResult _PEO_SLO_Mapping(String dptID)
        {
            DptID = dptID;
            MappedPEO_SLO mapped = new MappedPEO_SLO(dptID);
            
            return View(mapped);
        }
        public ActionResult _ViewUnmappedPEO(MappedPEO_SLO m)
        {
            return View(m);
        }
    }
}