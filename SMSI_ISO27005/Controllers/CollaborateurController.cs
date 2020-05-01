using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMSI_ISO27005.Models;
using System.Data.Entity;
using SMSI_ISO27005.ViewModels;


namespace SMSI_ISO27005.Controllers
{
    public class CollaborateurController : Controller
    {
        // GET: Collab
        public ActionResult Index()
        {
            using (SMSIEntities1 db = new SMSIEntities1())
            {

            return View(db.collaborateur.ToList());

            }
        }

        

        // GET: Collab/Details/5
        public ActionResult Details(CIDActifVM CID,string id)
        {
            //using (SMSIEntities1 db = new SMSIEntities1())
            //{
            //    return View(db.collaborateur.Where(x=> x.matricule == id).FirstOrDefault());
            //}
            SMSIEntities1 db = new SMSIEntities1();


            List<user_table> userNom = db.user_table.ToList();
            List<collaborateur> collaborateurNom = db.collaborateur.ToList();
            List<activite> activiteNom = db.activite.ToList();
            List<CID_actif> cidNom = db.CID_actif.ToList();
            List<actif> actifNom = db.actif.ToList();

            var querry = from us in userNom

                join mt in collaborateurNom on us.matricule equals mt.matricule
                into mtTable
                from mt in mtTable.DefaultIfEmpty()

                join av in activiteNom on mt.matricule equals av.matricule
                into mtaTable
                from av in mtaTable.DefaultIfEmpty()

                join cid in cidNom on av.id_activite equals cid.id_activite
                into avTable
                from cid in avTable.DefaultIfEmpty()

                join af in actifNom on cid.id_actif equals af.id_actif
                into afTable
                from af in afTable.DefaultIfEmpty()
                select new CIDActifVM
                {
                    user_tableDetailles = us,
                    collaborateurDetailles = mt,
                    activiteDetaillese = av,
                    CIDDetailles = cid,
                    actifDetailles = af
                };
            return View(querry.Where(x => x.collaborateurDetailles.matricule==id).FirstOrDefault());
        }

        // GET: Collab/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Collab/Create
        [HttpPost]
        public ActionResult Create(collaborateur collab)
        {
            //try
            //{
            //    // TODO: Add insert logic here
            //    using (SMSIEntities1 db = new SMSIEntities1())
            //    {
            //        db.collaborateur.Add(collab);
            //        db.SaveChanges();
            //    }

            //    return RedirectToAction("Index");
            //}
            //catch
            //{
            return View();
            //}
        }

        // GET: Collab/Edit/5
        public ActionResult Edit(string id)
        {
            //using (SMSIEntities1 db = new SMSIEntities1())
            //{
            //    db.collaborateur.Where(x => x.matricule == id).FirstOrDefault()
            return View();
            //}
        }

        // POST: Collab/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, collaborateur collab)
        {
            //try
            //{
            //    // TODO: Add update logic here
            //    using (SMSIEntities1 db = new SMSIEntities1())
            //    {
            //        db.Entry(collab).State = EntityState.Modified;
            //        db.SaveChanges();

            //    }
            //    return RedirectToAction("Index");
            //}
            //catch
            //{
            return View();
            //}
        }

        // GET: Collab/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Collab/Delete/5
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
