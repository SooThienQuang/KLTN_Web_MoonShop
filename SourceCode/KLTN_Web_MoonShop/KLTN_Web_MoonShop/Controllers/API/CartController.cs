using KLTN_Web_MoonShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.ModelBinding;

namespace KLTN_Web_MoonShop.Controllers.API
{
    public class CartController : ApiController
    {
         public class cartTam
    {
        public long id { get; set; }
        public string name { get; set; }
        public long quantity { get; set; }
        public long price { get; set; }
        public string img { get; set; }
        public long money { get; set; }
    }
        public class person
        {
            public long proID { get; set; }
            public long cusID { get; set; }
            public long cartID { get; set; }
            public int quantity { get; set; }
        }
        // GET: api/Cart
        DBCosmeticEntities db = new DBCosmeticEntities();
        [Route("getcart")]
        [HttpPost]
        public IEnumerable<Cart> getcart(person ob)
        {
            return db.Carts.ToList();
        }
        [Route("countcart")]
        public IEnumerable<Cart> Count(long id)
        {
            return db.Carts.ToList();
        }
        //Thêm giỏ hàng
        public bool Get(long id)
        {
            try
            {
                return true;
            }
            catch
            {
                return false;
            }
           
        }
        [HttpPost]
        public int AddToCart(person obj)
        {
            try
            {
                CartDetail cd1 = new CartDetail();
                cd1=  db.CartDetails.ToList().LastOrDefault();
               
                Cart c = new Cart();
                CartDetail cd = new CartDetail();
                cd.isActive = 1;
                if (cd1 == null)
                {
                    cd.cartDetailID = 0;
                }
                else
                {
                    cd.cartDetailID = cd1.cartDetailID+1;
                }
                long id = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
                c.cartID = id;
                c.customerID = obj.cusID;
                c.createDate = DateTime.Now;
                cd.productID = obj.proID;
                cd.cartID = id;
                cd.cartQuantity = obj.quantity;
                cd.cartMoney = obj.quantity * db.Products.ToList().FirstOrDefault(n => n.productID == obj.proID).productPrice;

                Cart cartold =new Cart();
                cartold= db.Carts.ToList().FirstOrDefault(n => n.customerID == obj.cusID);
                if (cartold==null)
                {
                    db.Carts.Add(c);
                    db.CartDetails.Add(cd);
                    db.SaveChanges();
                }
                else
                {
                    CartDetail ccc = db.CartDetails.ToList().FirstOrDefault(n => n.productID == obj.proID && n.cartID == cartold.cartID&&n.isActive==1);
                    if (ccc!=null)
                    {
                        ccc.cartQuantity = ccc.cartQuantity + obj.quantity;
                        ccc.cartMoney = ccc.cartQuantity * db.Products.ToList().FirstOrDefault(n => n.productID == obj.proID).productPrice;
                        db.CartDetails.AddOrUpdate(ccc);
                        db.SaveChanges();
                    }    
                    else
                    {
                        cd.createTime = DateTime.Now;
                        cd.cartID = cartold.cartID;
                        db.CartDetails.Add(cd);
                        db.SaveChanges();
                    }    
                }
                Cart cca = db.Carts.FirstOrDefault(n => n.customerID == obj.cusID);
                int sl=db.CartDetails.Where(n=>n.cartID==cca.cartID&&n.isActive==1).Count();
                return sl;
            }
             catch
            {
                return 0;
            } 
        }
        [HttpPut]        
        public string Put(person id)
        {
            try
            {
                Product pro = db.Products.ToList().FirstOrDefault(n => n.productID == id.proID);
                CartDetail cd = db.CartDetails.FirstOrDefault(n => n.cartID == id.cartID && n.productID == id.proID);
                if (id.quantity==0)
                {
                    cd.isActive = -1;
                }  
                else
                {
                    cd.cartQuantity = id.quantity;
                    cd.cartMoney = cd.cartQuantity * pro.productPrice;
                }
                db.CartDetails.AddOrUpdate(cd); 
                db.SaveChanges();
                return "Thêm giỏ hàng thành công";
            }
            catch
            {
                return "Thất bại";
            }
        }

        // DELETE: api/Cart/5
        [HttpDelete]
        public string Delete(person id)
        {
            try
            {
                Cart cart = db.Carts.FirstOrDefault(n => n.customerID == id.cusID);
                if(cart!=null)
                {
                    CartDetail cd = db.CartDetails.FirstOrDefault(n => n.cartID == cart.cartID && n.productID == id.proID&&n.isActive==1);
                    cd.isActive = -1;
                    db.CartDetails.AddOrUpdate(cd);
                    db.SaveChanges();
                }
                return "Xóa thành công";
            }
            catch
            {
                return "Có lỗi xảy ra";
            }
           
        }
    }
}
