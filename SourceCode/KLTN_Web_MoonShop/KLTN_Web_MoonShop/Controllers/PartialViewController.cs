using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KLTN_Web_MoonShop.Controllers
{
    public class PartialViewController : Controller
    {
        // GET: PartialView
        public ActionResult NavbarTop()
        {
            return PartialView();
        }


        public ActionResult CaroselHome()
        {
            return PartialView();
        }

        public ActionResult CaroselProductHome()
        {
            return PartialView();
        }
    }
}