﻿using AceTC.Models;
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
            //return View(from Student in entity.Students select Student);
            //var groupeddata = entity.Students.Where()
            // List<Student> studentlist = entity.Students.ToList();
            //List<Parent> parentlist = entity.Parents.ToList();
            //List<Package> packagetlist = entity.Packages.ToList();

            //ViewData["jointables"] = from p in parentlist
            //                        join s in studentlist on p.parents_ic equals s.parent_ic into table1
            //                      from s in table1.DefaultIfEmpty()
            //                     select new MultipleClass { parentdetails = p, studentdetails = s};
            // string userID = Convert.ToBase64String(Session["parents_ic"]);
            string uid = Session["parents_ic"].ToString();
            var list = entity.Students.Where(a => a.parent_ic.Equals(uid));
           //return View(entity.Students.ToList());
            return View(list);
        }

       public ActionResult ViewChildrenDetails(string id)
       {
           AceDBEntities entity = new AceDBEntities();
           Student stud = entity.Students.Find(id);
           /*List<Student> studentlist = entity.Students.ToList();
           List<Parent> parentlist = entity.Parents.ToList();
           List<Package> packagetlist = entity.Packages.ToList();

           ViewData["jointables"] = from p in parentlist
                                    join s in studentlist on p.parents_ic equals s.parent_ic into table1
                                    from s in table1.DefaultIfEmpty()
                                    join pk in packagetlist on s.student_package equals pk.package_id into table2
                                    from pk in table2.DefaultIfEmpty()
                                    select new MultipleClass { parentdetails = p, studentdetails = s, packagedetails = pk };

           if (ViewData["jointables"] == null)
               return View("NotFound");
           else
               return View(ViewData["jointables"]);*/

            if (stud == null)
                return View("NotFound");
            else
                return View(stud);

        }

        public ActionResult ViewPaymentHistory()
        {
            return View();
        }


/*        public ActionResult editPassword(string id)
        {
            AceDBEntities entity = new AceDBEntities();
            Parent par = entity.Parents.Find(id);
            if (par == null)
                return View("Error");
            else
                return View(par);

        }

        [HttpPost]
        public ActionResult editPassword(Parent parent)
        {
            using (AceDBEntities entity = new AceDBEntities())
            {
                entity.Entry(parent).State = EntityState.Modified;
                entity.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Save Changes Successful. ";
            return RedirectToAction("ViewChildren", "Parent");

        }*/



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