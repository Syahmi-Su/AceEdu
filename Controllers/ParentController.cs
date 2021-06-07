using AceTC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
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

        public ActionResult MakePayment(string id)
        {
            AceDBEntities entity = new AceDBEntities();
            string uid = Session["parents_ic"].ToString();

            List<Student> studentlist = entity.Students.ToList();
            List<Parent> parentlist = entity.Parents.ToList();
            List<Payment> paymentlist = entity.Payments.ToList();

            var multipletable = from s in studentlist
                                join p in parentlist on s.parent_ic equals p.parents_ic into table1
                                from p in table1.DefaultIfEmpty()
                                join o in paymentlist on p.parents_ic equals o.parent_ic into table2
                                from o in table2.DefaultIfEmpty()
                                where p.parents_ic == Session["parents_ic"].ToString()
                                select new MultipleClass { studentdetails = s, parentdetails = p, paymentdetails = o };

            return View(multipletable);
        }

        public ActionResult ViewPaymentHistory()
        {
            AceDBEntities entity = new AceDBEntities();
            string uid = Session["parents_ic"].ToString();

            List<Student> studentlist = entity.Students.ToList();
            List<Parent> parentlist = entity.Parents.ToList();
            List<Payment> paymentlist = entity.Payments.ToList();
            List<Status> status = entity.Status.ToList();

            var multipletable = from s in studentlist
                                join p in parentlist on s.parent_ic equals p.parents_ic into table1
                                from p in table1.DefaultIfEmpty()
                                join pa in paymentlist on p.parents_ic equals pa.parent_ic into table2
                                from pa in table2.DefaultIfEmpty()
                                join st in status on pa.status_id equals st.status_id into table3
                                from st in table3.DefaultIfEmpty()
                                where p.parents_ic == Session["parents_ic"].ToString()
                                select new MultipleClass { studentdetails = s, parentdetails = p, paymentdetails = pa , statusdetails = st};

            return View(multipletable);
        }

        public ActionResult ViewSubject()
        {
            AceDBEntities slist = new AceDBEntities();
            return View(from Subject in slist.Subjects select Subject);


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

        [HttpGet]
        public ActionResult Upload() 
        {
            AceDBEntities entity = new AceDBEntities();
            int id = 1;
            Payment p = entity.Payments.Find(id);
            return View(); 
        }
        [HttpPost]
        public ActionResult Upload([Bind(Include = "confirmation_id,student_ic,parent_ic,payment_fee,ref_num,status_id,confirmation_date,payment_date,payment_detail,payment_feedetailss,filename,meal_fee,transport_fee,first_register,lower_discount")] Payment p)
        {
            using (AceDBEntities entity = new AceDBEntities())
            {
                var candidate = new Payment()
                {
                    filename = p.filename
                };
                entity.Entry(candidate).State = EntityState.Modified;
                entity.SaveChanges();
            }
            return View(p);
        }
        private string SaveToPhysicalLocation(HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/upload"), fileName);
                file.SaveAs(path);
                return path;
            }
            return string.Empty;
        }
    }



}