using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using AceTC.Models;

namespace AceTC.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdminDashboard()
        {
            return View();
        }

        public ActionResult StudentList()
        {
            AceDBEntities entity = new AceDBEntities();
            return View(from Student in entity.Students select Student);
        }

        public ActionResult ParentList()
        {
            AceDBEntities plist = new AceDBEntities();
            return View(from Parent in plist.Parents select Parent);
        }

        public ActionResult PackageList()
        {
            return View();
        }

        public ActionResult SubjectList()
        {
            return View();
        }

        public ActionResult PaymentHistory()
        {
            return View();
        }

        public ActionResult testing()
        {
            return View();
        }
    }
}