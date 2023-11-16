using Sport.Libary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyClass.DAO;
using MyClass.Models;
using System.Web.Mvc;

namespace Sport.Controllers
{
    public class DangNhapController : Controller
    {
        UserDAO userDAO = new UserDAO();
        // GET: DangNhap
        public ActionResult DangKy()
        {
            return View("DangKy");
        }
       [HttpPost]
        public ActionResult DangKy(FormCollection filed, User user)
        {
            String fullname = filed["fullname"];
            String username = filed["username"];
            String phone = filed["phone"];
            String address = filed["address"];
            String email = filed["email"];
            String password = XString.ToMD5(filed["password"]);
            String repassword = XString.ToMD5(filed["repassword"]);
            String strError = "";
            if (fullname == null)
            {
                strError = "Nhập họ và tên";
            }
            if (phone == null)
            {
                strError = "Nhập số điện thoại";
            }
            if (address == null)
            {
                strError = "Nhập địa chỉ";
            }
            if (username == null)
            {
                strError = "Nhập tên đăng nhập";
            }
            if (email == null)
            {
                strError = "Nhập email";
            }
            if (password == null)
            {
                strError = "Nhập password";
            }
            if (repassword == null)
            {
                strError = "Nhập lại mật khẩu";
            }
            if(password != repassword)
            {
                strError = "Mật khẩu không trùng khớp";
            }
            user.FullName = fullname;
            user.Phone = phone;
            user.Address = address;
            user.UserName = username;
            user.Email = email;
            user.Password = password;
            user.Gender = "Chưa xác định";
            user.Roles = 0;
            user.Created_At = DateTime.Now;
            userDAO.Insert(user);
            return RedirectToAction("Home", "Site");
        }
        public ActionResult DangXuat()
        {
            Session["UserCustomer"] = "";
            return RedirectToAction("Home", "Site");
        }
        public ActionResult DangNhap()
        {
            return View("DangNhap");
        }
        [HttpPost]
        public ActionResult Login(FormCollection filed)
        {
            String username = filed["username"];
            String password = XString.ToMD5( filed["password"]);
            User row_user = userDAO.getRow(username, 0);
            String strError = "";
            if(row_user==null)
            {
                strError = "Tên đăng nhập không đúng";
            }
            else
            {
                if(password.Equals(row_user.Password))
                {
                    Session["UserCustomer"] = username;
                    Session["CustomerId"] = row_user.Id;
                    return RedirectToAction("Home", "Site");
                }   
                else
                {
                    strError = password;
                }    
            }    
            ViewBag.Error = "<span class='text-danger'>"+strError+"</div>";
            return View("DangNhap");
        }
    }
}