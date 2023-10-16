using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.Services.Sms
{
    public class SmsResponseDto
    {
        public string Value { get; set; }
        public int RetStatus { get; set; }
        public string StrRetStatus { get; set; }
    }
}
