using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.Services.Email
{
    public interface IEmailSender
    {
        Task SendResetPasswodEmail(string url, string emailAddress);
    }
}
