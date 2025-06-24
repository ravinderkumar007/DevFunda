using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace DevFunda.Controllers
{
    public class labController : Controller
    {
        // GET: lab
        public ActionResult Index()
        {
            return View();
        }
    }
}