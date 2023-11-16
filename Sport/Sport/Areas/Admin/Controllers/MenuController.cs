using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Models;
using Sport.Libary;

namespace Sport.Areas.Admin.Controllers
{
    public class MenuController : Controller
    {
        CategoryDAO categoryDAO = new CategoryDAO();
        TopicDAO topicDAO = new TopicDAO();
        PostDAO postDAO = new PostDAO();
        MenuDAO menuDAO = new MenuDAO();
        // GET: Admin/Menu
        public ActionResult Index()
        {
            ViewBag.ListCategory = categoryDAO.getList("Index");
            ViewBag.ListTopic = topicDAO.getList("Index");
            ViewBag.ListPage = postDAO.getList("Index", "Page");
            List<Menu> menu = menuDAO.getList("Index");
            return View("Index", menu);
        }
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            if (!string.IsNullOrEmpty(form["ThemCategory"]))
            {
                if (!string.IsNullOrEmpty(form["itemCat"]))
                {
                    var listitem = form["itemCat"];//1,1,1
                    var listarr = listitem.Split(',');
                    foreach (var row in listarr)
                    {
                        //Id của category
                        int id = int.Parse(row);
                        Category category = categoryDAO.getRow(id);
                        Menu menu = new Menu();
                        menu.Name = category.Name;
                        menu.Link = category.Slug;
                        menu.Table_id = category.Id;
                        menu.Type = "category";
                        menu.Position = form["Position"];
                        menu.PerenId = 0;
                        menu.Sort_order = 0;
                        menu.Created_By = Convert.ToInt32(Session["UserId"].ToString());
                        menu.Created_At = DateTime.Now;
                        menu.Status = 1;
                        menuDAO.Insert(menu);
                    }
                    TempData["message"] = new XMessenger("success", "Thêm thành công!");
                }
                else
                {
                    TempData["message"] = new XMessenger("danger", "Chưa chọn danh mục sản phẩm!");
                }
            }
            //Topic
            if (!string.IsNullOrEmpty(form["ThemTopic"]))
            {
                if (!string.IsNullOrEmpty(form["itemTopic"]))
                {
                    var listitem = form["itemTopic"];//1,1,1
                    var listarr = listitem.Split(',');
                    foreach (var row in listarr)
                    {
                        //Id của category
                        int id = int.Parse(row);
                        Topic topic = topicDAO.getRow(id);
                        Menu menu = new Menu();
                        menu.Name = topic.Name;
                        menu.Link = topic.Slug;
                        menu.Table_id = topic.Id;
                        menu.Type = "topic";
                        menu.Position = form["Position"];
                        menu.PerenId = 0;
                        menu.Sort_order = 0;
                        menu.Created_By = Convert.ToInt32(Session["UserId"].ToString());
                        menu.Created_At = DateTime.Now;
                        menu.Status = 1;
                        menuDAO.Insert(menu);
                    }
                    TempData["message"] = new XMessenger("success", "Thêm thành công!");
                }
                else
                {
                    TempData["message"] = new XMessenger("danger", "Chưa chọn danh mục sản phẩm!");
                }
            }
            //page
            if (!string.IsNullOrEmpty(form["ThemPage"]))
            {
                if (!string.IsNullOrEmpty(form["itemPage"]))
                {
                    var listitem = form["itemPage"];//1,1,1
                    var listarr = listitem.Split(',');
                    foreach (var row in listarr)
                    {
                        //Id của category
                        int id = int.Parse(row);
                        Post post = postDAO.getRow(id);
                        Menu menu = new Menu();
                        menu.Name = post.Title;
                        menu.Link = post.Slug;
                        menu.Table_id = post.Id;
                        menu.Type = "page";
                        menu.Position = form["Position"];
                        menu.PerenId = 0;
                        menu.Sort_order = 0;
                        menu.Created_By = Convert.ToInt32(Session["UserId"].ToString());
                        menu.Created_At = DateTime.Now;
                        menu.Status = 1;
                        menuDAO.Insert(menu);
                    }
                    TempData["message"] = new XMessenger("success", "Thêm thành công!");
                }
                else
                {
                    TempData["message"] = new XMessenger("danger", "Chưa chọn danh mục sản phẩm!");
                }
            }
            //Thêm Custom
            if (!string.IsNullOrEmpty(form["ThemCustom"]))
            {
                if (!string.IsNullOrEmpty(form["name"]) && !string.IsNullOrEmpty(form["link"]))
                {
                    Menu menu = new Menu();
                    menu.Name = form["name"];
                    menu.Link = form["link"];
                    menu.Type = "custom";
                    menu.Position = form["Position"];
                    menu.PerenId = 0;
                    menu.Sort_order = 0;
                    menu.Created_By = Convert.ToInt32(Session["UserId"].ToString());
                    menu.Created_At = DateTime.Now;
                    menu.Status = 1;
                    menuDAO.Insert(menu);
                    TempData["message"] = new XMessenger("success", "Thêm thành công!");
                }
                else
                {
                    TempData["message"] = new XMessenger("danger", "Chưa nhập đủ thông tin!");
                }
            }
            return RedirectToAction("Index", "Menu");
        }
        public ActionResult Edit(int? id)
        {
            ViewBag.ListMenu = new SelectList(menuDAO.getList("Index"), "Id", "Name");
            ViewBag.ListOrder = new SelectList(menuDAO.getList("Index"), "Sort_order", "Name");
            Menu menu = menuDAO.getRow(id);
            return View("Edit", menu);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Menu menu)
        {
            if (ModelState.IsValid)
            {
                if (menu.PerenId == null)
                {
                    menu.PerenId = 0;
                }
                if (menu.Sort_order == null)
                {
                    menu.Sort_order = 1;
                }
                else
                {
                    menu.Sort_order += 1;
                }
                menu.Updated_By = Convert.ToInt32(Session["UserId"].ToString());
                menu.Updated_At = DateTime.Now;
                menuDAO.Update(menu);
                TempData["message"] = new XMessenger("success", "Đã Lưu!");
                return RedirectToAction("Index");
            }
            ViewBag.ListMenu = new SelectList(menuDAO.getList("Index"), "Id", "Name");
            ViewBag.ListOrder = new SelectList(menuDAO.getList("Index"), "Sort_order", "Name");
            return View(menu);
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessenger("danger", "Mẫu tin không tồn tại!");
                return RedirectToAction("Index");
            }
            Menu menu = menuDAO.getRow(id);
            if (menu == null)
            {
                TempData["message"] = new XMessenger("danger", "Mẫu tin không tồn tại!");
                return RedirectToAction("Index");
            }
            menu.Status = (menu.Status == 1) ? 2 : 1;
            menu.Updated_By = Convert.ToInt32(Session["UserId"].ToString());
            menu.Updated_At = DateTime.Now;
            menuDAO.Update(menu);
            TempData["message"] = new XMessenger("success", "Thay đổi trạng thái thành công!");
            return RedirectToAction("Index");
        }
        public ActionResult DeTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessenger("danger", "Mẫu tin không tồn tại!");
                return RedirectToAction("Index");
            }
            Menu menu = menuDAO.getRow(id);
            if (menu == null)
            {
                TempData["message"] = new XMessenger("danger", "Mẫu tin không tồn tại!");
                return RedirectToAction("Index");
            }
            menu.Status = 0;
            menu.Updated_By = Convert.ToInt32(Session["UserId"].ToString());
            menu.Updated_At = DateTime.Now;
            menuDAO.Update(menu);
            TempData["message"] = new XMessenger("success", "Đã xóa vào thùng rác!");
            return RedirectToAction("Index");
        }
        public ActionResult Trash()
        {
            List<Menu> menu = menuDAO.getList("Trash");
            return View("Trash",menu);
        }
        public ActionResult ReTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessenger("danger", "Mẫu tin không tồn tại!");
                return RedirectToAction("Index");
            }
            Menu menu = menuDAO.getRow(id);
            if (menu == null)
            {
                TempData["message"] = new XMessenger("danger", "Mẫu tin không tồn tại!");
                return RedirectToAction("Index");
            }
            menu.Status = 2;
            menu.Updated_By = Convert.ToInt32(Session["UserId"].ToString());
            menu.Updated_At = DateTime.Now;
            menuDAO.Update(menu);
            TempData["message"] = new XMessenger("success", "Khôi phục thành công!");
            return RedirectToAction("Trash");
        }
    }
}