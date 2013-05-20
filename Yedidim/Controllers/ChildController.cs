using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YedideyChabad.Models;
using YedideyChabad.Code;
using YedideyChabad.Authentication.MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace YedideyChabad.Controllers
{
    public class ChildController : Controller
    {
        MongoAuthenticationRepository _a = new MongoAuthenticationRepository();
        private MongoDatabase _db;

        public ChildController() {
            _db = _a.GetMongoBatabase();
        }
        
        //
        // GET: /Start/GetChildren
        public ActionResult GetChildren()
        {
            //get all children for member
            var _collection = _db.GetCollection<Child>("Children");
            var _uoi = new ObjectId(Globals.SelectedMemberId);
            var _child = (from y in _collection.AsQueryable<Child>()
                         where y.OwnerUnique == Globals.OwnerId && y.YedidId == _uoi
                         select y).ToList();

            //return View(db.Children.ToList());
            return View(_child);
        }

        //
        // GET: /Child/Index
        public ActionResult Index(string id)
        {
            if (id != Constants.KEY_ADD_NEW)
            {
                var _uoi = new ObjectId(id);
                var _collection = _db.GetCollection<Child>("Children");

                var _child = (from y in _collection.AsQueryable<Child>()
                             where y._id == _uoi && y.OwnerUnique == Globals.OwnerId
                             select y).FirstOrDefault();

                if (_child == null)
                {
                    return HttpNotFound();
                }

                return View("Index", _child);
            }
            else
            {
                ///this is for creating a member
                ///
                return View("Index", new Child());
            }
        }


        /// <summary>
        /// insert new member or update
        /// </summary>
        /// <param name="child"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(Child child, string id)
        {
            child.OwnerUnique = Globals.OwnerId;
            child.YedidId = new ObjectId(Globals.SelectedMemberId);

            if (id == Constants.KEY_ADD_NEW)
            {
                //add new child
                if (ModelState.IsValid)
                {
                    var _collection = _db.GetCollection<Child>("Children");

                    child._id = new ObjectId();

                    _collection.Insert(child);

                    return RedirectToAction("GetChildren", "Child");
                }

                return View(child);
            }
            else
            {
                //update child
                if (ModelState.IsValid)
                {
                    var collection = _db.GetCollection<Child>("Children");
                    child._id = new ObjectId(id);
                    collection.Save(child);
                    
                    return RedirectToAction("Index");
                }
                return View(child);
            }
        }


        // GET: /YadidMain/Delete/5
        public ActionResult Delete(string id)
        {
            var _collChildren = _db.GetCollection<Child>("Children");

            _collChildren.Remove(
                Query.And(
                        Query.EQ("_id", new ObjectId(id)),
                        Query.EQ("OwnerUnique", Globals.OwnerId),
                        Query.EQ("YedidId", new ObjectId(Globals.SelectedMemberId))
                        )
                    );

            return RedirectToAction("GetChildren", "Child");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        // POST: /Child/UpdateChild/5
        //[HttpPost]
        //public ActionResult GetChildren(Child child)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(child).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("GetChildren");
        //    }
        //    return RedirectToAction("GetChildren");
        //}        











        ////
        //// GET: /Child/

        //public ActionResult Index()
        //{
        //    return View(db.Children.ToList());
        //}

        ////
        //// GET: /Child/Details/5

        //public ActionResult Details(int id = 0)
        //{
        //    Child child = db.Children.Find(id);
        //    if (child == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(child);
        //}

        ////
        //// GET: /Child/Create

        //public ActionResult Create()
        //{
        //    return View();
        //}

        ////
        //// POST: /Child/Create

        //[HttpPost]
        //public ActionResult Create(Child child)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Children.Add(child);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(child);
        //}

        ////
        //// GET: /Child/Edit/5

        //public ActionResult Edit(int id = 0)
        //{
        //    Child child = db.Children.Find(id);
        //    if (child == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(child);
        //}

        ////
        //// POST: /Child/Edit/5

        //[HttpPost]
        //public ActionResult Edit(Child child)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(child).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(child);
        //}

        ////
        //// GET: /Child/Delete/5

        //public ActionResult Delete(int id = 0)
        //{
        //    Child child = db.Children.Find(id);
        //    if (child == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(child);
        //}

        ////
        //// POST: /Child/Delete/5

        //[HttpPost, ActionName("Delete")]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Child child = db.Children.Find(id);
        //    db.Children.Remove(child);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

    }
}