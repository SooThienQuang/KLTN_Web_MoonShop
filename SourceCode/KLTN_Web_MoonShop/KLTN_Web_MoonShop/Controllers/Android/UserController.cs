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
        [Route("postuser")]
        [HttpGet]
        public string signup(string fullname,string phone, string password)
        { 
                string pass = md5.CreateMD5(password);
                Customer customer = db.Customers.FirstOrDefault(n => n.customerUserName == phone && n.isActive == 1);
                if (customer != null)
                {
                    return "Tài khoản đã tồn tại";
                }
                else
                {
                    Customer cs = new Customer();
                    long id = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
                    cs.customerID = id;
                    cs.customerUserName = (phone).Trim();
                    cs.customerPassword = md5.CreateMD5(password);
                    cs.customerPhoto = "Sample_User_Icon.png".Trim();
                    cs.isActive = 1;
                    cs.dateCreate = DateTime.Now;
                    db.Customers.Add(cs);
                    db.SaveChanges();
                    //thông báo đăng kí thành công
                    Notification noti = new Notification();
                    long idnoti = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
                    noti.notiID = idnoti;
                    noti.receiveUserID = id;
                    noti.receiveUserFullName = fullname;
                    noti.title = "Chúc mừng bạn đã đăng kí tài khoản thành công";
                    noti.message = "Cám ơn bạn đã tin tưởng Shop , mong bạn có một trải nghiệm tốt bằng các dịch vụ của chúng mình !";
                    noti.image = "check.jpg";
                    noti.menutype = 1;
                    noti.isRead = 0;
                    db.Notifications.Add(noti);
                    db.SaveChanges();
                    //thông báo giảm giá khách hàng mới
                    Notification noti2 = new Notification();
                    long idnoti2 = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
                    noti2.notiID = idnoti2;
                    noti2.receiveUserID = id;
                    noti2.receiveUserFullName = fullname;
                    noti2.title = "Giảm giá cho khách hàng mới đến";
                    noti2.message = "Tặng bạn voucher giảm giá 20% ! Click để xem chi tiết";
                    noti2.image = "discount20.jpg";
                    noti2.menutype = 1;
                    noti2.isRead = 0;
                    db.Notifications.Add(noti2);
                    db.SaveChanges();
                    return "Đăng kí thành công";
                }
            return " Đăng kí thất bại";
            
        }
        [Route("getdetailpro")]
        [HttpGet]
        public IEnumerable<ProductData> getdetailpro(string id)
        {
            long proid = long.Parse(id);
            List<ProductData> lst = new List<ProductData>();
            Product product = db.Products.FirstOrDefault(n => n.isActive == 1 && n.productID == proid);
                ProductDetail pd = db.ProductDetails.FirstOrDefault(n => n.ProductID == product.productID);
                ProductData pdd = new ProductData();
                pdd.productID = product.productID;
                pdd.productName = product.productName;
                pdd.productQuantity = product.productQuantity;
                pdd.productPrice = product.productPrice;
                pdd.productDescribe = product.productDescribe;
                pdd.productImage = product.productImage;
                pdd.productTypeID = product.productTypeID;
                pdd.isActive = product.isActive;
                pdd.Size = pd.Size;
                pdd.Color = pd.Color;
                pdd.img1 = pd.img1;
                pdd.img2 = pd.img2;
                pdd.img3 = pd.img3;
                pdd.origin = pd.origin;
                pdd.brand = pd.brand;
                pdd.desciption = pd.desciption;
                lst.Add(pdd);
            return lst;
        }
    }
}
