using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace DevFunda.Controllers
{
    public class MeanStackController : Controller
    {
        // GET: MeanStack
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MongoDB()
        {
            return View();
        }
        public ActionResult ExpressJS()
        {
            return View();
        }
        public ActionResult Angular()
        {
            return View();
        }
        public ActionResult NodeJS()
        {
            return View();
        }

    }
}