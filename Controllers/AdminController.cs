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
            List<Student> studentparent = entity.Students.ToList();
            List<Parent> parentname = entity.Parents.ToList();

            var multipletable = from s in studentparent
                                join p in parentname on s.parent_ic equals p.parents_ic into table1
                                from p in table1.DefaultIfEmpty()
                                select new MultipleClass { studentdetails = s, parentdetails = p };

            return View(multipletable);
        }

        public ActionResult ParentList()
        {
            
            return View();
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