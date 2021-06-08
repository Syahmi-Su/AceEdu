using AceTC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AceTC.Controllers
{
    public class ReceiptController : Controller
    {
        // GET: Receipt
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Slip(int id)
        {
            AceDBEntities entity = new AceDBEntities();

            List<Student> studentlist = entity.Students.ToList();
            List<Parent> parentlist = entity.Parents.ToList();
            List<Payment> paymentlist = entity.Payments.Where(a => a.confirmation_id.Equals(id)).ToList();
            List<Package> packagelist = entity.Packages.ToList();
            //After done manage payment join package table

            var multipletable = from s in studentlist
                                join p in parentlist on s.parent_ic equals p.parents_ic into table1
                                from p in table1.DefaultIfEmpty()
                                join py in paymentlist on p.parents_ic equals py.parent_ic into table2
                                from py in table2.DefaultIfEmpty()
                                join pc in packagelist on s.student_package equals pc.package_id into table3
                                from pc in table3.DefaultIfEmpty()
                                select new MultipleClass { studentdetails = s, parentdetails = p, paymentdetails = py, packagedetails = pc };

            return View(multipletable);

        }
    }
}