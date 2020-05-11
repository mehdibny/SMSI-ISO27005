using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMSI_ISO27005.Models;
using System.Data.Entity;
using SMSI_ISO27005.ViewModels;
using PagedList;

namespace SMSI_ISO27005.Controllers
{
    public class ActivitesController : Controller
    {
        // GET: Activites
        public ActionResult Index(string search, int? i)
        {
            // using (SMSIEntities1 db = new SMSIEntities1())
            //{
            //    return View(db.activite.ToList().ToPagedList(i ?? 1, 7));

            //}
            using (SMSIEntities1 db = new SMSIEntities1())
            {
                //List<user_table> userNom = db.user_table.ToList();
                List<collaborateur> collaborateurNom = db.collaborateur.ToList();
                List<activite> actviteNom = db.activite.ToList();

                var query = from av in actviteNom
                            join coll in collaborateurNom on av.matricule equals coll.matricule
                            into collTable
                            from coll in collTable.DefaultIfEmpty()

                                //join user in userNom on coll.matricule equals user.matricule
                                //into userTable
                                //from user in userTable.DefaultIfEmpty()
                            select new CIDActifVM
                            {
                                activiteDetaillese = av,
                                collaborateurDetailles = coll
                                //user_tableDetailles = user
                            };
                var matricule = Session["UserMatricule"].ToString();
                var fonction = Session["CollabFonction"].ToString();
                if (fonction == "admin")
                {
                    return View(query.OrderBy(x=>x.collaborateurDetailles.matricule).ToList().ToPagedList(i ?? 1, 7));
                }
                return View(query.Where(x => x.activiteDetaillese.matricule == matricule).ToPagedList(i ?? 1, 7));
            }
        }

        // GET: Activites/Details/5
        public ActionResult Details(int id=0)
        {
            using (SMSIEntities1 db = new SMSIEntities1())
            {
                return View(db.activite.Where(x => x.id_activite == id).FirstOrDefault());

            }
            

        }

        public ActionResult ActifList()
        {
            return View();
        }

        // GET: Activites/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Activites/Create
        [HttpPost]
        public ActionResult Create(activite Activite)
        {
            
                // TODO: Add insert logic here
                using (SMSIEntities1 db = new SMSIEntities1())
                {
                    activite Activites = new activite();
                   
                    Activites.nom_activite = Activite.nom_activite;
                    Activites.date_creation = DateTime.Now;
                    Activites.matricule = Session["UserMatricule"].ToString();
                    //var exists = db.activite.Where(w => w.id_activite == Activite.id_activite).FirstOrDefault();
                    //if (exists!=null)
                    //{
                    //    //Activite.errorMessage = "Activite Deja Existant";
                    //    TempData["errorMessage"] = "Activite Deja Existant";
                    //    //ViewBag.Message = "Activite Deja Existant";
                    //    return View("create", Activite);
                    //}
                    
                        db.activite.Add(Activites);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    

                }
            
        }

        // GET: Activites/Edit/5
        public ActionResult Edit(int id = 0)
        {
            using (SMSIEntities1 db = new SMSIEntities1())
            {
                return View(db.activite.Where(x => x.id_activite == id).FirstOrDefault());
            }
        }

        // POST: Activites/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, activite Activite)
        {
            try
            {
                using (SMSIEntities1 db = new SMSIEntities1())
                {
                    db.Entry(Activite).State = EntityState.Modified;
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

        // GET: Activites/Delete/5
        public ActionResult Delete(int id=0)
        {
            using (SMSIEntities1 db = new SMSIEntities1())
            {
                return View(db.activite.Where(x => x.id_activite == id).FirstOrDefault());
            }
        }

        // POST: Activites/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, activite Activite)
        {
            try
            {
                using (SMSIEntities1 db = new SMSIEntities1())
                {
                    Activite = db.activite.Where(x => x.id_activite == id).FirstOrDefault();
                    db.activite.Remove(Activite);
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
