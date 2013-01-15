using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using sbContact.Models;
using System.Configuration;



namespace sbContact.Controllers
{
    public class EmailController : Controller
    {
        //
        // GET: /Email/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Sendit(EmailFields Mailer)
        {

            MailMessage email = new MailMessage
            {
                Sender = new MailAddress(Mailer.Email),
                Subject = Mailer.Subject,
                Body = Mailer.Message
            };
            //email.To.Add(new MailAddress("whover.@whatver.com"));
            email.To.Add(System.Configuration.ConfigurationManager.AppSettings["EmailTo"].ToString());
            SmtpClient client = new SmtpClient();
            //client.Credentials = new NetworkCredential(username, password);
            client.Send(email);
            return View();
        }

    }
}