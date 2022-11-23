

using System.Net;
using System.Net.Mail;

namespace UbyTECAPI.Controllers
{
    public class Email
    {


       

        public void sendEmail(string email, string password)
        {
            /*
            string body = @"<h1> Nueva Cuenta </h1>
                              <b> " + "Correo: " + email + "</b>" +
                              "<b> " + "Contraseña: " + password + "</b>";*/
            string body = "HOLAAAA";
            MailMessage msg = new MailMessage("divadtec@gmail.com",email,"Nueva cuenta UbyTec", body);
            //msg.IsBodyHtml= true;

            SmtpClient sc = new SmtpClient("smtp.gmail.com", 587);
            NetworkCredential cre = new NetworkCredential("divadtec@gmail.com", "Ardinko0907TEC");
            sc.Credentials = cre;
            sc.EnableSsl= true;
            sc.Send(msg);


        }


    }
}
