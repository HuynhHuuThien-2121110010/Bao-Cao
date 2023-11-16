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
    public class OrderController : Controller
    {
        private OrderDAO orderDAO = new OrderDAO();
        private OrderDetailDAO orderDetailDAO = new OrderDetailDAO();

        // GET: Admin/Order
        public ActionResult Index()
        {
            List<Order> list = orderDAO.getList("Index");
            return View(list);
        }

        // GET: Admin/Order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListDetails = orderDetailDAO.getList(id);
            return View(order);
        }

        // GET: Admin/Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Code,DeliveryName,DeliveryEmail,DeliveryPhone,DeliveryAddress,Created_By,Created_At,Updated_By,Updated_At,Status")] Order order)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            return View(order);
        }

        // GET: Admin/Order/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Order order = db.Orders.Find(id);
        //    if (order == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(order);
        //}

        // POST: Admin/Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Code,DeliveryName,DeliveryEmail,DeliveryPhone,DeliveryAddress,Created_By,Created_At,Updated_By,Updated_At,Status")] Order order)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(order).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(order);
        //}

        // GET: Admin/Order/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Order order = db.Orders.Find(id);
        //    if (order == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(order);
        //}

        // POST: Admin/Order/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Order order = db.Orders.Find(id);
        //    db.Orders.Remove(order);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        public ActionResult Huy(int? id)
        {
            Order order = orderDAO.getRow(id);
            if (order.Status == null)
            {
                TempData["message"] = new XMessenger("danger", "Mẫu tin không tồn tại!");
                return RedirectToAction("Index");
            }
            if (order.Status == 1 || order.Status == 2)
            {
                order.Status = 0;
                order.Updated_At = DateTime.Now;
                order.Updated_By = 1;
            }
            else
            {
                if (order.Status == 3)
                {
                    TempData["message"] = new XMessenger("danger", "Đơn hàng đang vận chuyển!");
                }
                if (order.Status == 4)
                {
                    TempData["message"] = new XMessenger("danger", "Đơn hàng đã được giao thành công!");
                }
            }
            orderDAO.Update(order);
            return RedirectToAction("Index");
        }
        public ActionResult XacNhan(int? id)
        {
            Order order = orderDAO.getRow(id);
            if (order.Status == null)
            {
                TempData["message"] = new XMessenger("danger", "Mẫu tin không tồn tại!");
                return RedirectToAction("Index");
            }
            else
            {
                order.Status = 2;
                order.Updated_At = DateTime.Now;
                order.Updated_By = 1;
            }
            orderDAO.Update(order);
            TempData["message"] = new XMessenger("success", "Thay đổi trạng thái thành công!");
            return RedirectToAction("Index");
        }
        public ActionResult DangVanChuyen(int? id)
        {
            Order order = orderDAO.getRow(id);
            if (order.Status == null)
            {
                TempData["message"] = new XMessenger("danger", "Mẫu tin không tồn tại!");
                return RedirectToAction("Index");
            }
            else
            {
                order.Status = 3;
                order.Updated_At = DateTime.Now;
                order.Updated_By = 1;
            }
            orderDAO.Update(order);
            TempData["message"] = new XMessenger("success", "Thay đổi trạng thái thành công!");
            return RedirectToAction("Index");
        }
        public ActionResult DaGiao(int? id)
        {
            Order order = orderDAO.getRow(id);
            if (order.Status == null)
            {
                TempData["message"] = new XMessenger("danger", "Mẫu tin không tồn tại!");
                return RedirectToAction("Index");
            }
            else
            {
                order.Status = 4;
                order.Updated_At = DateTime.Now;
                order.Updated_By = 1;
            }
            orderDAO.Update(order);
            TempData["message"] = new XMessenger("success", "Thay đổi trạng thái thành công!");
            return RedirectToAction("Index");
        }
        public ActionResult Trash()
        {
            return View(orderDAO.getList("Trash"));
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //public ActionResult DeTrash(int id)
        //{
        //    Order order = orderDAO.getRow(id);
            
        //    if (categoryDAO.Delete(category) == 1)
        //    {
        //        linkDAO.Delete(link);
        //    }
        //    TempData["message"] = new XMessenger("success", "Đã xóa!");
        //    return RedirectToAction("Trash", "Category");
        //}
        public ActionResult DeTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessenger("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Category");
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                TempData["message"] = new XMessenger("danger", "Mẫu Tin không tồn tại");
                return RedirectToAction("Index", "Category");
            }
            order.Status = 0;
            order.Updated_By = Convert.ToInt32(Session["UserId"].ToString());
            order.Updated_At = DateTime.Now;
            orderDAO.Update(order);
            TempData["message"] = new XMessenger("success", "Đã xóa vào thùng rác!");
            return RedirectToAction("Trash", "Order");
        }
    }
}
