using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Aiursoft.Pylon.Services
{
    public class AiurEmailSender
    {
        public async Task SendEmail(string target, string subject, string content)
        {
            SmtpClient client = new SmtpClient("smtp.mxhichina.com");
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("service@aiursoft.com", "L_()And[]16");
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("service@aiursoft.com");
            mailMessage.To.Add(target);
            mailMessage.Body = content;
            mailMessage.Subject = subject;
            await Task.Factory.StartNew(() => client.Send(mailMessage));
        }
    }
}
