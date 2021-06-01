using AceTC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return View(entity.Students.ToList());
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
    }
}