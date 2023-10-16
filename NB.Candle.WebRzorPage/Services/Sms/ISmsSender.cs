using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.Services.Sms
{
    public interface ISmsSender
    {
         Task Send(string body, string PhoneNumber);
        Task SendVerification(string PhoneNumber, string verificationCode);

    }
}
