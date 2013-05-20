using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YedideyChabad.Code;

namespace YedideyChabad.Controllers
{
    public class UtilsController : Controller
    {
        //
        // GET: /Utils/GetHebDate
        
        [HttpPost]
        public ActionResult GetHebDate(string Date)
        {
            ///parse date
            DateTime dt = Convert.ToDateTime(Date);

            string hdate = HebrewDate.ToHebDate(dt.Day, dt.Month, dt.Year);

            return Json(new { HebDate = hdate });
        }

    }
}
