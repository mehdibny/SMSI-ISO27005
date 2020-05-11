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
    public class MenaceController : Controller
    {
        // GET: Menace
        public ActionResult Index(string search, int? i,int id= 0)
        {
            SMSIEntities1 db = new SMSIEntities1();

            List<actif> actifNom = db.actif.ToList();
            List<vulnerabilte> vulneNom = db.vulnerabilte.ToList();
            List<menace> menaceNom = db.menace.ToList();
            List<prob_occurrence> occuraneNom = db.prob_occurrence.ToList();
            List<impact> impactNom = db.impact.ToList();
            var querry = from vul in vulneNom

                         join af in actifNom on vul.id_actif equals af.id_actif
                         into afTable
                         from af in afTable.DefaultIfEmpty()

                         join men in menaceNom on vul.id_vulne equals men.id_vulne
                         into menTable
                         from men in menTable.DefaultIfEmpty()

                         join occu in occuraneNom on men.id_menace equals occu.id_menace
                         into occuTable
                         from occu in occuTable.DefaultIfEmpty()

                         join imp in impactNom on men.id_menace equals imp.id_menace
                         into impTable
                         from imp in impTable.DefaultIfEmpty()
                         select new CIDActifVM
                         {
                             actifDetailles = af,
                             vulnerabilteDetailles = vul,
                             menaceDetailles = men,
                             probOccurrenceDetailles = occu,
                             impactDetailles = imp
                         };
            var querryActif = from vul in vulneNom

                              join af in actifNom on vul.id_actif equals af.id_actif
                              into afTable
                              from af in afTable.DefaultIfEmpty()
                              select new CIDActifVM
                              {
                                  vulnerabilteDetailles = vul,
                                  actifDetailles = af
                              };
            if (querryActif.Any(w => w.vulnerabilteDetailles.id_actif == id))
            {
                return View(querry.Where(x => x.actifDetailles.id_actif == id).ToList().ToPagedList(i ?? 1, 7));

            }
            else
            {
                //Actif DropDownList
                List<actif> listActif = db.actif.ToList();
                ViewBag.actifList = new SelectList(listActif, "id_actif", "nom_actif", id);
                ////return View("create");
                return RedirectToAction("create", "menace");
            }

        }
        public ActionResult VulneDatails(int id = 0)
        {
            using (SMSIEntities1 db = new SMSIEntities1())
            {
                return View(db.vulnerabilte.Where(x => x.id_vulne == id).FirstOrDefault());

            }


        }
        public ActionResult MenaceDatails(int id = 0)
        {
            using (SMSIEntities1 db = new SMSIEntities1())
            {
                return View(db.menace.Where(x => x.id_menace == id).FirstOrDefault());

            }


        }
        public ActionResult ImpactDatails(int id = 0)
        {
            using (SMSIEntities1 db = new SMSIEntities1())
            {
                return View(db.impact.Where(x => x.id_impact == id).FirstOrDefault());

            }


        }
        public ActionResult OccuDatails(int id = 0)
        {
            using (SMSIEntities1 db = new SMSIEntities1())
            {
                return View(db.prob_occurrence.Where(x => x.id_occur == id).FirstOrDefault());

            }


        }

        // GET: Menace/Create
        public ActionResult Create()
        {
            SMSIEntities1 db = new SMSIEntities1();

            List<actif> listActif = db.actif.ToList();
            ViewBag.actifList = new SelectList(listActif, "id_actif", "nom_actif");
            return View();
        }


        // POST: Menace/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CIDActifVM model)
        {

            try
            {
                // TODO: Add insert logic here
                SMSIEntities1 db = new SMSIEntities1();

                //Actif DropDownList
                List<actif> listActif = db.actif.ToList();
                ViewBag.actifList = new SelectList(listActif, "id_actif", "nom_actif");


                //Insert Into Vulnerabilite Table
                vulnerabilte vuln = new vulnerabilte();
                vuln.id_vulne = model.vulnerabilteDetailles.id_vulne;
                vuln.nom_vulne = model.vulnerabilteDetailles.nom_vulne;
                vuln.date_creation_vulne = DateTime.Now;
                vuln.desc_vulne = model.vulnerabilteDetailles.desc_vulne;
                vuln.id_actif = model.actifDetailles.id_actif;
                //if (vuln.id_vulne.Equals(null) || vuln.nom_vulne.Equals(null) 
                //    || vuln.desc_vulne.Equals(null) || vuln.id_actif.Equals(null))
                //{
                //    vuln.errorMessage = null;
                //}
                //vuln.errorMessage = "Bien Ajoute";

                //Last Insetrded ID
                int LastVulnID = vuln.id_vulne;
                

                


                //Insert Into Menace Table
                menace men = new menace();
                men.id_menace = model.menaceDetailles.id_menace;
                men.nom_menace = model.menaceDetailles.nom_menace;
                men.date_creation_menace = DateTime.Now;
                men.desc_menace = model.menaceDetailles.desc_menace;
                men.id_vulne = LastVulnID;

                //if (men.id_menace.Equals(null) || men.nom_menace.Equals(null) 
                //    || men.desc_menace.Equals(null) || men.id_vulne.Equals(null))
                //{
                //    men.errorMessage = null;
                //}
                //men.errorMessage = "Bien Ajoute";

                //Last Insetrded ID
                int LastMenaceId = men.id_menace;


                //Insert Into Impact Table
                impact imp = new impact();
                imp.id_impact = model.impactDetailles.id_impact;
                imp.nom_impact = model.impactDetailles.nom_impact;
                imp.date_creation_impact = DateTime.Now;
                imp.descr_impact = model.impactDetailles.descr_impact;
                imp.score_impact = model.impactDetailles.score_impact;
                imp.id_menace = LastMenaceId;

                //if (imp.id_menace.Equals(null) || imp.id_impact.Equals(null) 
                //    || imp.nom_impact.Equals(null) || imp.descr_impact.Equals(null) 
                //    || imp.score_impact.Equals(null))
                //{
                //    imp.errorMessage = null;
                //}

                //imp.errorMessage = "Bien Ajoute";

                //Insert Into Probalite D'occurance Table
                prob_occurrence occu = new prob_occurrence();
                occu.id_occur = model.probOccurrenceDetailles.id_occur;
                occu.nom_occur = model.probOccurrenceDetailles.nom_occur;
                occu.date_creation_occur = DateTime.Now;
                occu.desc_occur = model.probOccurrenceDetailles.desc_occur;
                occu.score_occur = model.probOccurrenceDetailles.score_occur;
                occu.id_menace = LastMenaceId;

                //if (occu.id_menace.Equals(null) || occu.id_occur.Equals(null) 
                //    || occu.nom_occur.Equals(null) || occu.desc_occur.Equals(null) 
                //    || occu.score_occur.Equals(null))
                //{
                //    occu.errorMessage = null;
                //}
                //occu.errorMessage = "Bien Ajoute";

                //if (vuln.errorMessage!=null && men.errorMessage!=null && imp.errorMessage 
                //    != null && occu.errorMessage != null)
                //{
                    db.vulnerabilte.Add(vuln);
                    db.menace.Add(men);
                    db.impact.Add(imp);
                    db.prob_occurrence.Add(occu);
                    db.SaveChanges();
                    //model.vulnerabilteDetailles.errorMessage = "Bien Ajoute";
                    //TempData["errorMessage"] = model.vulnerabilteDetailles.errorMessage;
                    return View(model);
                    //return View("index");


                //}
                //else
                //{
                //    model.vulnerabilteDetailles.errorMessage = "Nope";
                //    TempData["errorMessage"] = model.vulnerabilteDetailles.errorMessage;
                //}

                //return View(model);
                //return View("index");

            }
            catch (Exception)
            {
                return View("");
            }
        }

        // GET: Menace/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        // POST: Menace/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Menace/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: Menace/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        //}
    }
}


    
