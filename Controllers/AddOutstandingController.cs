using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AceTC.Models;
using AceTC.Controllers;

namespace AceTC.Controllers
{
    public class AddOutstandingController : Controller
    {
        AceDBEntities db = new AceDBEntities();
        // GET: AddOutstanding
        public ActionResult AddOutstanding()
        {

            Outstanding addo = new Outstanding();
            var Id = db.Outstandings.OrderByDescending(c => c.O_ID).FirstOrDefault();
            if (Id == null)
            {
                addo.O_ID = 1;
            }
            else
            {
                addo.O_ID = Id.O_ID + 1;
            }

            List<SelectListItem> b = new List<SelectListItem>()
            {
                new SelectListItem {
                    Text = "Approved", Value = "2"
                },
                new SelectListItem {
                    Text = "Pending", Value = "1"
                },
            };
            ViewBag.O_status = new SelectList(b, "Value", "Text");
            return View(addo);
        }

        [HttpPost]
        public ActionResult AddOutstanding(Outstanding outstanding)
        {
            using (AceDBEntities db = new AceDBEntities())
            {
                db.Outstandings.Add(outstanding);
                db.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Add Outstanding Payment Successful. ";
            return RedirectToAction("Index", "Outstanding");
        }
    }
}