using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KLTN_Web_MoonShop.Models
{
    public class CartClass
    {
        public int Id { get; set; }
        public string name { get; set; }
        public int soluong { get; set; }
        public float gia { get; set; }
        public float thanhtien { get; set; }
    }
}