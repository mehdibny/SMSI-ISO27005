using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using SMSI_ISO27005.Models;
using SMSI_ISO27005.ViewModels;

namespace SMSI_ISO27005.Controllers
{
    public class CIDActifController : Controller
    {
        // GET: CIDActif
        //[HttpPost]
        public ActionResult CID(string search, int? i,int id=0)
        {
            SMSIEntities1 db = new SMSIEntities1();

            List<user_table> userNom = db.user_table.ToList();
            List<collaborateur> collaborateurNom = db.collaborateur.ToList();
            List<CID_actif> cidNom = db.CID_actif.ToList();
            List<actif> actifNom = db.actif.ToList();
            List<activite> activiteNom = db.activite.ToList();
            List<confid> confidNom = db.confid.ToList();
            List<integrite> integriteNom = db.integrite.ToList();
            List<disponibilte> dispoNom = db.disponibilte.ToList();
            var querry = from cid in cidNom

                join af in actifNom on cid.id_actif equals af.id_actif
                into afTable
                from af in afTable.DefaultIfEmpty()

                join av in activiteNom on cid.id_activite equals av.id_activite
                into avTable
                from av in avTable.DefaultIfEmpty()

                join con in confidNom on cid.id_confid equals con.id_confi
                into conTable
                from con in conTable.DefaultIfEmpty()

                join inte in integriteNom on cid.id_integ equals inte.id_integ
                into intTable
                from inte in intTable.DefaultIfEmpty()

                join disp in dispoNom on cid.id_dispo equals disp.id_dispo
                into dispTable
                from disp in dispTable.DefaultIfEmpty()

                join us in userNom on av.matricule equals us.matricule
                into usTable
                from us in usTable.DefaultIfEmpty()

                join col in collaborateurNom on us.matricule equals col.matricule
                into colTable
                from col in colTable.DefaultIfEmpty()
                         select new CIDActifVM
                {
                    CIDDetailles = cid,
                    actifDetailles = af,
                    activiteDetaillese = av,
                    confidDetailles = con,
                    integriteDetailles = inte,
                    disponibilteDetailles = disp,
                    user_tableDetailles = us,
                    collaborateurDetailles = col
                         };

            var querryActif = from cid in cidNom

                join af in actifNom on cid.id_actif equals af.id_actif
                into afTable
                from af in afTable.DefaultIfEmpty()
                select new CIDActifVM
                {
                    CIDDetailles = cid,
                    actifDetailles = af
                };

            if (querryActif.Any(w => w.actifDetailles.id_actif == id))
            {
                return View(querry.Where(x => x.actifDetailles.id_actif == id).ToList().ToPagedList(i ?? 1, 7));
            }
            else
            {
                //Activite DropDownList
                List<activite> list = db.activite.ToList();
                ViewBag.activiteList = new SelectList(list, "id_activite", "nom_activite");

                //Actif DropDownList
                List<actif> listActif = db.actif.ToList();
                ViewBag.actifList = new SelectList(listActif, "id_actif", "nom_actif", id);
                //return View(querry.ToList().ToPagedList(i ?? 1, 7));
                //return View("Create");
                return RedirectToAction("create", "CIDActif",id);
            }


            //return RedirectToAction("create", "Actifs");



        }

        // GET: CIDActif/Details/5
        public ActionResult Actifs(string search, int? i,CIDActifVM model, int id=0)
        {
                SMSIEntities1 db = new SMSIEntities1();
                List<user_table> userNom = db.user_table.ToList();
                List<collaborateur> collaborateurNom = db.collaborateur.ToList();
                List<activite> activiteNom = db.activite.ToList();
                List<CID_actif> cidNom = db.CID_actif.ToList();
                List<actif> actifNom = db.actif.ToList();
                List<confid> confidNom = db.confid.ToList();
                List<integrite> integriteNom = db.integrite.ToList();
                List<disponibilte> dispoNom = db.disponibilte.ToList();

            var querry = from cid in cidNom

                join af in actifNom on cid.id_actif equals af.id_actif
                into afTable
                from af in afTable.DefaultIfEmpty()

                join av in activiteNom on cid.id_activite equals av.id_activite
                into avTable
                from av in avTable.DefaultIfEmpty()

                join con in confidNom on cid.id_confid equals con.id_confi
                into conTable
                from con in conTable.DefaultIfEmpty()

                join inte in integriteNom on cid.id_integ equals inte.id_integ
                into intTable
                from inte in intTable.DefaultIfEmpty()

                join disp in dispoNom on cid.id_dispo equals disp.id_dispo
                into dispTable
                from disp in dispTable.DefaultIfEmpty()

                join us in userNom on av.matricule equals us.matricule
                into usTable
                from us in usTable.DefaultIfEmpty()

                join col in collaborateurNom on us.matricule equals col.matricule
                into colTable
                from col in colTable.DefaultIfEmpty()


                select new CIDActifVM
                {
                    user_tableDetailles = us,
                    collaborateurDetailles = col,
                    activiteDetaillese = av,
                    CIDDetailles = cid,
                    actifDetailles = af,
                    confidDetailles = con,
                    integriteDetailles = inte,
                    disponibilteDetailles = disp,
                };
            return View(querry.Where(x => x.activiteDetaillese.id_activite==id).ToList().ToPagedList(i ?? 1, 7));
        }

        // GET: CIDActif/Create
        public ActionResult Create()
        {
            SMSIEntities1 db = new SMSIEntities1();

            List<activite> list = db.activite.ToList();
            ViewBag.activiteList = new SelectList(list, "id_activite", "nom_activite");

            List<actif> listActif = db.actif.ToList();
            ViewBag.actifList = new SelectList(listActif, "id_actif", "nom_actif");
            return View();
        }

        // POST: CIDActif/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CIDActifVM model,int id=0)
        {

            try
            {
                // TODO: Add insert logic here
                SMSIEntities1 db = new SMSIEntities1();
                //Activite DropDownList
                List<activite> list = db.activite.ToList();
                ViewBag.activiteList = new SelectList(list, "id_activite", "nom_activite");

                //Actif DropDownList
                List<actif> listActif = db.actif.ToList();
                ViewBag.actifList = new SelectList(listActif, "id_actif", "nom_actif",id);
                //if (ModelState.IsValid)
                //{
                    //Insert Into Confidentialite Table
                    confid Confid = new confid();
                    Confid.id_confi = model.confidDetailles.id_confi;
                    Confid.nom_confid = model.confidDetailles.nom_confid;
                    Confid.date_creation_confid = DateTime.Now;/*model.confidDetailles.date_creation_confid;*/
                    Confid.descr_confid = model.confidDetailles.descr_confid;
                    Confid.score_confid = model.confidDetailles.score_confid;
                    if (Confid.id_confi.Equals(null) || Confid.nom_confid.Equals(null)
                        || Confid.date_creation_confid.Equals(null) || Confid.descr_confid.Equals(null)
                        || Confid.score_confid.Equals(null) || Confid.errorMessage != null)
                    {
                        Confid.errorMessage = "no";
                    }
                    db.confid.Add(Confid);
                    //db.SaveChanges();

                    //Last Insetrded ID
                    int LastConfId = Confid.id_confi;


                    //Insert Into Integrite Table
                    integrite Integrite = new integrite();
                    Integrite.id_integ = model.integriteDetailles.id_integ;
                    Integrite.nom_integ = model.integriteDetailles.nom_integ;
                    Integrite.date_creation_integ = DateTime.Now; /*model.integriteDetailles.date_creation_integ;*/
                    Integrite.descr_integ = model.integriteDetailles.descr_integ;
                    Integrite.score_integd = model.integriteDetailles.score_integd;
                    if (Integrite.id_integ.Equals(null) || Integrite.nom_integ.Equals(null)
                        || Integrite.date_creation_integ.Equals(null) || Integrite.descr_integ.Equals(null)
                        || Integrite.score_integd.Equals(null) || Integrite.errorMessage != null)
                    {
                        Integrite.errorMessage = "no";
                    }

                    db.integrite.Add(Integrite);
                    //db.SaveChanges();

                    //Last Insetrded ID
                    int LastIntegId = Integrite.id_integ;


                    //Insert Into Disponibilte Table
                    disponibilte Disponibilte = new disponibilte();
                    Disponibilte.id_dispo = model.disponibilteDetailles.id_dispo;
                    Disponibilte.nom_dispo = model.disponibilteDetailles.nom_dispo;
                    Disponibilte.date_creation_dispo = DateTime.Now; /*model.disponibilteDetailles.date_creation_dispo;*/
                    Disponibilte.descr_dispo = model.disponibilteDetailles.descr_dispo;
                    Disponibilte.score_dispo = model.disponibilteDetailles.score_dispo;
                    if (Disponibilte.id_dispo.Equals(null) || Disponibilte.nom_dispo.Equals(null)
                        || Disponibilte.date_creation_dispo.Equals(null) || Disponibilte.descr_dispo.Equals(null)
                        || Disponibilte.score_dispo.Equals(null) || Disponibilte.errorMessage != null)
                    {
                        Disponibilte.errorMessage = "no";
                    }

                    db.disponibilte.Add(Disponibilte);
                    //db.SaveChanges();

                    //Last Insetrded ID
                    int LastDispogId = Disponibilte.id_dispo;


                    //Insert Into CIDActif Table
                    CID_actif CID = new CID_actif();
                    CID.id_actif = model.actifDetailles.id_actif;
                    CID.id_activite = model.activiteDetaillese.id_activite;
                    CID.id_confid = LastConfId;
                    CID.id_integ = LastIntegId;
                    CID.id_dispo = LastDispogId;

                    if (CID.id_actif.Equals(null) || CID.id_activite.Equals(null)
                        || CID.id_confid.Equals(null) || CID.id_integ.Equals(null)
                        || CID.id_dispo.Equals(null) || CID.errorMessage != null)
                    {
                        CID.errorMessage = "no";
                    }

                    var exists = db.CID_actif.Where(x => x.id_actif == CID.id_actif
                    && x.id_activite == CID.id_activite).FirstOrDefault();

                    if (exists != null
                        && CID.errorMessage != "no" && Confid.errorMessage != "no"
                        && Integrite.errorMessage != "no" && Disponibilte.errorMessage != "no"
                        )
                    {
                        //CID.errorMessage = "ok";
                        TempData["SucccesMessage"] = "Nope";
                        return View(model);
                    }
                    else if (exists == null 
                        && CID.errorMessage != "no" && Confid.errorMessage != "no"
                        && Integrite.errorMessage != "no" && Disponibilte.errorMessage != "no"
                        )
                    {
                        TempData["SucccesMessage"] = "Bien Ajouter";
                        db.CID_actif.Add(CID);
                        db.SaveChanges();
                        return View(model);

                    }


                    //return RedirectToAction("CID");
                //} ModelState
                    return View(model);
            }


            catch (Exception)
            {
                return View("");
            }
        }
        public ActionResult ConfidDetails(int id = 0)
        {
            using (SMSIEntities1 db = new SMSIEntities1())
            {
                return View(db.confid.Where(x => x.id_confi == id).FirstOrDefault());

            }


        }
        public ActionResult INtegriteDetails(int id = 0)
        {
            using (SMSIEntities1 db = new SMSIEntities1())
            {
                return View(db.integrite.Where(x => x.id_integ == id).FirstOrDefault());

            }


        }
        public ActionResult DispoDetails(int id = 0)
        {
            using (SMSIEntities1 db = new SMSIEntities1())
            {
                return View(db.disponibilte.Where(x => x.id_dispo == id).FirstOrDefault());

            }


        }

        // GET: CIDActif/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CIDActif/Edit/5
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

        // GET: CIDActif/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CIDActif/Delete/5
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
