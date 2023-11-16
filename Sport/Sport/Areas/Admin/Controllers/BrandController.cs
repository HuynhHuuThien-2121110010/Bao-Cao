using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Models;
using Sport.Libary;

namespace Sport.Areas.Admin.Controllers
{
    public class BrandController : Controller
    {
        BrandDAO brandDAO = new BrandDAO();
        LinkDAO linkDAO = new LinkDAO();
        // GET: Admin/Brand
        public ActionResult Index()
        {
            return View(brandDAO.getList("Index"));
        }

        // GET: Admin/Brand/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = brandDAO.getRow(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // GET: Admin/Brand/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Brand/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Brand brand)
        {
            if (ModelState.IsValid)
            {
                brand.Slug = XString.Str_Slug(brand.Name);
                var img = Request.Files["img"];
                if (img.ContentLength != 0)
                {
                    string[] FileExtentions = new string[] { ".jpg", ".jepg", ".png", ".gif" };
                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))
                    {
                        string imgName = brand.Slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        brand.Img = imgName;
                        string PathDir = "~/Public/images/brand/";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        img.SaveAs(PathFile);
                    }
                }
                brand.Created_By = Convert.ToInt32(Session["UserId"].ToString());
                brand.Created_At = DateTime.Now;
                //category.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
                if (brandDAO.Insert(brand) == 1)
                {
                    Link link = new Link();
                    link.Slug = brand.Slug;
                    link.TableId = brand.Id;
                    link.TypeLink = "brand";
                    linkDAO.Insert(link);
                }
                TempData["message"] = new XMessenger("success", "Danh mục đã được thêm thành công!");
                return RedirectToAction("Index");

            }
            return View(brand);
        }

        // GET: Admin/Brand/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = brandDAO.getRow(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // POST: Admin/Brand/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Brand brand)
        {
            if (ModelState.IsValid)
            {
                brand.Slug = XString.Str_Slug(brand.Name);
                var img = Request.Files["img"];
                if (img.ContentLength != 0)
                {
                    string[] FileExtentions = new string[] { ".jpg", ".jepg", ".png", ".gif" };
                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))
                    {
                        string imgName = brand.Slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        brand.Img = imgName;
                        string PathDir = "~/Public/images/brand/";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        //Xoa file ảnh
                        if (brand.Img.Length > 0)
                        {
                            string DelPath = Path.Combine(Server.MapPath(PathDir), brand.Img);
                            System.IO.File.Delete(DelPath);
                        }
                        img.SaveAs(PathFile);
                    }
                }
                brand.Updated_By = Convert.ToInt32(Session["UserId"].ToString());
                brand.Updated_At = DateTime.Now;
                if (brandDAO.Update(brand) == 1)
                {
                    Link link = linkDAO.getRow(brand.Id, "brand");
                    link.Slug = brand.Slug;
                    linkDAO.Update(link);
                }
                TempData["message"] = new XMessenger("success", "Đã Lưu!");
                return RedirectToAction("Index");
            }
           return View(brand);
        }

        // GET: Admin/Brand/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = brandDAO.getRow(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // POST: Admin/Brand/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Brand brand = brandDAO.getRow(id);
            Link link = linkDAO.getRow(brand.Id,"brand");
            if (brandDAO.Delete(brand) == 1)
            {
                linkDAO.Delete(link);
            }
            TempData["message"] = new XMessenger("success", "Đã xóa!");
            return RedirectToAction("Trash", "Category");
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessenger("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Brand");
            }
            Brand brand = brandDAO.getRow(id);
            if (brand == null)
            {
                TempData["message"] = new XMessenger("danger", "Mẫu Tin không tồn tại");
                return RedirectToAction("Index", "Brand");
            }
            brand.Status = (brand.Status == 1) ? 2 : 1;
            brand.Updated_By = Convert.ToInt32(Session["UserId"].ToString());
            brand.Updated_At = DateTime.Now;
            brandDAO.Update(brand);
            TempData["message"] = new XMessenger("success", "Thay đổi trạng thái thành công!");
            return RedirectToAction("Index", "Brand");
        }
        public ActionResult Trash()
        {
            return View(brandDAO.getList("Trash"));
        }
        public ActionResult DeTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessenger("danger", "Mã sản phẩm không tồn tại");
                return RedirectToAction("Index", "Brand");
            }
            Brand brand = brandDAO.getRow(id);
            if (brand == null)
            {
                TempData["message"] = new XMessenger("danger", "Mẫu Tin không tồn tại");
                return RedirectToAction("Index", "Brand");
            }
            brand.Status = 0;
            brand.Updated_By = Convert.ToInt32(Session["UserId"].ToString());
            brand.Updated_At = DateTime.Now;
            brandDAO.Update(brand);
            TempData["message"] = new XMessenger("success", "Đã xóa vào thùng rác!");
            return RedirectToAction("Index", "Brand");
        }
        public ActionResult ReTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessenger("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Trash", "Brand");
            }
            Brand brand = brandDAO.getRow(id);
            if (brand == null)
            {
                TempData["message"] = new XMessenger("danger", "Mẫu Tin không tồn tại");
                return RedirectToAction("Trash", "Brand");
            }
            brand.Status = 2;
            brand.Updated_By = Convert.ToInt32(Session["UserId"].ToString());
            brand.Updated_At = DateTime.Now;
            brandDAO.Update(brand);
            TempData["message"] = new XMessenger("success", "Khôi phục thành công!");
            return RedirectToAction("Trash", "Brand");
        }
    }
}
