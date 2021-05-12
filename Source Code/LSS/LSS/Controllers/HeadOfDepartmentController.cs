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
        private  LSS_databaseEntities _databaseEntities = new LSS_databaseEntities();
        // GET: HeadOfDepartment
        public ActionResult Index(int? dptID )
        {
            if(dptID != null)
            {
                DptID = (int)dptID;
            }
            else
            {
                DptID = 2;
            }
            Department d = _databaseEntities.Departments.Find(2);
            return View(d);
        }
        public ActionResult MapSLOToPEO(int? DeptID)
        {
            //if (DeptID == null)
            //{
            //    return RedirectToAction("Index", "LogedIn");
            //}

            Department d = _databaseEntities.Departments.Find(2);

            //if (d == null)
            //{
            //    return RedirectToAction("Index", "LogedIn");
            //}


            return View(d);
        }
        [HttpPost]
        public ActionResult MapSLOToPEO(int ID, string [] PEOID,string SLOID)
        {
            
            SLO_PEO SLO_PEO = new SLO_PEO();
            foreach(String peo in PEOID) { 
                SLO_PEO.SLOID = SLOID;
                SLO_PEO.DeptID = ID;
                SLO_PEO.PEOID = peo;
                _databaseEntities.SLO_PEO.Add(SLO_PEO);
                _databaseEntities.SaveChanges();
            }
            return RedirectToAction("Index", "HeadOfDepartment", ID);
        }


        //  [Authorize(Roles = "HOD,Dean,ViceDean")]
        [HttpPost]
        public ActionResult AddPEO(PEO peo)
        {
            if (ModelState.IsValid)
            {
                _databaseEntities.PEOs.Add(peo);
                _databaseEntities.SaveChanges();
                return RedirectToAction("index", new { d = peo.DeptID });
            }
            return View();
        }
//        [Authorize(Roles = "HOD,Dean,ViceDean")]
        [HttpPost]
      //  [Authorize(Roles = "HOD,Dean,ViceDean")]
        public ActionResult AddSLO(SLO slo)
        {
            if (ModelState.IsValid)
            {
                _databaseEntities.SLOes.Add(slo);
                _databaseEntities.SaveChanges();
                return RedirectToAction("index", new { d = slo.DeptID });
            }

            return View(slo);
        }
      
        public ActionResult addPI(String SLOID,String DptID)
        {
            return View();
        }

       


    }
}