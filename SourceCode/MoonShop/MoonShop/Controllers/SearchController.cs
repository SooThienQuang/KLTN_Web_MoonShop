using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoonShop.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Search(string keyword)
        {
            return View();
        }
    }
}