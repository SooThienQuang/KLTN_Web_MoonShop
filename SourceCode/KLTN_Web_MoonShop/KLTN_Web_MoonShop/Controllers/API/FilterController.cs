using KLTN_Web_MoonShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace KLTN_Web_MoonShop.Controllers.API
{
    public class datafilter
    {
        public string loai { get; set; } 
        public long gia1 { get; set; }
        public long gia2 { get; set; }
        public int typeprice { get; set; }   
    }
    public class product2
    {
        public long productID { get; set; }
        public string productName { get; set; }
        public Nullable<long> productQuantity { get; set; }
        public Nullable<long> productPrice { get; set; }
        public string productDescribe { get; set; }
        public string productImage { get; set; }
    }
    public class FilterController : ApiController
    {
        DBCosmeticEntities db = new DBCosmeticEntities();
        ReplaceUnitcode replace = new ReplaceUnitcode();
        [HttpPost]
        public List<product2> Filter(datafilter data)
        {
            var lsttam = db.Products.Where(n => n.isActive == 1).ToList();
            List<Product> lst = new List<Product>();
            foreach (var item in lsttam)
            {
                Product d = item;
                if (item.productDescribe.Length > 30)
                {
                    d.productDescribe = item.productDescribe.Substring(0, 30);
                }
                if (item.productName.Length > 30)
                {
                    d.productName = item.productName.Substring(0, 30);
                }
                lst.Add(d);
            }
            List<Product> main = new List<Product>();
            foreach (var item in lst)
            {
                string[] mang = null;
                if (data.loai != null)
                {
                    mang = data.loai.Split(',');
                }
                if (mang != null)
                {
                    foreach (string s in mang)
                    {
                        try
                        {
                            int idloai = int.Parse(s);
                            if (idloai >= 0 && idloai == item.productTypeID)
                            {
                                Product d = item;
                                main.Add(d);
                            }
                        }
                        catch
                        {
                            ProductDetail de = db.ProductDetails.FirstOrDefault(n => n.ProductID == item.productID);
                            string ten1 = replace.RemoveUnicode(s).ToLower();
                            string ten2 = replace.RemoveUnicode(de.origin).ToLower();
                            if (ten1.Contains(ten2))
                            {
                                Product d2 = item;
                                main.Add(d2);
                            }
                        }
                    }
                }
            }
            if (data.gia2 > 0 && data.gia1 == 0)
            {
                main = main.Where(n => n.productPrice <= data.gia2).ToList();
            }
            if (data.gia1 >= 0 && data.gia2 > 0)
            {
                main = main.Where(n => n.productPrice <= data.gia2 && n.productPrice >= data.gia1).ToList();
            }

            var mm = main.Select(x => new product2
            {
                productID = x.productID,
                productName = x.productName,
                productDescribe = x.productDescribe,
                productImage = x.productImage,
                productQuantity=x.productQuantity,
                productPrice=x.productPrice
            }
            ).ToList();
            return mm;
        }

     
        [Route("filterprice")]
        [HttpPost]
        public List<product2>filterprice(datafilter da)
        {
            var lsttam = db.Products.Where(n => n.isActive == 1).ToList();
            List<Product> lst = new List<Product>();
            foreach (var item in lsttam)
            {
                Product d = item;
                if (item.productDescribe.Length > 30)
                {
                    d.productDescribe = item.productDescribe.Substring(0, 30);
                }
                if (item.productName.Length > 30)
                {
                    d.productName = item.productName.Substring(0, 30);
                }
                lst.Add(d);
            }
            if(da.typeprice==1)
            {
                lst = lst.OrderBy(n => n.productPrice).ToList();
            }   
            if(da.typeprice==2)
                lst = lst.OrderByDescending(n => n.productPrice).ToList();
            var mm = lst.Select(x => new product2
            {
                productID = x.productID,
                productName = x.productName,
                productDescribe = x.productDescribe,
                productImage = x.productImage,
                productQuantity = x.productQuantity,
                productPrice = x.productPrice
            }
          ).ToList();
            return mm;
        }
    }
}
