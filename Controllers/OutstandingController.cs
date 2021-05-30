using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AceTC.Controllers
{
    public class OutstandingController : Controller
    {
        // GET: Outstanding
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddOutstanding()
        {
            return View();
        }
    }
}