using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace DevFunda.Controllers
{
    public class frontendController : Controller
    {
        // GET: frontend
        public ActionResult javascript()
        {
            return View();
        }
        [HttpPost]

        public IActionResult formsubmit()
        {
            TempData["msg"] = "Thanks for details, we will contact you soon";
            return View("javascript");

        }
        public ActionResult html5()
        { 
            return View();
        }

        public ActionResult css()
        {
            return View();
        }

        public ActionResult json()
        {
            return View();
        }
 public ActionResult learnjavascript()
        {
            return View();
        }
        public ActionResult dotnetcore()
        {
            return View();
        }
    }
}