using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoonShop.Controllers
{
    public class CaroselController : Controller
    {
        // GET: Carosel
        public ActionResult CaroselBaner()
        {
            return PartialView();
        }
        public ActionResult CaroselBanner()
        {
            return PartialView();
        }
        public ActionResult CaroselProductHot()
        {
            return PartialView();
        }
    }
}