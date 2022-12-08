using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KLTN_Web_MoonShop.Controllers.Android
{
    public class NotifyData
    {
            public long notiID { get; set; }
            public Nullable<long> sendUserID { get; set; }
            public string sendUserFullName { get; set; }
            public Nullable<long> receiveUserID { get; set; }
            public string receiveUserFullName { get; set; }
            public Nullable<long> objectID { get; set; }
            public Nullable<long> objectTypeID { get; set; }
            public string title { get; set; }
            public string message { get; set; }
            public string image { get; set; }
            public Nullable<long> menutype { get; set; }
            public Nullable<int> isRead { get; set; }
        }
    
}