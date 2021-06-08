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

        public ActionResult MakePayment(string id)
        {
            AceDBEntities entity = new AceDBEntities();
           

            List<Student> studentlist = entity.Students.ToList();
            List<Parent> parentlist = entity.Parents.ToList();
            List<Payment> paymentlist = entity.Payments.ToList();
            List<Status> status = entity.Status.ToList();

            //var multipletable = from s in studentlist
            //                    join p in parentlist on s.parent_ic equals p.parents_ic into table1
            //                    from p in table1.DefaultIfEmpty()
            //                    join o in paymentdetails on p.parents_ic equals o.parent_ic into table2
            //                    from o in table2.DefaultIfEmpty()
            //                    where p.parents_ic == Session["parents_ic"].ToString()
            //                    select new MultipleClass { studentdetails = s, parentdetails = p, paymentdetails = o };

            var multipletable = from pa in paymentlist
                                join s in studentlist on pa.student_ic equals s.student_ic
                                join p in parentlist on pa.parent_ic equals p.parents_ic
                                join st in status on pa.status_id equals st.status_id
                                where p.parents_ic == Session["parents_ic"].ToString()
                                select new MultipleClass { studentdetails = s, parentdetails = p, paymentdetails = pa, statusdetails = st };

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
        public ActionResult Upload(int id) 
        {
            AceDBEntities entity = new AceDBEntities();
            Payment p = entity.Payments.Find(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            return View(p);
        }
        [HttpPost]
        public ActionResult Upload(Payment p,int id)
            {
            AceDBEntities entity = new AceDBEntities();
            if (ModelState.IsValid)
            {

                var result = entity.Payments.Where(x => x.confirmation_id.Equals(id));
                if (result != null)
                {
                    p.payment_date = DateTime.Now;
                    string ImageName = System.IO.Path.GetFileName(p.filename);
                    entity.Entry(p).State = EntityState.Modified;
                    entity.SaveChanges();
                    return RedirectToAction("ViewChildren", "Parent");
                }

            }

            return View(p);
        }
        private string SaveToPhysicalLocation(HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/upload/"), fileName);
                file.SaveAs(path);
                return path;
            }
            return string.Empty;
        }
    }



}