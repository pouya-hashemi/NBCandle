using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.Data.Enums
{
    public enum ProductType
    {
        [Description("شمع")]
        candle,
        [Description("قالب")]
        mold
    }
}
