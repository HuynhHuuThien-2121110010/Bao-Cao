using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;
using MyClass.DAO;
using PagedList;
using System.Net.Mail;
using System.Net;

namespace Sport.Controllers
{
    public class SiteController : Controller
    {
        LinkDAO linkDAO = new LinkDAO();
        ProductDAO productDAO = new ProductDAO();
        PostDAO postDAO = new PostDAO();
        CategoryDAO categoryDAO = new CategoryDAO();
        // GET: Site
        public ActionResult Index(string slug = null, int? page = null)
        {
            if (slug == null)
            {
                return this.Home();
            }
            else
            {
                Link link = linkDAO.getRow(slug);
                if (link != null)
                {
                    string typelink = link.TypeLink;
                    switch (typelink)
                    {
                        case "category":
                            {
                                return this.ProductCategory(slug, page);
                            }
                        case "topic":
                            {
                                return this.PostTopic(slug, page);
                            }
                        case "page":
                            {
                                return this.PostPage(slug);
                            }
                        default:
                            {
                                return this.Error404(slug);
                            }

                    }
                }
                else
                {
                    Product product = productDAO.getRow(slug);
                    if (product != null)
                    {
                        return this.ProductDetail(product);
                    }
                    else
                    {
                        Post post = postDAO.getRow(slug, "Page");
                        if (post != null)
                        {
                            return this.PostDetail(post);
                        }
                        else
                        {
                            return this.Error404(slug);
                        }
                    }
                }
            }
            //SportDbContext db = new SportDbContext();
            //int somau = db.Products.Count();
            //ViewBag.Somau = somau;
            //return View();
        }
        [HttpPost]
        public ActionResult SendEmail(FormCollection filed)
        {
            // Tạo đối tượng MailMessage
            MailMessage mail = new MailMessage();
            String sentemail = filed["email"];
            String senttieude = filed["tieude"];
            String sentnoidung = filed["noidung"];
            mail.From = new MailAddress(sentemail);  // Địa chỉ email người gửi
            mail.To.Add("huynhthien91007@gmail.com");  // Địa chỉ email người nhận
            mail.Subject = senttieude;  // Chủ đề email
            mail.Body = sentnoidung;  // Nội dung email

            // Tạo đối tượng SmtpClient và cấu hình thông tin SMTP của Gmail
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("huynhthien91007@gmail.com", "Thien0342591007@");
            smtpClient.EnableSsl = true;

            // Gửi email
            smtpClient.Send(mail);

            return View();
        }
        public ActionResult Map()
        {
            return View();
        }
        public ActionResult Home()
        {
            List<Category> list = categoryDAO.getListByParentId(0);
            return View("Home", list);
        }
        public ActionResult HomeProduct(int id)
        {
            Category category = categoryDAO.getRow(id);
            ViewBag.Category = category;
            // Sản phẩm theo danh mục loại 3 cấp
            // Cấp 1
            List<int> listcatid = new List<int>();
            listcatid.Add(id);
            // Cấp 2
            List<Category> listcategory2 = categoryDAO.getListByParentId(id);
            if (listcategory2.Count() != 0)
            {
                foreach (var category2 in listcategory2)
                {
                    listcatid.Add(category2.Id);
                    //Cấp 3
                    List<Category> listcategory3 = categoryDAO.getListByParentId(category2.Id);
                    if (listcategory3.Count() != 0)
                    {
                        foreach (var category3 in listcategory3)
                            listcatid.Add(category3.Id);
                    }
                }
            }
            List<ProductInfo> list = productDAO.getListByListCatId(listcatid, 8);
            return View("HomeProduct", list);
        }
        public ActionResult Product(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 9;
            IPagedList<ProductInfo> list = productDAO.getList(pageSize, pageNumber);
            return View("Product", list);
        }
        public ActionResult ProductCategory(string slug, int? page)
        {

            int pageNumber = page ?? 1;
            int pageSize = 9;
            Category category = categoryDAO.getRow(slug);
            ViewBag.Category = category;
            // Sản phẩm theo danh mục loại 3 cấp
            // Cấp 1
            List<int> listcatid = new List<int>();
            listcatid.Add(category.Id);
            // Cấp 2
            List<Category> listcategory2 = categoryDAO.getListByParentId(category.Id);
            if (listcategory2.Count() != 0)
            {
                foreach (var category2 in listcategory2)
                {
                    listcatid.Add(category2.Id);
                    //Cấp 3
                    List<Category> listcategory3 = categoryDAO.getListByParentId(category2.Id);
                    if (listcategory3.Count() != 0)
                    {
                        foreach (var category3 in listcategory3)
                            listcatid.Add(category3.Id);
                    }
                }
            }
            IPagedList<ProductInfo> list = productDAO.getListByListCatId(listcatid, pageSize, pageNumber);
            return View("ProductCategory", list);
        }
        public ActionResult NewProduct(Product product)
        {
            return View("NewProduct", product);
        }
        public ActionResult ProductDetail(Product product)
        {
            return View("ProductDetail", product);
        }
        // Nhóm bài viết
        public ActionResult Post()
        {
            List<PostInfo> list = postDAO.getList("Post");
            return View("Post", list);
        }
        //PostLastNews
        public ActionResult PostLastNews()
        {
            return View("PostLastNews");
        }
        public ActionResult PostTopic(string slug, int? page)
        {
            return View("PostTopic");
        }
        public ActionResult PostPage(string slug)
        {
            Post post = postDAO.getRow(slug, "page");
            return View("PostPage", post);
        }
        public ActionResult PostDetail(Post post)
        {
            ViewBag.ListOther = postDAO.getListByTopicId(post.TopicId, "Post");
            return View("PostDetail", post);
        }
        // Lỗi
        public ActionResult Error404(string slug)
        {
            return View("Error404");
        }
    }
}