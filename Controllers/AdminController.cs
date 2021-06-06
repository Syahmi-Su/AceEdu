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

        // GET: STUDENT
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
            //ViewBag.SuccessMessage = "Save Changes Successful. ";
            return RedirectToAction("StudentList", "Admin");

        }

        public ActionResult DeleteStudent(string id)
        {
            AceDBEntities entity = new AceDBEntities();
            Student std = entity.Students.Find(id);
            if (std == null)
                return View("NotFound");
            else
                return View(std);
        }

        [HttpPost]
        [ActionName("DeleteStudent")]
        public ActionResult ConfirmedDeleteStudent(string id)
        {
            AceDBEntities entity = new AceDBEntities();
            Student std = entity.Students.Find(id);
            entity.Students.Remove(std);
            entity.SaveChanges();
            return RedirectToAction("StudentList", "Admin");
        }



        // GET: PARENT
        public ActionResult ParentList()
        {
            AceDBEntities plist = new AceDBEntities();
            return View(from Parent in plist.Parents select Parent);
        }

        public ActionResult EditParentDetails(string id)
        {
            AceDBEntities entity = new AceDBEntities();
            Parent prnt = entity.Parents.Find(id);
            if (prnt == null)
                return View("NotFound");
            else
                return View(prnt);
        }
        [HttpPost]
        public ActionResult EditParentDetails(Parent parent)
        {
            using (AceDBEntities entity = new AceDBEntities())
            {
                entity.Entry(parent).State = EntityState.Modified;
                entity.SaveChanges();
            }
            ModelState.Clear();
            //ViewBag.SuccessMessage = "Save Changes Successful. ";
            return RedirectToAction("ParentList", "Admin");

        }



        public ActionResult DeleteParent(string id)
        {
            AceDBEntities entity = new AceDBEntities();
            Parent prnt = entity.Parents.Find(id);
            if (prnt == null)
                return View("NotFound");
            else
                return View(prnt);
        }
        [HttpPost]
        [ActionName("DeleteParent")]
        public ActionResult ConfirmedDeleteParent(string id)
        {
            AceDBEntities entity = new AceDBEntities();
            Parent prnt = entity.Parents.Find(id);
            entity.Parents.Remove(prnt);
            entity.SaveChanges();
            return RedirectToAction("ParentList", "Admin");
        }


        // GET: PACKAGE
        public ActionResult PackageList()
        {
<<<<<<< Updated upstream
            return View();
        }

        public ActionResult SubjectList()
        {
            return View();
=======
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

            using (AceDBEntities entity = new AceDBEntities())
            {
                entity.Entry(package).State = EntityState.Modified;
                entity.SaveChanges();
            }
            ModelState.Clear();
            //ViewBag.SuccessMessage = "Save Changes Successful. ";
            return RedirectToAction("PackageList", "Admin");

        }

        public ActionResult DeletePackage(int id)
        {
            AceDBEntities entity = new AceDBEntities();
            Package pkg = entity.Packages.Find(id);
            if (pkg == null)
                return View("NotFound");
            else
                return View(pkg);
        }

        [HttpPost]
        [ActionName("DeletePackage")]
        public ActionResult ConfirmedDeletePackage(int id)
        {
            AceDBEntities entity = new AceDBEntities();
            Package pkg = entity.Packages.Find(id);
            entity.Packages.Remove(pkg);
            entity.SaveChanges();
            return RedirectToAction("PackageList", "Admin");
        }


        // GET: SUBJECT
        public ActionResult SubjectList()
        {
            AceDBEntities slist = new AceDBEntities();
            return View(from Subject in slist.Subjects select Subject);

        }

        public ActionResult EditSubjectDetails(string id)
        {
            AceDBEntities entity = new AceDBEntities();
            Subject subj = entity.Subjects.Find(id);
            if (subj == null)
                return View("NotFound");
            else
                return View(subj);
        }
        [HttpPost]
        public ActionResult EditSubjectDetails(Subject subject)
        {
            using (AceDBEntities entity = new AceDBEntities())
            {
                entity.Entry(subject).State = EntityState.Modified;
                entity.SaveChanges();
            }
            ModelState.Clear();
            //ViewBag.SuccessMessage = "Save Changes Successful. ";
            return RedirectToAction("SubjectList", "Admin");

>>>>>>> Stashed changes
        }

        public ActionResult DeleteSubject(string id)
        {
            AceDBEntities entity = new AceDBEntities();
            Subject subj = entity.Subjects.Find(id);
            if (subj == null)
                return View("NotFound");
            else
                return View(subj);
        }
        [HttpPost]
        [ActionName("DeleteSubject")]
        public ActionResult ConfirmedDeleteSubject(string id)
        {
            AceDBEntities entity = new AceDBEntities();
            Subject subject = entity.Subjects.Find(id);
            entity.Subjects.Remove(subject);
            entity.SaveChanges();
            return RedirectToAction("SubjectList", "Admin");
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