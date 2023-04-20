using Microsoft.AspNetCore.Mvc;
using SAT.UI.MVC.Models;
using System.Diagnostics;
using SAT.DATA.EF.Models;
using MimeKit;
using MailKit.Net.Smtp;

namespace SAT.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public IActionResult Index()
        {
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

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Contact(ContactViewModel cvm)
        {
            //when a class has validation attributes, that validation should be checked
            //BEFORE attempting to process any of the data they provided.
            if (!ModelState.IsValid)
            {
                //Send them back to the form. we can pass the object to the view
                //so the form will contain the original info they provided.
                return View(cvm);
            }
            //To handle sending the email, we'll need to install a NuGet package.

            //MIME -> Multipurpose Internet Mail Extensions - Allows email to contain
            //info other thatn ASCII -> Audio, Video, Images, and HTML

            //SMTP - Simple Mail Transfer Protocol -> unencrytped - an internet protocol
            //like HTTP that specializes in the collection & transfer of email data

            #region Email Setup Steps & Email Info
            //1. Go to Tools > NuGet Package Manager > Manage NuGet Packages for Solution
            //2. Go to the Browse tab and search for NETCore.MailKit
            //3. Click NETCore.MailKit
            //4. On the right, check the box next to the CORE1 project, then click "Install"
            //5. Once installed, return here
            //6. Add the following using statements & comments:
            //      - using MimeKit; //Added for access to MimeMessage class
            //      - using MailKit.Net.Smtp; //Added for access to SmtpClient class
            //7. Once added, return here to continue coding email functionality
            #endregion

            //create and format the message content we will receive from the contact form.
            string message = $"You have received a new email from your site's contact form!<br>" +
                $"Sender: {cvm.Name}<br>" +
                $"Email: {cvm.Email}<br>" +
                $"Message: {cvm.Message}";

            //Build a MimeMessage object to assist with transporting the email info.
            var mm = new MimeMessage();

            //We can access the credentials for this email user from our appsettings.json file
            mm.From.Add(new MailboxAddress("Sender", _config.GetValue<string>("Credentials:Email:User")));

            mm.To.Add(new MailboxAddress("Personal", _config.GetValue<string>("Credentials:Email:Recipient")));

            mm.ReplyTo.Add(new MailboxAddress("User", cvm.Email));

            mm.Subject = cvm.Subject;

            //the body of the message will be formatted with the string we created above
            mm.Body = new TextPart("HTML") { Text = message };

            //we can set the prio of a message to "Urgent" so it will be flagged in our email client.
            mm.Priority = MessagePriority.Urgent;

            //using directive - create the SmtpClient object used to actully send the email.
            //Once all of the code is done, it will clise any open connections and dispose of the object
            //for us
            using (var client = new SmtpClient())
            {
                try
                {
                    //connect to the mail server using the credentials in our appesttings.json
                    //client.Connect(_config.GetValue<string>("Credentials:Email:Client"));
                    // should be port 25. if blocked uncomment below
                    //handle alt ports
                    client.Connect(_config.GetValue<string>("Credentials:Email:Client"), 8889);

                    //login to the mail server using your credentials
                    client.Authenticate(
                        //UserName
                        _config.GetValue<string>("Credentials:Email:User"),
                        //Password
                        _config.GetValue<string>("Credentials:Email:Password")
                        );
                    //send the email
                    client.Send(mm);
                }
                //handle any issues.
                catch (Exception ex)
                {
                    //if there are ANY issues, we can store an error message in a ViewBag variable
                    //and display in the view
                    ViewBag.ErrorMessage = $"There was an error processing your request. Please try again later.<br>Error Message: {ex.Message}";

                    return View(cvm);
                }//end try catch
            }//end using
            //if all goes well, return a view that displays a confirmation message to the user that their email was sent.
            return View("EmailConfirmation", cvm);
        }
    }
}