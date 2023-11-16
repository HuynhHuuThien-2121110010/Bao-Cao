using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;

namespace Sport.Areas.Admin.Views
{
    public class ExportController : Controller
    {
        private SportDbContext db = new SportDbContext();

        // GET: Admin/Export
        public ActionResult Index()
        {
            return View(db.Exports.ToList());
        }

        // GET: Admin/Export/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Export export = db.Exports.Find(id);
            if (export == null)
            {
                return HttpNotFound();
            }
            return View(export);
        }

        // GET: Admin/Export/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Export/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductName,ExportDate,Quantity,Customer")] Export export)
        {
            if (ModelState.IsValid)
            {
                db.Exports.Add(export);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(export);
        }

        // GET: Admin/Export/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Export export = db.Exports.Find(id);
            if (export == null)
            {
                return HttpNotFound();
            }
            return View(export);
        }

        // POST: Admin/Export/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductName,ExportDate,Quantity,Customer")] Export export)
        {
            if (ModelState.IsValid)
            {
                db.Entry(export).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(export);
        }

        // GET: Admin/Export/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Export export = db.Exports.Find(id);
            if (export == null)
            {
                return HttpNotFound();
            }
            return View(export);
        }

        // POST: Admin/Export/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Export export = db.Exports.Find(id);
            db.Exports.Remove(export);
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
