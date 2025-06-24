using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net.Mail;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using DevFunda.Models;

namespace Devfunda.Controllers
{
    public class HomeController : Controller
    {

        private readonly IConfiguration _configuration;
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Route("aboutus")]
        public IActionResult aboutus()
        {
            return View();
        }

        public IActionResult LoadEnrollForm()
        {
          return PartialView("_enrollform");
        }
        [HttpPost]
        public IActionResult SubmitEnrollForm(Enrollform model)
        {
            if (ModelState.IsValid)
            {
                // process data here (save to DB, etc.)
                //Mail to Admin
                MailMessage message = new MailMessage();
                message.From = new MailAddress(_configuration["DemoSessionEmail:SMTP_Email"]);
                message.To.Add(new MailAddress(_configuration["DemoSessionEmail:To_Email"]));
                message.Subject = "Demo Class Request";
                message.IsBodyHtml = true;

                message.Body = model.description + "<Br> Submit by " + model.email + "<BR> phone" + model.phone;
                message.Body += "<br> name" + model.Name;
                SmtpClient client = new SmtpClient(_configuration["DemoSessionEmail:SMTP"]);

                {
                    client.UseDefaultCredentials = true;

                    // client.EnableSsl = false;
                    //  client.Port = 80;

                    client.Credentials = new NetworkCredential(_configuration["DemoSessionEmail:SMTP_Email"], _configuration["DemoSessionEmail:SMTP_Pwd"]);

                }

                try
                {
                    client.Send(message);
                }
                catch (Exception e)
                {
                    TempData["emsg"] = "email not sent";
                    return PartialView("_enrollform");
                }


                return Content("<p class='text-success'>Form submitted successfully!</p>");
            }

            // If validation fails, return form with validation errors
            ViewData["emsg"] = "Please fill all the fields";
             return Content("<p class='bs-danger'>Please fill all the fields!</p>");
        }

        private async Task<bool> VerifyRecaptcha(string captchaResponse)
        {
            var secretKey = "6Ld6zDgaAAAAAAB0FmuQxz4tRZmWvFSxVNRPzaQl";
            var client = new HttpClient();

            var content = new FormUrlEncodedContent(new[]
            {
        new KeyValuePair<string, string>("secret", secretKey),
        new KeyValuePair<string, string>("response", captchaResponse)
    });

            var response = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", content);
            var json = await response.Content.ReadAsStringAsync();

            dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

            return result.success == "true";
        }


        [HttpPost]

        public async Task<IActionResult> DemoClassRequest(Contact_Model contact)
        {


            //Contact_Model contact = new Contact_Model();
            //contact.Name = Request.Form["name"];
            //contact.phone = Request.Form["phone"];
            //contact.email = Request.Form["email"];
            //contact.description = Request.Form["description"];

            #region MyRegion

            //var captchaResponse = Request.Form["g-recaptcha-response"];

            var isCaptchaValid = true;// await VerifyRecaptcha(captchaResponse);


            if (isCaptchaValid)
            {

                #endregion
                //check captcha


                //end captcha

                //Mail to Admin
                MailMessage message = new MailMessage();
                message.From = new MailAddress(_configuration["DemoSessionEmail:SMTP_Email"]);
                message.To.Add(new MailAddress(_configuration["DemoSessionEmail:To_Email"]));
                message.Subject = "Demo Class Request";
                message.IsBodyHtml = true;

                message.Body = contact.description + "<Br> Submit by " + contact.email + "<BR> phone" + contact.phone;
                message.Body += "<br> name" + contact.Name;
                SmtpClient client = new SmtpClient(_configuration["DemoSessionEmail:SMTP"]);

                {
                    client.UseDefaultCredentials = true;

                    // client.EnableSsl = false;
                    //  client.Port = 80;

                    client.Credentials = new NetworkCredential(_configuration["DemoSessionEmail:SMTP_Email"], _configuration["DemoSessionEmail:SMTP_Pwd"]);

                }

                try
                {
                    client.Send(message);
                }
                catch (Exception e)
                {
                    TempData["msg"] = "email not sent";

                }


                message.Dispose();
                MailMessage message1 = new MailMessage();
                //Mail to Customer
                message1.From = new MailAddress(_configuration["DemoSessionEmail:SMTP_Email"]);
                message1.To.Add(new MailAddress(contact.email));
                message1.Subject = "Demo class request submitted - DevFunda.com";
                message1.IsBodyHtml = true;

                string body = "";
                body = "Hi, <br> Greetings!<br> We have received your request for demo class. We will contact you soon.";
                message1.Body = body;


                SmtpClient client2 = new SmtpClient(_configuration["DemoSessionEmail:SMTP"]);

                {
                    client2.UseDefaultCredentials = true;

                    // client.EnableSsl = false;
                    //  client.Port = 80;

                    client2.Credentials = new NetworkCredential(_configuration["DemoSessionEmail:SMTP_Email"], _configuration["DemoSessionEmail:SMTP_Pwd"]);

                }

                try
                {
                    client2.Send(message1);
                }
                catch (Exception e)
                {
                    TempData["msg"] = "email not sent";

                }

                TempData["msg"] = "Thanks for details, we will contact you soon";
            }
            //TempData["msg"] = "Please check, captcha box";
            return RedirectToAction("index");

        }


    }
}
