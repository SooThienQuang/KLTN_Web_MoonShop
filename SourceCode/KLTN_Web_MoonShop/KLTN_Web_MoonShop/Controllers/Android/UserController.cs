using KLTN_Web_MoonShop.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KLTN_Web_MoonShop.Controllers.Android
{
    public class user
    {
        public string phone { get; set; }
        public string password { get; set; }
    }
    public class UserController : ApiController
    {
        DBCosmeticEntities db = new DBCosmeticEntities();
        MD5 md5 = new MD5();
        [Route("getpro")]
        [HttpGet]
        public IEnumerable<ProductData> getproduct()
        {
            List<ProductData> lst = new List<ProductData>();
            foreach(Product product in db.Products.Where(n=>n.isActive==1).ToList())
            {
                ProductDetail pd = db.ProductDetails.FirstOrDefault(n => n.ProductID == product.productID);
                ProductData pdd = new ProductData();
                pdd.productID=product.productID;
                pdd.productName=product.productName;
                pdd.productQuantity= product.productQuantity;
                pdd.productPrice= product.productPrice;
                pdd.productDescribe = product.productDescribe;
                pdd.productImage = product.productImage;
                pdd.productTypeID = product.productTypeID;
                pdd.isActive=product.isActive;
                pdd.Size = pd.Size;
                pdd.Color = pd.Color;
                pdd.img1 = pd.img1;
                pdd.img2 = pd.img2;
                pdd.img3 = pd.img3;
                pdd.origin = pd.origin;
                pdd.brand = pd.brand;
                pdd.desciption = pd.desciption;
                lst.Add(pdd);

            }
            return lst;
        }
        [Route("user")]
        [HttpGet]
        public string login(string phone, string password)
        {
            try
            {
                string pass = md5.CreateMD5(password);
                var item = db.Customers.FirstOrDefault(n => n.customerUserName == phone && n.customerPassword.Equals(pass)&&n.isActive==1);
                if (item == null)
                {
                    return null;
                }
                else
                    return item.customerID.ToString();
                        }
            catch
            {
                return null;
            }
        }
    }
}
