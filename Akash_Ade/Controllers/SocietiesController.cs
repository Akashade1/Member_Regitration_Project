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
    public class SocietiesController : Controller
    {
        private iBlueAnts_MembersEntities2 db = new iBlueAnts_MembersEntities2();

        public ActionResult Index()
        {
            return View(db.Societies.ToList());
        }

    
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Society society = db.Societies.Find(id);
            if (society == null)
            {
                return HttpNotFound();
            }
            return View(society);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SocietyName,IsActive")] Society society)
        {
            if (ModelState.IsValid)
            {
                society.Id = Guid.NewGuid();
                db.Societies.Add(society);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(society);
        }

    
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Society society = db.Societies.Find(id);
            if (society == null)
            {
                return HttpNotFound();
            }
            return View(society);
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SocietyName,IsActive")] Society society)
        {
            if (ModelState.IsValid)
            {
                db.Entry(society).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(society);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Society society = db.Societies.Find(id);
            if (society == null)
            {
                return HttpNotFound();
            }
            return View(society);
        }

    
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Society society = db.Societies.Find(id);
            db.Societies.Remove(society);
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
