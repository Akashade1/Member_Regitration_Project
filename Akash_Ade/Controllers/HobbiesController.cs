using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Akash_Ade.Models;

namespace Akash_Ade.Controllers
{
    public class HobbiesController : Controller
    {
        private iBlueAnts_MembersEntities2 db = new iBlueAnts_MembersEntities2();


        public ActionResult Index()
        {
            return View(db.Hobbies.ToList());
        }

       
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hobby hobby = db.Hobbies.Find(id);
            if (hobby == null)
            {
                return HttpNotFound();
            }
            return View(hobby);
        }

     
        public ActionResult Create()
        {
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,HobbyName,IsActive")] Hobby hobby)
        {
            if (ModelState.IsValid)
            {
                hobby.Id = Guid.NewGuid();
                db.Hobbies.Add(hobby);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hobby);
        }

      
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hobby hobby = db.Hobbies.Find(id);
            if (hobby == null)
            {
                return HttpNotFound();
            }
            return View(hobby);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,HobbyName,IsActive")] Hobby hobby)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hobby).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hobby);
        }

 
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hobby hobby = db.Hobbies.Find(id);
            if (hobby == null)
            {
                return HttpNotFound();
            }
            return View(hobby);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Hobby hobby = db.Hobbies.Find(id);
            db.Hobbies.Remove(hobby);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
