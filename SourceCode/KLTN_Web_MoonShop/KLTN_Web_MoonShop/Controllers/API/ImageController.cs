using KLTN_Web_MoonShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.ModelBinding;

namespace KLTN_Web_MoonShop.Controllers.API
{
    public class dataImage
    {
        public long id { get; set; }
        public string name { get; set; }
        public string b64 { get; set; }
        public string chuoiname { get; set; }
        public int type { get; set; }
    }
    public class imageController : ApiController
    {
        DBCosmeticEntities db = new DBCosmeticEntities();
        [HttpDelete]
        public string delete(dataImage id)
        {
            int count = db.images.Where(n => n.type == 0&&n.isActive==1).Count();
            if(count==1)
            {
                return "Dữ liệu không được bỏ trống";
            }    
            else
            {
                var item = db.images.FirstOrDefault(n => n.isActive == 1 && n.id == id.id);
                item.isActive = -1;
                db.images.AddOrUpdate(item);
                db.SaveChanges();
                return "Thành công";
            }    
          
        }
        [HttpPost]
        public string post(List<dataImage>lstimg)
        {
            if (lstimg.Count() > 1 )
            {
                dataImage last = lstimg.LastOrDefault();
                //type =0 la carosel trang chủ
                if(last!=null&&last.type==0)
                {
                    string[] hinh = last.chuoiname.Split(',');
                    foreach(string h in hinh)
                    {
                        if(h!="")
                        {
                            foreach (dataImage di in lstimg)
                            {
                                if (di.name.Equals(h))
                                {
                                    
                                    //save img
                                    string imgbase6 = di.b64.Substring(di.b64.LastIndexOf(',') + 1);
                                    byte[] imageBytes = Convert.FromBase64String(imgbase6);
                                    MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                                    ms.Flush();
                                    ms.Position = 0;
                                    ms.Write(imageBytes, 0, imageBytes.Length);
                                    System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                                    var mappedPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Asset/img/banner/");
                                    image.Save(mappedPath + h);
                                    image imk = db.images.ToList().LastOrDefault();
                                    KLTN_Web_MoonShop.Models.image img = new image();
                                    img.id = imk.id+1;
                                    img.name=h;
                                    img.type = 0;
                                    img.isActive = 1;
                                    db.images.Add(img);
                                    db.SaveChanges();
                                }
                            }
                        }    
                        
                    }    
                }    
                return "Thành công";
            }
            else
            {
                return "Vui lòng chọn hình ảnh";
            }
           

        }
    }
}
