using Microsoft.EntityFrameworkCore;
using NB.Candle.WebRzorPage.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.Services.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public EmailSender(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }
        private async Task SendEmail(string message,string subject, string emailAddress)
        {
            var emailConfig = await _applicationDbContext.EmailConfigurations.FirstOrDefaultAsync();
            using (SmtpClient smtpClient = new SmtpClient(emailConfig.SmtpHost, emailConfig.Port))
            {
                smtpClient.Credentials = new System.Net.NetworkCredential(emailConfig.EmailAddress, emailConfig.Password);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = true;
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailConfig.EmailAddress, emailConfig.DisplayName);
                    mail.To.Add(new MailAddress(emailAddress));
                    mail.Body = message;
                    mail.Subject = subject;
                    mail.IsBodyHtml = true;

                    smtpClient.Send(mail);
                }


            }
        }

        public async Task SendResetPasswodEmail(string url,string emailAddress)
        {
            var message = "<span style=\"display:block;width:100%;text-align:center;\">برای تغیر رمز خود روی لینک زیر کلیک کنید:</span>";
            message += "<a href=\"" + url + "\">تغیر رمز عبور</a>";
            await SendEmail(message,"بازیابی رمز عبور", emailAddress);
        }
    }
}
