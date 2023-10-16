using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.Services.Sms
{
    public class SmsSendDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string Text { get; set; }
        public bool IsFlash { get; set; }
    }
}
