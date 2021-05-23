using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        private readonly  LSS_databaseEntities _databaseEntities = new LSS_databaseEntities();
        // GET: HeadOfDepartment
        //todo remove = 2 
        public ActionResult Index(int? dptID=2 )
        {

            Department d = _databaseEntities.Departments.Find(dptID);
            return View(d);
        }


        [HttpPost]
        public ActionResult EditSLO(AddSLOMV md,String[] PEOID)
        {
            if (ModelState.IsValid)
            {
                List<PEO> PEOs = md.PEOs.ToList();
                _databaseEntities.Entry(md.SLO).State = EntityState.Modified;
                _databaseEntities.SaveChanges();
                List<SLO_PEO> sLO_PEOs = _databaseEntities.SLO_PEO.Where(x => x.SLOID.Equals(md.SLO.SLOID) && x.DeptID.Equals(md.SLO.DeptID)).ToList();
                foreach (PEO peo in PEOs)
                {
                    SLO_PEO SLO_PEO = new SLO_PEO();
                    if (PEOID != null && PEOID.Contains(peo.ID))
                    {
                        if (!sLO_PEOs.Select(x => x.PEOID).Contains(peo.ID)) { 
                            SLO_PEO.SLOID = md.SLO.SLOID;
                            SLO_PEO.DeptID = md.SLO.DeptID;
                            SLO_PEO.PEOID = peo.ID;
                            _databaseEntities.SLO_PEO.Add(SLO_PEO);
                            _databaseEntities.SaveChanges();
                        }
                    }
                    else
                    {
                        if (sLO_PEOs.Select(x => x.PEOID).Contains(peo.ID))
                        {

                            SLO_PEO = _databaseEntities.SLO_PEO.Single(i => i.DeptID.Equals(md.SLO.DeptID)
                                    && i.SLOID.Equals(md.SLO.SLOID)
                                    && i.PEOID.Equals(peo.ID));
                            _databaseEntities.SLO_PEO.Remove(SLO_PEO);
                            _databaseEntities.SaveChanges();
                        }
                    }
                }
                return RedirectToAction("Index", md.SLO.DeptID);
            }



            return View(md);
        }


        [HttpPost]
        public ActionResult AddPI(PI pi)
        {
            _databaseEntities.PIs.Add(pi);
            _databaseEntities.SaveChanges();

            return RedirectToAction("index", pi.DeptID);
        }

        [HttpPost]
        //  [Authorize(Roles = "HOD,Dean,ViceDean")]
        public ActionResult AddSLO(AddSLOMV md, String[] PEOID)
        {
            if (ModelState.IsValid)
            {
                _databaseEntities.SLOes.Add(md.SLO);
                _databaseEntities.SaveChanges();
                SLO_PEO SLO_PEO = new SLO_PEO();

                foreach (String str in PEOID)
                {
                    SLO_PEO.SLOID = md.SLO.SLOID;
                    SLO_PEO.DeptID = md.SLO.DeptID;
                    SLO_PEO.PEOID = str;
                    _databaseEntities.SLO_PEO.Add(SLO_PEO);
                }
                _databaseEntities.SaveChanges();
                return RedirectToAction("index", new { d = md.SLO.DeptID });
            }
            return View(md);
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
    }
}