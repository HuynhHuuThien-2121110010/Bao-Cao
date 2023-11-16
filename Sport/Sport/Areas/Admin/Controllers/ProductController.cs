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
using OfficeOpenXml;
using OfficeOpenXml.Drawing;

namespace Sport.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private ProductDAO productDAO = new ProductDAO();
        private ProductSaleDAO productSaleDAO = new ProductSaleDAO();
        private ProductStoreDAO productStoreDAO = new ProductStoreDAO();
        private CategoryDAO categoryDAO = new CategoryDAO();
        private BrandDAO brandDAO = new BrandDAO();
        private LinkDAO linkDAO = new LinkDAO();

        // GET: Admin/Product
        public ActionResult Index()
        {
            return View(productDAO.getList("Index"));
        }
        public ActionResult GetProductSale()
        {
            return View(productDAO.GetProductSale());
        }
        // GET: Admin/Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Product/Create
        public ActionResult Create()
        {
            ViewBag.ListCat = new SelectList(categoryDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListBrand = new SelectList(brandDAO.getList("Index"), "Id", "Name", 0);
            //ViewBag.ListOrder = new SelectList(productDAO.getList("Index"), "Sort_Order", "Name", 0);
            return View();
        }

        // POST: Admin/Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product, ProductStore productStore)
        {
            if (ModelState.IsValid)
            {
                product.Slug = XString.Str_Slug(product.Name);
                if (product.CatId == null)
                {
                    product.CatId = 0;
                }
                if (product.BrandId == null)
                {
                    product.BrandId = 0;
                }
                if (product.Sort_Order == null)
                {
                    product.Sort_Order = 1;
                }
                else
                {
                    product.Sort_Order += 1;
                }
                //upload file
                var img = Request.Files["img"];
                if (img.ContentLength != 0)
                {
                    string[] FileExtentions = new string[] { ".jpg", ".jepg", ".png", ".gif" };
                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))
                    {
                        string imgName = product.Slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        product.Img = imgName;
                        string PathDir = "~/Public/images/products/";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        img.SaveAs(PathFile);
                    }
                }
                product.Created_By = Convert.ToInt32(Session["UserId"].ToString());
                product.Created_At = DateTime.Now;
                product.Promotion = false;
                //supplier.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
                productDAO.Insert(product);
                productStore.ProductId = product.Id;
                productStore.Qty = 0;
                productStore.Price = product.Price;
                productStore.Created_By = Convert.ToInt32(Session["UserId"].ToString());
                productStore.Created_At = DateTime.Now;
                productStoreDAO.Insert(productStore);

                //if (productDAO.Insert(product) == 1)
                //{
                //    Link link = new Link();
                //    link.Slug = product.Slug;
                //    link.TableId = product.Id;
                //    link.TypeLink = "product";
                //    linkDAO.Insert(link);
                //}
                TempData["message"] = new XMessenger("success", "Danh mục đã được thêm thành công!");
                return RedirectToAction("Index");
            }
            ViewBag.ListCat = new SelectList(categoryDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListBrand = new SelectList(brandDAO.getList("Index"), "Id", "Name", 0);
            //ViewBag.ListOrder = new SelectList(productDAO.getList("Index"), "Sort_Order", "Name", 0);
            return View(product);
        }

        // GET: Admin/Product/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.ListCat = new SelectList(categoryDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListBrand = new SelectList(brandDAO.getList("Index"), "Id", "Name", 0);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Slug = XString.Str_Slug(product.Name);
                if (product.CatId == null)
                {
                    product.CatId = 0;
                }
                if (product.BrandId == null)
                {
                    product.BrandId = 0;
                }

                var img = Request.Files["img"];
                if (img.ContentLength != 0)
                {
                    string[] FileExtentions = new string[] { ".jpg", ".jepg", ".png", ".gif" };
                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))
                    {
                        string imgName = product.Slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        product.Img = imgName;
                        string PathDir = "~/Public/images/products/";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        //Xoa file ảnh
                        if (product.Img.Length > 0)
                        {
                            string DelPath = Path.Combine(Server.MapPath(PathDir), product.Img);
                            System.IO.File.Delete(DelPath);
                        }
                        img.SaveAs(PathFile);
                    }
                }
                product.Updated_By = Convert.ToInt32(Session["UserId"].ToString());
                product.Updated_At = DateTime.Now;
                productDAO.Update(product);
                //if (productDAO.Update(product) == 1)
                //{
                //    Link link = linkDAO.getRow(product.Id, "product");
                //    link.Slug = product.Slug;
                //    linkDAO.Update(link);
                //}
                TempData["message"] = new XMessenger("success", "Đã Lưu!");
                return RedirectToAction("Index");
            }
            ViewBag.ListCat = new SelectList(categoryDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListBrand = new SelectList(brandDAO.getList("Index"), "Id", "Name", 0);
            return View(product);
        }
        // GET: Admin/Product/Delete/5
        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = productDAO.getRow(id);
            ProductStore productStore = productStoreDAO.getRow(product.Id);
            if (productDAO.Delete(product) == 1)
            {
                productStoreDAO.Delete(productStore);
            }
            TempData["message"] = new XMessenger("success", "Đã xóa!");
            return RedirectToAction("Trash", "Product");
        }
        public ActionResult Trash()
        {
            return View(productDAO.getList("Trash"));
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessenger("danger", "Mã sản phẩm không tồn tại");
                return RedirectToAction("Index", "Product");
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                TempData["message"] = new XMessenger("danger", "Mẫu Tin không tồn tại");
                return RedirectToAction("Index", "Product");
            }
            product.Status = (product.Status == 1) ? 2 : 1;
            product.Updated_By = Convert.ToInt32(Session["UserId"].ToString());
            product.Updated_At = DateTime.Now;
            productDAO.Update(product);
            TempData["message"] = new XMessenger("success", "Thay đổi trạng thái thành công!");
            return RedirectToAction("Index", "Product");
        }
        public ActionResult DeTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessenger("danger", "Mã sản phẩm không tồn tại");
                return RedirectToAction("Index", "Product");
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                TempData["message"] = new XMessenger("danger", "Mẫu Tin không tồn tại");
                return RedirectToAction("Index", "Product");
            }
            product.Status = 0;
            product.Updated_By = Convert.ToInt32(Session["UserId"].ToString());
            product.Updated_At = DateTime.Now;
            productDAO.Update(product);
            TempData["message"] = new XMessenger("success", "Đã xóa vào thùng rác!");
            return RedirectToAction("Index", "Product");
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        public ActionResult ReTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessenger("danger", "Mã sản phẩm không tồn tại");
                return RedirectToAction("Trash", "Product");
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                TempData["message"] = new XMessenger("danger", "Mẫu Tin không tồn tại");
                return RedirectToAction("Trash", "Product");
            }
            product.Status = 2;
            product.Updated_By = Convert.ToInt32(Session["UserId"].ToString());
            product.Updated_At = DateTime.Now;
            productDAO.Update(product);
            TempData["message"] = new XMessenger("success", "Khôi phục thành công!");
            return RedirectToAction("Trash", "Product");
        }
        public string NameImg(int? productid)
        {
            Product product = productDAO.getRow(productid);
            if (product == null)
            {
                return "";
            }
            else
            {
                return product.Img;
            }
        }
        public string NameProduct(int? productid)
        {
            Product product = productDAO.getRow(productid);
            if (product == null)
            {
                return "";
            }
            else
            {
                return product.Name;
            }
        }
        public ActionResult ProductSale(int? id)
        {
            ViewBag.ListCat = new SelectList(categoryDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListBrand = new SelectList(brandDAO.getList("Index"), "Id", "Name", 0);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductSale(Product product, string datestart, string dateend, int priceSale, ProductSale productSale)
        {

            DateTime dateStartTime = DateTime.Parse(datestart);
            DateTime dateEndTime = DateTime.Parse(dateend);
            int productsale = priceSale;
            String strError = "";
            if (dateStartTime > dateEndTime)
            {
                strError = "Ngày bắt đầu và kết thúc không hợp lệ";
            }
            if (priceSale < product.Price)
            {
                strError = "Giá khuyến mãi phải bé hơn giá góc";
            }
            else
            {
                productSale.ProductId = product.Id;
                productSale.PriceSale = productsale;
                productSale.DateStart = dateStartTime;
                productSale.DateEnd = dateEndTime;
                if(!Product.IsExistPromotionInDatabase(productSale.ProductId))
                {
                    productSaleDAO.Insert(productSale);
                    product.Promotion = true;
                }    
                
                productDAO.Update(product);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProductSale(Product product, string datestart, string dateend, int priceSale, ProductSale productSale)
        {
            if (ModelState.IsValid)
            {
                DateTime dateStartTime = DateTime.Parse(datestart);
                DateTime dateEndTime = DateTime.Parse(dateend);
                int productsale = priceSale;
                String strError = "";
                if (dateStartTime > dateEndTime)
                {
                    strError = "Ngày bắt đầu và kết thúc không hợp lệ";
                }
                if (priceSale < product.Price)
                {
                    strError = "Giá khuyến mãi phải bé hơn giá góc";
                }
                else
                {
                    productSale.ProductId = product.Id;
                    productSale.PriceSale = productsale;
                    productSale.DateStart = dateStartTime;
                    productSale.DateEnd = dateEndTime;
                    product.Promotion = true;
                    productSaleDAO.Update(productSale);
                    productDAO.Update(product);
                }
                return RedirectToAction("Index");
            }
            return View(product);
        }
        public ActionResult ProductStore()
        {
            return View(productDAO.getListStore());
        }
        [HttpPost]
        public ActionResult Import(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                using (var package = new ExcelPackage(file.InputStream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet1"];
                    int rowCount = worksheet.Dimension.Rows;// Lấy sheet đầu tiên
                    for (int row = 2; row <= rowCount; row++) // Bắt đầu từ dòng thứ 2 (dòng header bỏ qua)
                    {
                        Product product = new Product();
                        ProductStore productStore = new ProductStore();
                        String IdValue = worksheet.Cells[row, 1].Value?.ToString();
                        String catIdValue = worksheet.Cells[row, 2].Value?.ToString();
                        String braIdValue = worksheet.Cells[row, 3].Value?.ToString();
                        String statusValue = worksheet.Cells[row, 9].Value.ToString();
                        if (!string.IsNullOrEmpty(IdValue) && int.TryParse(IdValue, out int Id))
                        {
                            product.Id = Id;
                        }
                        if (!string.IsNullOrEmpty(catIdValue) && int.TryParse(catIdValue, out int catId))
                        {
                            product.CatId = catId;
                        }
                        if (!string.IsNullOrEmpty(braIdValue) && int.TryParse(braIdValue, out int braId))
                        {
                            product.BrandId = braId;
                        }
                        product.Name = worksheet.Cells[row, 4].Value.ToString();
                        product.Slug = XString.Str_Slug(product.Name);
                        product.Img = null;
                        product.Promotion = false;
                        product.Created_At = DateTime.Now;
                        product.Created_By = Convert.ToInt32(Session["UserId"].ToString());
                        product.Detail = worksheet.Cells[row, 5].Value.ToString();
                        product.Price = float.Parse(worksheet.Cells[row, 6].Value.ToString());
                        product.Description = worksheet.Cells[row, 7].Value.ToString();
                        if (!string.IsNullOrEmpty(statusValue) && int.TryParse(statusValue, out int staId))
                        {
                            product.Status = staId;
                        }
                        if (!Product.IsExistInDatabase(product.Id))
                        {
                            productDAO.Insert(product);
                            productStore.ProductId = product.Id;
                            productStore.Price = product.Price;
                            product.Created_At = DateTime.Now;
                            product.Created_By = Convert.ToInt32(Session["UserId"].ToString());
                            productStore.Updated_At = DateTime.Now;
                            productStore.Updated_By = Convert.ToInt32(Session["UserId"].ToString());
                            productStore.Qty = float.Parse(worksheet.Cells[row, 8].Value.ToString());
                            productStoreDAO.Insert(productStore);
                        }
                        else
                        {
                            //productStoreDAO.Update(productStore);
                        }
                        // Thêm dữ liệu vào DbContext
                    } // Lưu thay đổi vào cơ sở dữ liệu
                }

                ViewBag.Message = "Nhập hàng thành công!";
            }
            else
            {
                ViewBag.Message = "Vui lòng chọn một file Excel.";
            }
            return RedirectToAction("ProductStore");
        }
    }
}
