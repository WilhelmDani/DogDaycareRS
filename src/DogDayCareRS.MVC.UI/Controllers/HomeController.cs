using DogDayCareRS.MVC.UI.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace DogDayCareRS.MVC.UI.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult About()
        {
            

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {


            return View();
        }

        //[HttpGet]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel cvm)
        {
            if (!ModelState.IsValid)
            {
                return View(cvm);
            }

            string message = $"You have received an email from {cvm.Name} with a subject " + $"{cvm.Subject}. Please respond to {cvm.Email} with " +
                             $"your response to the following message: <br/>{cvm.Message}";

            MailMessage mm = new MailMessage("admin@dani-dev.com", "Wilhelm_Danielle@Outlook.com", cvm.Subject, message);

            mm.IsBodyHtml = true;
            mm.Priority = MailPriority.High;
            mm.ReplyToList.Add(cvm.Email);

            SmtpClient client = new SmtpClient("mail.dani-dev.com");
            client.Credentials = new NetworkCredential("admin@dani-dev.com", "horror11121!");

            try
            {
                client.Send(mm);
            }
            catch (Exception ex)
            {
                ViewBag.CustomerMessage = $"We're Sorry, your request could not be completed at this time." +
                                          $" Please try again later. Error Message: <br/> {ex.StackTrace}";
                return View(cvm);
            }


            return View("EmailConfirmation", cvm);
        }


        public ActionResult Services()
        {

            return View();
        }
    }
}
