using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Models;

namespace Sport.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        private SportDbContext db = new SportDbContext();
        UserDAO userDAO = new UserDAO();

        // GET: Admin/User
        public ActionResult Index()
        {
            return View(userDAO.getList("Index"));
        }
        public ActionResult Custommer()
        {
            return View(userDAO.getList("Custommer"));
        }
        // GET: Admin/User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Admin/User/Create
        public ActionResult Create()
        {
            List<SelectListItem> dropdownValues = new List<SelectListItem>
                {
                    new SelectListItem { Value = "0", Text = "Khách hàng" },
                    new SelectListItem { Value = "1", Text = "Quản trị" },
                    new SelectListItem { Value = "2", Text = "Thành viên" }
                };
            ViewBag.DropdownValues = dropdownValues;
            ViewBag.GetlistUser = new SelectList(userDAO.getList("Custommer"), "Id", "Name", 0);
            return View();
        }

        // POST: Admin/User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Admin/User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FullName,UserName,Password,Email,Gender,Phone,Img,Roles,Address,Created_By,Created_At,Updated_By,Updated_At,Status")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Admin/User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public string NameCustomer(int userid)
        {
            User user = userDAO.getRow(userid);
            if (user == null)
            {
                return "";
            }
            else
            {
                return user.FullName;
            }
        }
    }
}
