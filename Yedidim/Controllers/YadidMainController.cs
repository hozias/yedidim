    using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YedideyChabad.Models;
using YedideyChabad.Filters;
using YedideyChabad.Controllers;
using YedideyChabad.Code;
using YedideyChabad.Authentication.MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;
using MongoDB.Bson;

namespace YedideyChabad.Controllers
{
    [Authorize]
    public class YadidMainController : Controller
    {
        MongoAuthenticationRepository _a = new MongoAuthenticationRepository();
        private MongoDatabase _db;

        public YadidMainController() {
            _db = _a.GetMongoBatabase();
        }

        
        // GET: /YadidMain/
        /// <summary>
        /// reruen a collection of members
        /// or empty form for new member
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Index(string id)
        {
            if (id != Constants.KEY_ADD_NEW)
            {
                /// edit memberdetails
                var _uoi = new ObjectId(id);
                var _collection = _db.GetCollection<Yadid>("Members");

                var yadid = (from y in _collection.AsQueryable<Yadid>()
                             where y._id == _uoi  && y.OwnerUnique == Globals.OwnerId
                             select y).FirstOrDefault();

                if (yadid == null)
                {
                    return HttpNotFound();
                }

                return View("Index", yadid);
            }
            else
            {
                ///return new user view (empty form)
                return View("Index", new Yadid());
            }
        }

        
        // POST: /YadidMain/Index/5
        /// <summary>
        /// insert new member or update
        /// </summary>
        /// <param name="yadid"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(Yadid yadid, string id)
        {
            yadid.OwnerUnique = Globals.OwnerId;

            if (id == Constants.KEY_ADD_NEW)
            {
                //add new user
                if (ModelState.IsValid)
                {  
                    var collection = _db.GetCollection<Yadid>("Members");

                    yadid._id = new ObjectId();

                    collection.Insert(yadid);

                    return RedirectToAction("GetAll", "Start");
                }

                return View(yadid);
            }
            else
            {
                //update state
                if (ModelState.IsValid)
                {
                    yadid._id = new ObjectId(Globals.SelectedMemberId);

                    var collection = _db.GetCollection<Yadid>("Members");
                    collection.Save(yadid);

                    return RedirectToAction("Index");
                }
                return View(yadid);                     
            }
        }
        
        // GET: /YadidMain/Delete/5
        /// <summary>
        /// delete members & children
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(string id)
        {
            var _collMembers = _db.GetCollection<Yadid>("Members");
            var _collChildren = _db.GetCollection<Child>("Children");

            _collMembers.Remove(Query.EQ("_id",new ObjectId(id)));
            _collChildren.Remove(
                Query.And(
                        Query.EQ("OwnerUnique", Globals.OwnerId),
                        Query.EQ("YedidId", new ObjectId(id))
                        )
                    );

            return RedirectToAction("GetAll","Start");
        }


        protected override void Dispose(bool disposing)
        {
            ///MONGO-REPLACE
            
            //db.Dispose();
            base.Dispose(disposing);
        }
    }
}