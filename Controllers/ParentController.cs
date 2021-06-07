using AceTC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
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

        // GET: editpass/Edit/5
        public ActionResult editPassword(string id)
        {
            string uid = Session["parents_ic"].ToString();
            AceDBEntities entity = new AceDBEntities();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parent parent = entity.Parents.Find(id);
            if (parent == null)
            {
                return HttpNotFound();
            }
            return View(parent);
        }

        // POST: editpass/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult editPassword([Bind(Include = "parents_ic,parents_name,parents_pass,confirmPass,parents_email,parents_phone,parents_address")] Parent parent)
        {
            string uid = Session["parents_ic"].ToString();
            AceDBEntities entity = new AceDBEntities();
            if (ModelState.IsValid)
            {
                entity.Entry(parent).State = EntityState.Modified;
                entity.SaveChanges();
                return RedirectToAction("ViewChildren", "Parent");
            }
            return View(parent);

        }
    }
}