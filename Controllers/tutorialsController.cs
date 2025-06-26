using Microsoft.AspNetCore.Mvc;

namespace Devfunda.Controllers
{
    public class tutorialsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult MongoDBtutorial()
        {
            return View();
        }
    }
}
