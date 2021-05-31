using AceTC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AceTC.Controllers
{
    public class ParentController : Controller
    {
        // GET: Parent
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewChildren()
        {
            AceDBEntities entity = new AceDBEntities();
            return View(from Student in entity.Students select Student);
        }

        public ActionResult ViewPaymentHistory()
        {
            return View();
        }
    }
}