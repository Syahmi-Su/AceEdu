using AceTC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

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
            string uid = Session["parents_ic"].ToString();
            List<Student> studentpackage = entity.Students.Where(a => a.parent_ic.Equals(uid)).ToList();
            List<Package> packagename = entity.Packages.ToList();
            var jointable = from s in studentpackage
                            join p in packagename on s.student_package equals p.package_id into table1
                            from p in table1.DefaultIfEmpty()
                            select new JoinClass { studentdetails = s, packagedetails = p };
            
           // var list = entity.Students.Where(a => a.parent_ic.Equals(uid));
           //return View(entity.Students.ToList());
            return View(jointable);
        }

       public ActionResult ViewChildrenDetails(string id)
       {
           AceDBEntities entity = new AceDBEntities();
            //Student stud = entity.Students.Find(id);
            List<Student> studentpackage = entity.Students.Where(a => a.student_ic.Equals(id)).ToList();
            List<Package> packagename = entity.Packages.ToList();
            var combinetable = from s in studentpackage
                            join p in packagename on s.student_package equals p.package_id into table1
                            from p in table1.DefaultIfEmpty()
                            select new JoinClass { studentdetails = s, packagedetails = p };


            return View(combinetable);

        }

        public ActionResult ViewPaymentHistory()
        {
            return View();
        }

        public ActionResult ViewSubject()
        {
            AceDBEntities slist = new AceDBEntities();
            return View(from Subject in slist.Subjects select Subject);
        }
    }
}