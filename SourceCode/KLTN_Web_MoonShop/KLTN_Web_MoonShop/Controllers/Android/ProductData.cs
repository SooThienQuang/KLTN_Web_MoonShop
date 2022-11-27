using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KLTN_Web_MoonShop.Controllers.Android
{
    public class ProductData
    {
        public long productID { get; set; }
        public string productName { get; set; }
        public Nullable<long> productQuantity { get; set; }
        public Nullable<long> productPrice { get; set; }
        public string productDescribe { get; set; }
        public string productImage { get; set; }
        public Nullable<int> productTypeID { get; set; }
        public Nullable<int> isActive { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public string img1 { get; set; }
        public string img2 { get; set; }
        public string img3 { get; set; }
        public string origin { get; set; }
        public string brand { get; set; }
        public string desciption { get; set; }
    }
}