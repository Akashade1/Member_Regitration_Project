using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Akash_Ade.Models;
using DocumentFormat.OpenXml.Spreadsheet;
using MailKit.Search;
using Microsoft.Ajax.Utilities;

namespace Akash_Ade.Controllers
{
    public class ClubMembersController : Controller
    {
        private iBlueAnts_MembersEntities2 db = new iBlueAnts_MembersEntities2();


        public ActionResult Index(string SearchTerm)
        {
            var clubMembers = db.ClubMembers.AsQueryable();


            if (!string.IsNullOrEmpty(SearchTerm))
            {
                clubMembers = clubMembers.Where(m => m.MemberName.Contains(SearchTerm));
            }
            return View(clubMembers);
        }

        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClubMember clubMember = db.ClubMembers.Find(id);
            if (clubMember == null)
            {
                return HttpNotFound();
            }
            return View(clubMember);
        }


        public ActionResult Create()
        {
            ViewBag.HobbiesId = new SelectList(db.Hobbies, "Id", "HobbyName");
            ViewBag.SocietyId = new SelectList(db.Societies, "Id", "SocietyName");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MemberName,SocietyId,Gender,MembershipCategory,HobbiesId,Remark,IsActive")] ClubMember clubMember)
        {
            if (ModelState.IsValid)
            {
                clubMember.Id = Guid.NewGuid();
                db.ClubMembers.Add(clubMember);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HobbiesId = new SelectList(db.Hobbies, "Id", "HobbyName", clubMember.HobbiesId);
            ViewBag.SocietyId = new SelectList(db.Societies, "Id", "SocietyName", clubMember.SocietyId);
            return View(clubMember);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClubMember clubMember = db.ClubMembers.Find(id);
            if (clubMember == null)
            {
                return HttpNotFound();
            }
            ViewBag.HobbiesId = new SelectList(db.Hobbies, "Id", "HobbyName", clubMember.HobbiesId);
            ViewBag.SocietyId = new SelectList(db.Societies, "Id", "SocietyName", clubMember.SocietyId);
            return View(clubMember);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MemberName,SocietyId,Gender,MembershipCategory,HobbiesId,Remark,IsActive")] ClubMember clubMember)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clubMember).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HobbiesId = new SelectList(db.Hobbies, "Id", "HobbyName", clubMember.HobbiesId);
            ViewBag.SocietyId = new SelectList(db.Societies, "Id", "SocietyName", clubMember.SocietyId);
            return View(clubMember);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClubMember clubMember = db.ClubMembers.Find(id);
            if (clubMember == null)
            {
                return HttpNotFound();
            }
            return View(clubMember);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ClubMember clubMember = db.ClubMembers.Find(id);
            db.ClubMembers.Remove(clubMember);
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

        public ActionResult Active()
        {
            var activeMembers = db.ClubMembers
                                  .Where(m => m.IsActive == 1) 
                                  .ToList();

            return View(activeMembers);
        }

        public ActionResult IsNotActive()
        {
            var activeMembers = db.ClubMembers
                                  .Where(m => m.IsActive == 0 )
                                  .ToList();

            return View(activeMembers);
        }
    }
}