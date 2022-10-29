using KLTN_Web_MoonShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KLTN_Web_MoonShop.Controllers.Admin
{
    public class ActionLogController : Controller
    {
        // GET: ActionLog
        DBCosmeticEntities db = new DBCosmeticEntities();
        public ActionResult List()
        {
            return View(db.ActionLogs.ToList());
        }
    }
}