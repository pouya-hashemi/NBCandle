using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.Data.Enums
{
    public enum OrderStatus
    {
        [Description("در انتظار تایید")]
        Submited,
        [Description("تایید شده")]
        Confirmed,
        [Description("ارسال شده")]
        Shipped,
        [Description("تحویل داده شده")]
        Delivered
    }
}
