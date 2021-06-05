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

        public ActionResult EditStudentDetails(string id)
        {
            AceDBEntities entity = new AceDBEntities();
            Student stud = entity.Students.Find(id);
            if (stud == null)
                return View("NotFound");
            else
                return View(stud);
        }

        [HttpPost]
        public ActionResult EditStudentDetails(Student student)
        {
            /*AceDBEntities entity = new AceDBEntities();
            entity.Entry(student).State = EntityState.Modified;
            entity.SaveChanges();
            //ModelState.Clear();
            //ViewBag.SuccessMessage = "Save Changes Successful. ";
            return RedirectToAction("StudentList", "Admin");*/

            using (AceDBEntities entity = new AceDBEntities())
            {
                entity.Entry(student).State = EntityState.Modified;
                entity.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Save Changes Successful. ";
            return RedirectToAction("StudentList", "Admin");

        }

        public ActionResult DeleteStudent()
        {


            return View();
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