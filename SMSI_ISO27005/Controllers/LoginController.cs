using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMSI_ISO27005.Models;

namespace SMSI_ISO27005.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(user_table userModel)
        {
            using (SMSIEntities1 db = new SMSIEntities1())
            {
                var userDetailes = db.user_table.Where(x => x.username == userModel.username && x.passeword == userModel.passeword).FirstOrDefault();
                if (userDetailes == null)
                {
                    userModel.errorMessage = "Matricule ou Mot De Passe Incorect !";
                    return View("Index", userModel);
                }
                else
                {
                    Session["UserID"] = userDetailes.username;
                    Session["UserMatricule"] = userDetailes.matricule;
                    Session["UserPass"] = userDetailes.passeword;
                    return RedirectToAction("Index", "Collaborateur");
                }
            }

        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index","Login");
        }
    }
}