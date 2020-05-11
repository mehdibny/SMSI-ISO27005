using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SMSI_ISO27005.Models;

namespace SMSI_ISO27005.Controllers
{
    public class ActionsController : Controller
    {
        private SMSIEntities1 db = new SMSIEntities1();

        // GET: Actions
        public ActionResult Index()
        {
            var action = db.action.Include(a => a.gestion_risque).Include(a => a.action_mesure);
            return View(action.ToList());
        }

        // GET: Actions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            action action = db.action.Find(id);
            if (action == null)
            {
                return HttpNotFound();
            }
            return View(action);
        }

        // GET: Actions/Create
        public ActionResult Create()
        {
            ViewBag.id_gestion_risk = new SelectList(db.gestion_risque, "id_gestion_risk", "nom_gesion_risk");
            ViewBag.id_mesure = new SelectList(db.action_mesure, "id_mesure", "chapitre");
            return View();
        }

        // POST: Actions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_action,nom_action,description_action,échéance,etat_avancement,commentaire,matricule_responable,id_gestion_risk,date_creation,id_mesure")] action action)
        {
            if (ModelState.IsValid)
            {
                db.action.Add(action);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_gestion_risk = new SelectList(db.gestion_risque, "id_gestion_risk", "nom_gesion_risk", action.id_gestion_risk);
            ViewBag.id_mesure = new SelectList(db.action_mesure, "id_mesure", "chapitre", action.id_mesure);
            return View(action);
        }

        // GET: Actions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            action action = db.action.Find(id);
            if (action == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_gestion_risk = new SelectList(db.gestion_risque, "id_gestion_risk", "nom_gesion_risk", action.id_gestion_risk);
            ViewBag.id_mesure = new SelectList(db.action_mesure, "id_mesure", "chapitre", action.id_mesure);
            return View(action);
        }

        // POST: Actions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_action,nom_action,description_action,échéance,etat_avancement,commentaire,matricule_responable,id_gestion_risk,date_creation,id_mesure")] action action)
        {
            if (ModelState.IsValid)
            {
                db.Entry(action).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_gestion_risk = new SelectList(db.gestion_risque, "id_gestion_risk", "nom_gesion_risk", action.id_gestion_risk);
            ViewBag.id_mesure = new SelectList(db.action_mesure, "id_mesure", "chapitre", action.id_mesure);
            return View(action);
        }

        // GET: Actions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            action action = db.action.Find(id);
            if (action == null)
            {
                return HttpNotFound();
            }
            return View(action);
        }

        // POST: Actions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            action action = db.action.Find(id);
            db.action.Remove(action);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
