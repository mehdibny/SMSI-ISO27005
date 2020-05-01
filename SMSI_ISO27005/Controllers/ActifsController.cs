using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMSI_ISO27005.Models;
using SMSI_ISO27005.ViewModels;
using PagedList.Mvc;
using PagedList;

namespace SMSI_ISO27005.Controllers
{
    public class ActifsController : Controller
    {
        // GET: Actifs
        public ActionResult Index(string search, int? i)
        {
            using (SMSIEntities1 db = new SMSIEntities1())
            {
                //List<user_table> userNom = db.user_table.ToList();
                List<collaborateur> collaborateurNom = db.collaborateur.ToList();
                List<actif> actifNom = db.actif.ToList();

                var query = from af in actifNom
                            join coll in collaborateurNom on af.matricule equals coll.matricule
                            into collTable
                            from coll in collTable.DefaultIfEmpty()

                                //join user in userNom on coll.matricule equals user.matricule
                                //into userTable
                                //from user in userTable.DefaultIfEmpty()
                            select new CIDActifVM
                            {
                                actifDetailles = af,
                                collaborateurDetailles = coll
                                //user_tableDetailles = user
                            };
                var matricule = Session["UserMatricule"].ToString();
                var fonction = Session["CollabFonction"].ToString();
                if (fonction=="admin")
                {
                    return View(query.OrderBy(x => x.collaborateurDetailles.matricule).ToList().ToPagedList(i ?? 1, 7));
                }
                return View(query.Where(x => x.actifDetailles.matricule == matricule).ToPagedList(i ?? 1, 7));
            }
        }

        // GET: Actifs/Details/5
        public ActionResult Details(int id)
        {
            using (SMSIEntities1 db = new SMSIEntities1())
            {
                return View(db.actif.Where(x => x.id_actif == id).FirstOrDefault());
            }
        }

        // GET: Actifs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Actifs/Create
        [HttpPost]
        public ActionResult Create(actif Actif)
        {
            try
            {
                // TODO: Add insert logic here
                using (SMSIEntities1 db = new SMSIEntities1())
                {
                    actif Actifs = new actif();
                    Actifs.id_actif = Actif.id_actif;
                    Actifs.nom_actif = Actif.nom_actif;
                    Actifs.date_creation_actif = DateTime.Now;
                    Actifs.descr_actif = Actif.descr_actif;
                    Actifs.categ_actif = Actif.categ_actif;
                    Actifs.matricule = Session["UserMatricule"].ToString();

                    db.actif.Add(Actifs);
                    db.SaveChanges();
                }
                return RedirectToAction("Index",Actif);
            }
            catch
            {
                return View("");
            }
        }

        // GET: Actifs/Edit/5
        public ActionResult Edit(int id=0)
        {
            using (SMSIEntities1 db = new SMSIEntities1())
            {
                return View(db.actif.Where(x => x.id_actif == id).FirstOrDefault());
            }
        }

        // POST: Actifs/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, actif Actif)
        {
            try
            {
                using (SMSIEntities1 db = new SMSIEntities1())
                {
                    db.Entry(Actif).State = EntityState.Modified;
                    db.SaveChanges();
                }
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Actifs/Delete/5
        public ActionResult Delete(int id=0)
        {
            using (SMSIEntities1 db = new SMSIEntities1())
            {
                return View(db.actif.Where(x => x.id_actif == id).FirstOrDefault());
            }
        }

        // POST: Actifs/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, actif Actif)
        {
            try
            {
                using (SMSIEntities1 db = new SMSIEntities1())
                {
                    Actif = db.actif.Where(x => x.id_actif == id).FirstOrDefault();
                    db.actif.Remove(Actif);
                    db.SaveChanges();
                }
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
