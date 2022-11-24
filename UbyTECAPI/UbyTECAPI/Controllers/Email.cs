

using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;
using System.Net.Mail;

namespace UbyTECAPI.Controllers
{
    public class Email
    {


       
        public async Task sendEmail(string email, string user, string password)
        {
            var apikey = "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";

            var client = new SendGridClient(apikey);

            var from = new EmailAddress("ubytec0907@gmail.com", "UbyTec");
            var to = new EmailAddress(email);

            var subject = "Nueva cuenta de UbyTec";

            var plainTextContent = "";
            var htmlContent = "<h1> UbyTec </h1>" +"<h2> Usuario: "+ user+" </h2>\n\n"+ "<h2> Contraseña: " + password + " </h2>\n\n";
            var msg = MailHelper.CreateSingleEmail(
                from,
                to,
                subject,
                plainTextContent,
                htmlContent
                );

            client.SendEmailAsync(msg);

        }
        
     


    }
}
