using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YedideyChabad.Code;
using System.DirectoryServices.AccountManagement;
using System.Web.Security;
using YedideyChabad.Filters;
using YedideyChabad.Models;
using System.Data;
using MongoDB.Driver.Linq;
using YedideyChabad.Authentication.MongoDB;
using MongoDB.Driver;

namespace YedideyChabad.Controllers
{
    [InitializeSimpleMembership]
    [Authorize]
    public class StartController : Controller
    {
        MongoAuthenticationRepository _a = new MongoAuthenticationRepository();
        private MongoDatabase _db;

        public StartController() {
            _db = _a.GetMongoBatabase();
        }

        //
        // GET: /Start/
        public ActionResult Index()
        {
            //TODO: need to get the loggen in user i a different way
            MongoMembershipProvider m = new MongoMembershipProvider();
            bool isvalidUser = m.ValidateUser(User.Identity.Name, "");

            return View();
        }

        //
        // GET: /Start/GetAll
        public ActionResult GetAll()
        {   
            var collection = _db.GetCollection<Yadid>("Members");

            var yadid = (from y in collection.AsQueryable<Yadid>()
                         where y.OwnerUnique == Globals.OwnerId
                         select y).ToList();

            return View(yadid);
        }

        public ActionResult Select(string member)
        {
            Globals.SelectedMemberId = member;
            return RedirectToAction("Index", "YadidMain", new { id = member });
        }

    }
}
