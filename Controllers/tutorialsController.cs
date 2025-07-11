using Microsoft.AspNetCore.Mvc;

namespace Devfunda.Controllers
{
    public class tutorialsController : Controller
    {
        private static readonly Dictionary<string, string> SlugToViewMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        { "node-js-intro", "Intro" },
        { "what-is-node-js", "whatisnode" },
        { "node-js-middleware", "Middleware" },
        // Add more as needed
    };
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult MongoDBtutorial()
        {
            return View();
        }
        public IActionResult NodeJs()
        {
            return View();
        }
        [Route("tutorials/nodejs/{topic}")]
        public IActionResult NodeJs(string topic)
        {

            if (SlugToViewMap.TryGetValue(topic, out var viewName))
            {
                return View("nodejs/"+viewName); // Opens Views/Tutorials/Nodejs/Intro.cshtml etc.
            }

            return NotFound(); // Or redirect to a default page
        }
    }
}

   
