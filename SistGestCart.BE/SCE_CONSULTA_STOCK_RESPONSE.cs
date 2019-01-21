using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistGestCart.BE
{
    public class SCE_CONSULTA_STOCK_RESPONSE
    {
        public int code { get; set; }
        public string message { get; set; }
        public SCE_DATA_STOCK data { get; set; }
    }

    public class SCE_DATA_STOCK 
    {
        public int totalStockRecords { get; set; }
        public object product { get; set; }
    }
}
