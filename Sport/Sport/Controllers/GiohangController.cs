using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Models;

namespace Sport.Controllers
{
    public class GiohangController : Controller
    {
        ProductDAO productDAO = new ProductDAO();
        UserDAO userDAO = new UserDAO();
        XCart xcart = new XCart();
        OrderDAO orderDAO = new OrderDAO();
        OrderDetailDAO orderDetailDAO = new OrderDetailDAO();
        // GET: Cart
        public ActionResult Index()
        {
            List<CartItem> listcart = xcart.GetCart();
            return View("Index", listcart);
        }
        public ActionResult AddCart(int productid)
        {
            Product product = productDAO.getRow(productid);
            CartItem cartitem = new CartItem(product.Id, product.Name, product.Img, product.Price, 1);
            List<CartItem> listcart = xcart.AddCart(productid, cartitem);
            return Json(new { count = listcart.Count });
        }
        public ActionResult DeleteCart(int productid)
        {
            xcart.DeleteCart(productid);
            return RedirectToAction("Index", "Giohang");
        }
        //UpdateCart
        public ActionResult UpdateCart(FormCollection form)
        {
            if (!string.IsNullOrEmpty(form["CAPNHAT"]))
            {
                var listqty = form["qty"];
                var listarr = listqty.Split(',');
                xcart.UpdateCart(listarr);
            }
            return RedirectToAction("Index", "Giohang");
        }
        public ActionResult DeleteCartAll()
        {
            xcart.DeleteCart();
            return RedirectToAction("Index", "Giohang");
        }
        //Thanh toan
        public ActionResult ThanhToan()
        {
            
            List<CartItem> listcart = xcart.GetCart();
            //Kiểm tra đăng nhập trang người dùng
            if (Session["UserCustomer"].Equals(""))
            {
                return Redirect("~/dang-nhap");
            }
            int userid = int.Parse(Session["CustomerId"].ToString());//Mã người đăng nhập
            User user = userDAO.getRow(userid);
            ViewBag.User = user;
            return View("ThanhToan",listcart);
        }
        public ActionResult DatHang(FormCollection field)
        {
            int userid = int.Parse(Session["CustomerId"].ToString());//Mã người đăng nhập
            User user = userDAO.getRow(userid);
            String deliveryName = field["deliveryName"];
            String deliveryGender = field["deliveryGender"];
            String deliveryEmail = field["deliveryEmail"];
            String deliveryPhone = field["deliveryPhone"];
            String deliveryAddress = field["deliveryAddress"];
            String email = field["email"];
            String phone = field["phone"];
            String address = field["address"];
            String note = field["note"];
            Order order = new Order();
            if (deliveryName == "")
            {
                order.User_Id = userid;
            }
            else
            {
                order.DeliveryName = deliveryName;
            }
            if (deliveryGender == "")
            {
                order.DeliveryGender = "Nam";
            }
            else
            {
                order.DeliveryGender = deliveryGender;
            }
            if (deliveryEmail == "")
            {
                order.DeliveryEmail = email;
            }
            else
            {
                order.DeliveryEmail = order.DeliveryEmail;
            }
            if (deliveryPhone == "")
            {
                order.DeliveryPhone = phone;
            }
            else
            {
                order.DeliveryPhone = order.DeliveryPhone;
            }
            if (deliveryAddress == "")
            {
                order.DeliveryAddress = address;
            }
            else
            {
                order.DeliveryAddress = order.DeliveryAddress;
            }
            //order.User_Id = userid;
            order.Note = note;
            order.Created_At = DateTime.Now;
            order.Status = 1;
            //// Gửi email
            //var message = new MailMessage();
            //message.From = new MailAddress("huynhthien91007@gmail.com");
            //message.To.Add(order.DeliveryEmail); // Lấy địa chỉ email từ OrderViewModel
            //message.Subject = "Xác nhận đơn hàng";
            //message.Body = "Đơn hàng của bạn đã được đặt thành công.";

            //using (var smtpClient = new SmtpClient())
            //{
            //    smtpClient.Send(message);
            //}
            //if (orderDAO.Insert(order) == 1)
            //{
            //    //Thêm vào Chi tiết đơn hàng
            //    List<CartItem> listcart = xcart.GetCart();
            //    foreach (CartItem cartItem in listcart)
            //    {
            //        OrderDetail orderDetail = new OrderDetail();
            //        orderDetail.Order_id = order.Id;
            //        orderDetail.Product_id = cartItem.ProductId;
            //        orderDetail.Price = cartItem.ProductPrice;
            //        orderDetail.Qty = cartItem.Qty;
            //        orderDetail.Amount = cartItem.Amount;
            //        orderDetailDAO.Insert(orderDetail);
            //    }
            //}
            DeleteCartAll();
            
            return Redirect("~/thanh-cong");
        }
    }
}