using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NewsletterApp.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace NewsletterApp.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration config;

        public HomeController(IConfiguration configuration)
        {
            config = configuration;
        }
        public IActionResult SendEmail()
        {
            var mail = new MailMessage();
            var smtpServer = new SmtpClient(config["smtpServer"]);
            mail.From = new MailAddress(config["SendGridFrom"]);

            mail.To.Add("to email here");
            smtpServer.Port = Convert.ToInt32(config["sendGridPort"]);
            smtpServer.Credentials = new NetworkCredential(config["sendGridUser"], config["sendGridPassword"]);
            smtpServer.EnableSsl = Convert.ToBoolean(config["EnableSsl"]);

            mail.Subject = "Subject here";
            mail.Body = "Body message here";
            mail.IsBodyHtml = true;
            
            smtpServer.Send(mail);

            return View();
        }
        public  async Task<IActionResult> Index()
        {

            //var mail = new MailMessage();
            //var smtpServer = new SmtpClient(config["smtpServer"]);
            //mail.From = new MailAddress(config["SendGridFrom"]);


            //mail.To.Add("davidzagi@yahoo.com");
            //mail.To.Add("davidzagi93@gmail.com");
            //mail.To.Add("davidzagi@outlook.com");
            //smtpServer.Port = Convert.ToInt32(config["SendGridPort"]);
            //smtpServer.Credentials = new NetworkCredential(config["sendGridUserName"], config["sendGridPassword"]);
            //smtpServer.EnableSsl = Convert.ToBoolean(config["EnableSsl"]);

            //mail.Subject = "Subject here";
            //mail.Body = "A test using Send <b>Grid</b>";
            //mail.IsBodyHtml = true;

            //smtpServer.Send(mail);
            var apiKey = config["ApiKey"];
            var client = new SendGridClient(apiKey);
          
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("davidzagi@outlook.com", "DX Team"),
                Subject = "Sending with SendGrid is Fun",
                PlainTextContent = "and easy to do anywhere, even with C#",
                HtmlContent = "<strong>and easy to do anywhere, even with C#</strong>",
              
            };
            // msg.AddAttachment("C:\\Users\\David\\source\\repos\\NewsletterApp\\NewsletterApp\\Controllers\\me.jpg");
   
            msg.AddTo(new EmailAddress("davidzagi93@gmail.com", "Test User"));
            var response = await client.SendEmailAsync(msg);
            return View();
        }


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
