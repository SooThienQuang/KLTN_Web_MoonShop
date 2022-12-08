using KLTN_Web_MoonShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KLTN_Web_MoonShop.Controllers.Android
{
    public class CartData
    {
        public long cartID { get; set; }
        public Nullable<long> productID { get; set; }
        public string productName { get; set; }
        public string  productImage { get; set; }
        public long productPrice { get; set; }
        public Nullable<int> cartQuantity { get; set; }
        public Nullable<long> cartMoney { get; set; }
    }
}