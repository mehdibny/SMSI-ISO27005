using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMSI_ISO27005.Models;

namespace SMSI_ISO27005.Controllers
{
    public class ActionMesureController : Controller
    {
        SMSIEntities1 db = new SMSIEntities1();
        // GET: ActionMesure
        public ActionResult Index()
        {
            return View();
        }

        // GET: ActionMesure/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ActionMesure/Create
        public ActionResult CreateActionMesure()
        {
            
            
            ViewBag.chapitre = new SelectList(db.action_mesure.Select(a=>a.chapitre).Distinct());
            
            ViewBag.objects = new SelectList(db.action_mesure.Select(a => a.objects).Distinct());

            ViewBag.mesures = new SelectList(db.action_mesure.Select(a => a.mesures).Distinct());
            

            return View();
        }
        public JsonResult GetObjects(string chapitre)
        {
            
            var objectlist = db.action_mesure.Where(a => a.chapitre == chapitre).Select(a=>a.objects).Distinct();
            return Json(objectlist.AsEnumerable().ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMesures(string objects)
        {

            var objectlist = db.action_mesure.Where(a => a.objects == objects).Select(a => a.mesures).Distinct();
            return Json(objectlist.AsEnumerable().ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDescription(string mesures)
        {

            var objectlist = db.action_mesure.Where(a => a.mesures == mesures);
            return Json(objectlist.AsEnumerable().ToList(), JsonRequestBehavior.AllowGet);
        }
        // POST: ActionMesure/Create
        [HttpPost]
        public ActionResult CreateActionMesure(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ActionMesure/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ActionMesure/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ActionMesure/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ActionMesure/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
