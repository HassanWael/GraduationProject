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
        private int DptID;
        private readonly LSS_databaseEntities _databaseEntities = new LSS_databaseEntities();
        // GET: HeadOfDepartment
        public ActionResult Index(int dptID )
        {
            DptID = dptID;
            Department d = _databaseEntities.Departments.Find(dptID);
            return View();
        }
      //  [Authorize(Roles = "HOD,Dean,ViceDean")]
        public ActionResult AddPEO(int dptID)
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
        public ActionResult AddSLO(int dptID)
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
        public ActionResult _PEO_SLO_Mapping(int dptID)
        {
            DptID = dptID;
            MappedPEO_SLO mapped = new MappedPEO_SLO(dptID);
            
            return View(mapped);
        }
        public ActionResult addPI(String SLOID,String DptID)
        {
            return View();
        }

        public ActionResult _ViewUnmappedPEO(MappedPEO_SLO m)
        {
            return View(m);
        }
    }
}