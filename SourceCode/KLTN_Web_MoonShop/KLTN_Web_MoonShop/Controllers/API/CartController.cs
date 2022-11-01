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
        public class person
        {
            public long proID { get; set; }
            public long cusID { get; set; }
            public int quantity { get; set; }
        }
        // GET: api/Cart
        DBCosmeticEntities db = new DBCosmeticEntities();
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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
        public string AddToCart(person obj)
        {
            try
            {
                CartDetail cd1 = new CartDetail();
                cd1=  db.CartDetails.ToList().LastOrDefault();
               
                Cart c = new Cart();
                CartDetail cd = new CartDetail();
              
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
                    CartDetail ccc = db.CartDetails.ToList().FirstOrDefault(n => n.productID == obj.proID && n.cartID == cartold.cartID);
                    if (ccc!=null)
                    {
                        ccc.cartQuantity = ccc.cartQuantity + obj.quantity;
                        ccc.cartMoney = ccc.cartQuantity * db.Products.ToList().FirstOrDefault(n => n.productID == obj.proID).productPrice;
                        db.CartDetails.AddOrUpdate(ccc);
                        db.SaveChanges();
                    }    
                    else
                    {
                        cd.cartID = cartold.cartID;
                        db.CartDetails.Add(cd);
                        db.SaveChanges();
                    }    
                }    
              
                
                return "Thêm giỏ hàng thành công";
            }
             catch
            {
                return "Thêm giỏ hàng thất bại";
            } 
        }
        // PUT: api/Cart/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Cart/5
        [HttpDelete]
        public void Delete(long id)
        {
        }
    }
}
