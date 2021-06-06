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
            AceDBEntities entity = new AceDBEntities();
            CountClass cnt = new CountClass()
            {
                studentcount = entity.Students.Count(),
                packagecount = entity.Packages.Count()
            };
            return View(cnt);
        }

        public ActionResult StudentList()
        {
            AceDBEntities entity = new AceDBEntities();
            List<Student> studentparent = entity.Students.ToList();
            List<Parent> parentname = entity.Parents.ToList();
            List<Package> packagename = entity.Packages.ToList();

            var multipletable = from s in studentparent
                                join p in parentname on s.parent_ic equals p.parents_ic 
                                join pc in packagename on s.student_package equals pc.package_id
                                select new MultipleClass { studentdetails = s, parentdetails = p, packagedetails = pc };

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
            AceDBEntities packagelist = new AceDBEntities();
            return View(from Package in packagelist.Packages select Package);
            
        }


        public ActionResult EditPackageDetails(int id)
        {

            AceDBEntities entity = new AceDBEntities();
            Package pack = entity.Packages.Find(id);
            if (pack == null)
                return View("NotFound");
            else
                return View(pack);
        }
        [HttpPost]
        public ActionResult EditPackageDetails(Package package)
        {
            /*AceDBEntities entity = new AceDBEntities();
            entity.Entry(student).State = EntityState.Modified;
            entity.SaveChanges();
            //ModelState.Clear();
            //ViewBag.SuccessMessage = "Save Changes Successful. ";
            return RedirectToAction("StudentList", "Admin");*/

            using (AceDBEntities entity = new AceDBEntities())
            {
                entity.Entry(package).State = EntityState.Modified;
                entity.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Save Changes Successful. ";
            return RedirectToAction("PackageList", "Admin");

        }

        public ActionResult DeletePackage()
        {


            return View();
        }



        public ActionResult SubjectList()
        {
            AceDBEntities slist = new AceDBEntities();
            return View(from Subject in slist.Subjects select Subject);
            
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