using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AceTC.Models;

namespace AceTC.Controllers
{
    public class AddPackageController : Controller
    {
        // GET: AddPackage
        public ActionResult AddPackage()
        {
            Package addpack = new Package();
            return View(addpack);
        }

        [HttpPost]
        public ActionResult AddPackage(Package package)
        {
            using (AceDBEntities db = new AceDBEntities())
            {
                db.Packages.Add(package);
                db.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Add Package Successfully. ";
            return RedirectToAction("PackageList", "Admin");
        }
    }
}