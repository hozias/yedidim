using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YedideyChabad.Authentication.MongoDB;
using YedideyChabad.Models;

namespace YedideyChabad.Controllers
{
    public class AdminController : Controller
    {

        MongoAuthenticationRepository _a = new MongoAuthenticationRepository();
        private MongoDatabase _db;

        public AdminController() {
            _db = _a.GetMongoBatabase();
        }

        // GET: /Info/

        public ActionResult Info()
        {
            int _memCount = (int)_db.GetCollection<Yadid>("Members").Count();
            int _userCount = (int)_db.GetCollection<Yadid>("Users").Count();

            Info _info = new Info { Members = _memCount, Users = _userCount };

            return View(_info);
        }

    }
}
