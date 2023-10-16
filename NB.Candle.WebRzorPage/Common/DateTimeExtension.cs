using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.Common
{
    public static class DateTimeExtension
    {
        public static string PersianDateFormatString( this DateTime dateTime)
        {
            return dateTime.ToLocalTime().ToString("yyyy/MM/dd - hh:mm");
        }
    }
}
