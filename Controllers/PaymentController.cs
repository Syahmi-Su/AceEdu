using AceTC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AceTC.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MakePayment()
        {
            AceDBEntities entity = new AceDBEntities();
            List<Student> student = entity.Students.ToList();
            List<Parent> parentname = entity.Parents.ToList();
            List<Outstanding> outstanding = entity.Outstandings.ToList();

            var multipletable = from s in student
                                join p in parentname on s.parent_ic equals p.parents_ic into table1
                                from p in table1.DefaultIfEmpty()
                                join o in outstanding on p.parents_ic equals o.O_pID into table2
                                from o in table2.DefaultIfEmpty()
                                select new MultipleClass { studentdetails = s, parentdetails = p, outstandingdetails = o };

            return View(multipletable);
        }

        public ActionResult ManagePayment()
        {
            return View();
        }

        public ActionResult PaymentHistories()
        {
            return View();
        }
    }
}