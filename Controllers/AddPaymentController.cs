using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AceTC.Controllers;
using AceTC.Models;

namespace AceTC.Controllers
{
    public class AddPaymentController : Controller
    {
        // GET: AddPayment
        public ActionResult Index()
        {
            using (AceDBEntities entity = new AceDBEntities())
            {
                return View(entity.Payments.ToList());
            }
                
        }

        // GET: AddPayment/Details/5
        public ActionResult Details(int id)
        {
            using(AceDBEntities entity = new AceDBEntities())
            {
                return View(entity.Payments.Where(x => x.confirmation_id == id).FirstOrDefault());
            }
            
        }

        // GET: AddPayment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AddPayment/Create
        [HttpPost]
        public ActionResult Create(Payment payment)
        {
            try
            {
                // TODO: Add insert logic here
                using (AceDBEntities entity = new AceDBEntities())
                {
                    entity.Payments.Add(payment);
                    entity.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AddPayment/Edit/5
        public ActionResult Edit(int id)
        {
            using (AceDBEntities entity = new AceDBEntities())
            {
                return View(entity.Payments.Where(x => x.confirmation_id == id).FirstOrDefault());
            }
        }

        // POST: AddPayment/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Payment payment
            )
        {
            try
            {
                // TODO: Add update logic here
                using (AceDBEntities entity = new AceDBEntities())
                {
                    entity.Entry(payment).State = EntityState.Modified;
                    entity.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AddPayment/Delete/5
        public ActionResult Delete(int id)
        {
            using (AceDBEntities entity = new AceDBEntities())
            {
                return View(entity.Payments.Where(x => x.confirmation_id == id).FirstOrDefault());
            }
        }

        // POST: AddPayment/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Payment payment)
        {
            try
            {
                // TODO: Add delete logic here
                using (AceDBEntities entity = new AceDBEntities())
                {
                    payment = entity.Payments.Where(x => x.confirmation_id == id).FirstOrDefault();
                    entity.Payments.Remove(payment);
                    entity.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
