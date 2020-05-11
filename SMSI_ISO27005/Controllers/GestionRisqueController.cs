using PagedList;
using PagedList.Mvc;
using SMSI_ISO27005.Models;
using SMSI_ISO27005.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMSI_ISO27005.Controllers
{
    public class GestionRisqueController : Controller
    {
        private SMSIEntities1 db = new SMSIEntities1();
        // GET: GestionRisque
        public ActionResult Index(string search, int? i, int id = 0)
        {
            SMSIEntities1 db = new SMSIEntities1();

            List<actif> actifNom = db.actif.ToList();
            List<vulnerabilte> vulneNom = db.vulnerabilte.ToList();
            List<gestion_risque> risqueNom = db.gestion_risque.ToList();
            List<action> actionNom = db.action.ToList();
            var querry = from risk in risqueNom

                         join vul in vulneNom on risk.id_vulne equals vul.id_vulne
                         into vulTable
                         from vul in vulTable.DefaultIfEmpty()

                         join af in actifNom on vul.id_actif equals af.id_actif
                         into afTable
                         from af in afTable.DefaultIfEmpty()

                         join act in actionNom on risk.id_gestion_risk equals act.id_gestion_risk
                         into actTable
                         from act in actTable.DefaultIfEmpty()
                         
                         select new CIDActifVM
                         {
                             actifDetailles = af,
                             vulnerabilteDetailles = vul,
                             gestionDetailles = risk,
                             actionDetailles =act
                         };
                return View(querry.OrderBy(x=>x.gestionDetailles.id_vulne).ToList().ToPagedList(i ?? 1, 7));
            //.GroupBy(x => x.gestionDetailles.id_vulne)
            /*.Where(x => x.vulnerabilteDetailles.id_vulne == id)*/
        }

        // GET: GestionRisque/Details/5
        public ActionResult ActionDatails(int id = 0)
        {
            using (SMSIEntities1 db = new SMSIEntities1())
            {
                return View(db.action.Where(x => x.id_action == id).FirstOrDefault());

            }


        }
        public ActionResult ActionEdit(int id = 0)
        {
            using (SMSIEntities1 db = new SMSIEntities1())
            {
                return View(db.action.Where(x => x.id_action == id).FirstOrDefault());

            }
        }
        public ActionResult RiskDatails(int id = 0)
        {
            using (SMSIEntities1 db = new SMSIEntities1())
            {
                return View(db.gestion_risque.Where(x => x.id_gestion_risk == id).FirstOrDefault());

            }


        }

        // GET: GestionRisque/Create
        public ActionResult Create()
        {
            
            ViewBag.chapitre = new SelectList(db.action_mesure.Select(a => a.chapitre).Distinct().ToList());

            ViewBag.objects = new SelectList(db.action_mesure.Select(a => a.objects).Distinct());

            ViewBag.mesures = new SelectList(db.action_mesure.Select(a => a.mesures).Distinct());
            List<vulnerabilte> listVuln = db.vulnerabilte.ToList();
            ViewBag.vulnList = new SelectList(listVuln, "id_vulne", "nom_vulne");
            return View();
        }
        public JsonResult GetObjects(string chapitre)
        {

            var objectlist = db.action_mesure.Where(a => a.chapitre == chapitre).Select(a => a.objects).Distinct();
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
        // POST: GestionRisque/Create
        [HttpPost]
        public ActionResult Create(CIDActifVM model)
        {
            try
            {
                // TODO: Add insert logic here
                SMSIEntities1 db = new SMSIEntities1();
                List<gestion_risque> risqueNom = db.gestion_risque.ToList();

                //Vulnerabilite DropDownList
                List<vulnerabilte> listVuln = db.vulnerabilte.ToList();
                ViewBag.vulnList = new SelectList(listVuln, "id_vulne", "nom_vulne");

                //Insert Into Gestion Risque Table
                gestion_risque risk = new gestion_risque();
                risk.id_gestion_risk = model.gestionDetailles.id_gestion_risk;
                risk.nom_gesion_risk = model.gestionDetailles.nom_gesion_risk;
                risk.description_gestion_risk = model.gestionDetailles.description_gestion_risk;
                risk.date_creation_g_risk = DateTime.Now;
                risk.risk_dicision = model.gestionDetailles.risk_dicision;
                risk.id_vulne = model.vulnerabilteDetailles.id_vulne;
                //risk.errorMessage = "Done";
                db.gestion_risque.Add(risk);
                //db.SaveChanges();

                int LastRiskID = risk.id_gestion_risk;

                //Insert Into Action Table
                action act = new action();
                act.id_action = model.actionDetailles.id_action;
                act.nom_action = model.actionDetailles.nom_action;
                act.description_action = model.actionDetailles.description_action;
                act.échéance = model.actionDetailles.échéance;
                act.etat_avancement = model.actionDetailles.etat_avancement;
                act.commentaire = model.actionDetailles.commentaire;
                act.matricule_responable = Session["UserMatricule"].ToString();
                act.id_gestion_risk = LastRiskID;
                act.date_creation = DateTime.Now;
                act.id_mesure = model.actionDetailles.id_mesure;
                //act.errorMessage = "Done";
                db.action.Add(act);
                //db.SaveChanges();

                //if (act.errorMessage == "Done" && risk.errorMessage == "Done")
                //{
                var vulCount = risqueNom.Count(x => x.id_vulne == risk.id_vulne);
                               
                if (vulCount >= 3 )
                {
                    TempData["SucccesMessage"] = "Plus";
                    ViewBag.chapitre = new SelectList(db.action_mesure.Select(a => a.chapitre).Distinct().ToList());
                    return View(model);

                }
                TempData["SucccesMessage"] = "Bien Ajouter";
                db.SaveChanges();
                ViewBag.chapitre = new SelectList(db.action_mesure.Select(a => a.chapitre).Distinct().ToList());
                return View();
                //}
                //TempData["SucccesMessage"] = "Veuillez Remplisez Les Champs";
                //return View(model);
            }
            catch (Exception)
            {
                ViewBag.chapitre = new SelectList(db.action_mesure.Select(a => a.chapitre).Distinct().ToList());
                return View("");
            }
            ViewBag.chapitre = new SelectList(db.action_mesure.Select(a => a.chapitre).Distinct().ToList());
        }

        // GET: GestionRisque/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GestionRisque/Edit/5
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

        // GET: GestionRisque/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GestionRisque/Delete/5
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
