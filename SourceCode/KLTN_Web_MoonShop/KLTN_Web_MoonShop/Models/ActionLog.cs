//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KLTN_Web_MoonShop.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ActionLog
    {
        public long actionLogID { get; set; }
        public Nullable<long> userID { get; set; }
        public string userName { get; set; }
        public string actionName { get; set; }
        public string modun { get; set; }
        public Nullable<int> actionType { get; set; }
        public Nullable<System.DateTime> actionDate { get; set; }
        public Nullable<long> idOject { get; set; }
        public string nameTable { get; set; }
    
        public virtual Customer Customer { get; set; }
    }
}
