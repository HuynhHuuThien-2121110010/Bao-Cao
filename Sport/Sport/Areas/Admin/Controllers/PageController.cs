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
    public class PageController : Controller
    {
        private PostDAO postDao = new PostDAO();
        private LinkDAO linkDAO = new LinkDAO();

        // GET: Admin/Post
        public ActionResult Index()
        {
            return View(postDao.getList("Index", "Page"));
        }

        // GET: Admin/Post/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postDao.getRow(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Admin/Post/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Post/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                post.PostType = "page";
                post.Slug = XString.Str_Slug(post.Title);
                post.Created_By = Convert.ToInt32(Session["UserId"].ToString());
                post.Created_At = DateTime.Now;
                var img = Request.Files["img"];
                //upload Img
                if (img.ContentLength != 0)
                {
                    string[] FileExtentions = new string[] { ".jpg", ".jepg", ".png", ".gif" };
                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))
                    {
                        string imgName = post.Slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        post.Img = imgName;
                        string PathDir = "~/Public/images/posts/";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        img.SaveAs(PathFile);
                    }
                }
                postDao.Insert(post);
                    TempData["message"] = new XMessenger("success", "Bài viết đã được thêm thành công!");  
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Admin/Post/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postDao.getRow(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Admin/Post/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                post.PostType = "page";
                post.Slug = XString.Str_Slug(post.Title);
                post.Updated_By = Convert.ToInt32(Session["UserId"].ToString());
                post.Updated_At = DateTime.Now;
                if(postDao.Update(post)==1)
                {
                    Link link = new Link();
                    link.Slug = post.Slug;
                    link.TableId = post.Id;
                    link.TypeLink = "page";
                    linkDAO.Insert(link);
                    TempData["message"] = new XMessenger("success", "Danh mục đã được lưu thành công!");
                }    
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Admin/Post/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postDao.getRow(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Admin/Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = postDao.getRow(id);
            postDao.Delete(post);
            return RedirectToAction("Index");
        }
        public ActionResult Trash()
        {
            return View(postDao.getList("Trash"));
        }
        //public ActionResult Status(int? id)
        //{
        //    if (id == null)
        //    {
        //        TempData["message"] = new XMessenger("danger", "Mã sản phẩm không tồn tại");
        //        return RedirectToAction("Index", "Product");
        //    }
        //    Product product = postDao.getRow(id);
        //    if (product == null)
        //    {
        //        TempData["message"] = new XMessenger("danger", "Mẫu Tin không tồn tại");
        //        return RedirectToAction("Index", "Product");
        //    }
        //    product.Status = (product.Status == 1) ? 2 : 1;
        //    product.Updated_By = Convert.ToInt32(Session["UserId"].ToString());
        //    product.Updated_At = DateTime.Now;
        //    productDAO.Update(product);
        //    TempData["message"] = new XMessenger("success", "Thay đổi trạng thái thành công!");
        //    return RedirectToAction("Index", "Product");
        //}
        public ActionResult DeTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessenger("danger", "Mã sản phẩm không tồn tại");
                return RedirectToAction("Index", "Product");
            }
            Post post = postDao.getRow(id);
            if (post == null)
            {
                TempData["message"] = new XMessenger("danger", "Mẫu Tin không tồn tại");
                return RedirectToAction("Index", "Product");
            }
            post.Status = 0;
            post.Updated_By = Convert.ToInt32(Session["UserId"].ToString());
            post.Updated_At = DateTime.Now;
            postDao.Update(post); 
            TempData["message"] = new XMessenger("success", "Đã xóa vào thùng rác!");
            return RedirectToAction("Index", "Product");
        }
        //public ActionResult ReTrash(int? id)
        //{
        //    if (id == null)
        //    {
        //        TempData["message"] = new XMessenger("danger", "Mã sản phẩm không tồn tại");
        //        return RedirectToAction("Trash", "Product");
        //    }
        //    Product product = productDAO.getRow(id);
        //    if (product == null)
        //    {
        //        TempData["message"] = new XMessenger("danger", "Mẫu Tin không tồn tại");
        //        return RedirectToAction("Trash", "Product");
        //    }
        //    product.Status = 2;
        //    product.Updated_By = Convert.ToInt32(Session["UserId"].ToString());
        //    product.Updated_At = DateTime.Now;
        //    productDAO.Update(product);
        //    TempData["message"] = new XMessenger("success", "Khôi phục thành công!");
        //    return RedirectToAction("Trash", "Product");
        //}
    }
}
