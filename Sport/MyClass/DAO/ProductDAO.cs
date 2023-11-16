using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PagedList;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class ProductDAO
    {
        private SportDbContext db = new SportDbContext();
        public IPagedList<ProductInfo> getListByListCatId(List<int> listcatid, int pageSize, int pageNumber)
        {
            IPagedList<ProductInfo> list = db.Products
                .Join(
                    db.Categories,
                    p => p.CatId,
                    c => c.Id,
                    (p, c) => new
                    {
                        Id = p.Id,
                        CatId = p.CatId,
                        BrandId = p.BrandId,
                        Sort_Order = p.Sort_Order,
                        Name = p.Name,
                        CategoryName = c.Name,
                        Slug = p.Slug,
                        Promotion = p.Promotion,
                        Img = p.Img,
                        Detail = p.Detail,
                        Description = p.Description,
                        Price = p.Price,
                        Created_By = p.Created_By,
                        Created_At = p.Created_At,
                        Updated_By = p.Updated_By,
                        Updated_At = p.Updated_At,
                        Status = p.Status
                    }
                )
                .Join(db.Brands, p => p.BrandId, b => b.Id, (p, b) => new ProductInfo()
                {
                    Id = p.Id,
                    CatId = p.CatId,
                    BrandName = b.Name,
                    Sort_Order = p.Sort_Order,
                    Name = p.Name,
                    CategoryName = p.Name,
                    Slug = p.Slug,
                    Img = p.Img,
                    Promotion = p.Promotion,
                    Detail = p.Detail,
                    Description = p.Description,
                    Price = p.Price,
                    Created_By = p.Created_By,
                    Created_At = p.Created_At,
                    Updated_By = p.Updated_By,
                    Updated_At = p.Updated_At,
                    Status = p.Status
                })
                .Where(m => m.Status == 1 && listcatid.Contains(m.CatId))
                .OrderByDescending(m => m.Created_At)
                .ToPagedList(pageNumber, pageSize);
            return list;
        }
        public IPagedList<ProductSaleInfo> getListByListProductSale(int pageSize, int pageNumber)
        {
            IPagedList<ProductSaleInfo> list = db.Products
                .Join(db.ProductSales, p => p.Id, s => s.ProductId, (p, s) => new ProductSaleInfo()
                {
                    Id = p.Id,
                    CatId = p.CatId,
                    BrandId = p.BrandId,
                    Sort_Order = p.Sort_Order,
                    Name = p.Name,
                    Slug = p.Slug,
                    Img = p.Img,
                    Detail = p.Detail,
                    Price = p.Price,
                    Promotion = p.Promotion,
                    Description = p.Description,
                    Created_By = p.Created_By,
                    Created_At = p.Created_At,
                    Updated_By = p.Updated_By,
                    Updated_At = p.Updated_At,
                    Status = p.Status,
                    ProductId = s.ProductId,
                    PriceSale = s.PriceSale,
                    DateStart = s.DateStart,
                    DateEnd = s.DateEnd
                })
                .Where(m => m.Status == 1 && m.Promotion==true)
                .OrderByDescending(m => m.Created_At)
                .ToPagedList(pageNumber, pageSize);
            return list;
        }
        public List<ProductInfo> getListByListCatId(List<int> listcatid, int take)
        {
            List<ProductInfo> list = db.Products
                .Join(
                    db.Categories,
                    p => p.CatId,
                    c => c.Id,
                    (p, c) => new
                    {
                        Id = p.Id,
                        CatId = p.CatId,
                        BrandId = p.BrandId,
                        Sort_Order = p.Sort_Order,
                        Name = p.Name,
                        CategoryName = c.Name,
                        Slug = p.Slug,
                        Img = p.Img,
                        Promotion = p.Promotion,
                        Description = p.Description,
                        Detail = p.Detail,
                        Price = p.Price,
                        Created_By = p.Created_By,
                        Created_At = p.Created_At,
                        Updated_By = p.Updated_By,
                        Updated_At = p.Updated_At,
                        Status = p.Status
                    }
                )
                .Join(db.Brands, p => p.BrandId, b => b.Id, (p, b) => new ProductInfo()
                {
                    Id = p.Id,
                    CatId = p.CatId,
                    BrandName = b.Name,
                    Sort_Order = p.Sort_Order,
                    Name = p.Name,
                    CategoryName = p.Name,
                    Slug = p.Slug,
                    Img = p.Img,
                    Detail = p.Detail,
                    Description = p.Description,
                    Promotion = p.Promotion,
                    Price = p.Price,
                    Created_By = p.Created_By,
                    Created_At = p.Created_At,
                    Updated_By = p.Updated_By,
                    Updated_At = p.Updated_At,
                    Status = p.Status
                })
                .Where(m => m.Status == 1 && listcatid.Contains(m.CatId)&&m.Promotion==false)
                .OrderByDescending(m => m.Created_At)
                .Take(take)
                .ToList();
            return list;
        }
        public IPagedList<ProductInfo> getList(int pageSize, int pageNumber)
        {
            IPagedList<ProductInfo> list = db.Products
                .Join(
                    db.Categories,
                    p => p.CatId,
                    c => c.Id,
                    (p, c) => new
                    {
                        Id = p.Id,
                        CatId = p.CatId,
                        BrandId = p.BrandId,
                        Sort_Order = p.Sort_Order,
                        Name = p.Name,
                        CategoryName = c.Name,
                        Slug = p.Slug,
                        Img = p.Img,
                        Detail = p.Detail,
                        Description = p.Description,
                        Promotion = p.Promotion,
                        Price = p.Price,
                        Created_By = p.Created_By,
                        Created_At = p.Created_At,
                        Updated_By = p.Updated_By,
                        Updated_At = p.Updated_At,
                        Status = p.Status
                    }
                )
                .Join(db.Brands, p => p.BrandId, b => b.Id, (p, b) => new ProductInfo()
                {
                    Id = p.Id,
                    CatId = p.CatId,
                    BrandName = b.Name,
                    Sort_Order = p.Sort_Order,
                    Name = p.Name,
                    CategoryName = p.Name,
                    Slug = p.Slug,
                    Img = p.Img,
                    Detail = p.Detail,
                    Description = p.Description,
                    Promotion = p.Promotion,
                    Price = p.Price,
                    Created_By = p.Created_By,
                    Created_At = p.Created_At,
                    Updated_By = p.Updated_By,
                    Updated_At = p.Updated_At,
                    Status = p.Status
                })
                .Where(m => m.Status == 1)
                .OrderByDescending(m => m.Created_At)
                .ToPagedList(pageNumber, pageSize);
            return list;
        }
        public List<ProductStoreInfo> getListStore()
        {
            List<ProductStoreInfo> list = db.ProductStores
                            .Join(
                    db.Products,
                    s => s.ProductId,
                    p => p.Id,
                    (s, p) => new ProductStoreInfo()
                    {
                        Id = p.Id,
                        CatId = p.CatId,
                        BrandId = p.BrandId,
                        Name = p.Name,
                        Sort_Order = p.Sort_Order,
                        Slug = p.Slug,
                        Img = p.Img,
                        Qty = s.Qty,
                        Detail = p.Detail,
                        Description = p.Description,
                        Promotion = p.Promotion,
                        Price = p.Price,
                        Created_By = p.Created_By,
                        Created_At = p.Created_At,
                        Updated_By = p.Updated_By,
                        Updated_At = p.Updated_At,
                        Status = p.Status
                    }
                ).Where(p=>p.Status!=0).ToList();
            return list;
        }
        public List<ProductInfo> getList(String page = "All")
        {
            List<ProductInfo> list = null;
            switch (page)
            {
                case "Index":
                    {
                        list = db.Products
                            .Join(
                    db.Categories,
                    p => p.CatId,
                    c => c.Id,
                    (p, c) => new
                    {
                        Id = p.Id,
                        CatId = p.CatId,
                        BrandId = p.BrandId,
                        Sort_Order = p.Sort_Order,
                        Name = p.Name,
                        CategoryName = c.Name,
                        Slug = p.Slug,
                        Img = p.Img,
                        Detail = p.Detail,
                        Description = p.Description,
                        Promotion = p.Promotion,
                        Price = p.Price,
                        Created_By = p.Created_By,
                        Created_At = p.Created_At,
                        Updated_By = p.Updated_By,
                        Updated_At = p.Updated_At,
                        Status = p.Status
                    }
                )
                            .Join(db.Brands, p => p.BrandId, b => b.Id, (p, b) => new ProductInfo()
                            {
                                Id = p.Id,
                                BrandName = b.Name,
                                BrandId = p.BrandId,
                                Sort_Order = p.Sort_Order,
                                Name = p.Name,
                                CategoryName = p.CategoryName,
                                Slug = p.Slug,
                                Img = p.Img,
                                Detail = p.Detail,
                                Description = p.Description,
                                Promotion = p.Promotion,
                                Price = p.Price,
                                Created_By = p.Created_By,
                                Created_At = p.Created_At,
                                Updated_By = p.Updated_By,
                                Updated_At = p.Updated_At,
                                Status = p.Status
                            })
                            .Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Store":
                    {
                        list = db.Products
                            .Join(
                    db.Categories,
                    p => p.CatId,
                    c => c.Id,
                    (p, c) => new
                    {
                        Id = p.Id,
                        CatId = p.CatId,
                        BrandId = p.BrandId,
                        Sort_Order = p.Sort_Order,
                        Name = p.Name,
                        CategoryName = c.Name,
                        Slug = p.Slug,
                        Img = p.Img,
                        Detail = p.Detail,
                        Description = p.Description,
                        Promotion = p.Promotion,
                        Price = p.Price,
                        Created_By = p.Created_By,
                        Created_At = p.Created_At,
                        Updated_By = p.Updated_By,
                        Updated_At = p.Updated_At,
                        Status = p.Status
                    }
                )
                            .Join(db.Brands, p => p.BrandId, b => b.Id, (p, b) => new ProductInfo()
                            {
                                Id = p.Id,
                                BrandName = b.Name,
                                BrandId = p.BrandId,
                                Sort_Order = p.Sort_Order,
                                Name = p.Name,
                                CategoryName = p.CategoryName,
                                Slug = p.Slug,
                                Img = p.Img,
                                Detail = p.Detail,
                                Description = p.Description,
                                Promotion = p.Promotion,
                                Price = p.Price,
                                Created_By = p.Created_By,
                                Created_At = p.Created_At,
                                Updated_By = p.Updated_By,
                                Updated_At = p.Updated_At,
                                Status = p.Status
                            })
                            .Where(m => m.Status == 1).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Products
                            .Join(
                    db.Categories,
                    p => p.CatId,
                    c => c.Id,
                    (p, c) => new
                    {
                        Id = p.Id,
                        CatId = p.CatId,
                        BrandId = p.BrandId,
                        Sort_Order = p.Sort_Order,
                        Name = p.Name,
                        CategoryName = c.Name,
                        Slug = p.Slug,
                        Img = p.Img,
                        Detail = p.Detail,
                        Description = p.Description,
                        Promotion = p.Promotion,
                        Price = p.Price,
                        Created_By = p.Created_By,
                        Created_At = p.Created_At,
                        Updated_By = p.Updated_By,
                        Updated_At = p.Updated_At,
                        Status = p.Status
                    }
                )
                            .Join(db.Brands, p => p.BrandId, b => b.Id, (p, b) => new 
                {
                    Id = p.Id,
                    BrandName = b.Name,
                    BrandId = p.BrandId,
                    Sort_Order = p.Sort_Order,
                    Name = p.Name,
                    CategoryName = p.CategoryName,
                    Slug = p.Slug,
                    Img = p.Img,
                    Detail = p.Detail,
                    Description = p.Description,
                    Promotion = p.Promotion,
                    Price = p.Price,
                    Created_By = p.Created_By,
                    Created_At = p.Created_At,
                    Updated_By = p.Updated_By,
                    Updated_At = p.Updated_At,
                    Status = p.Status
                })
                            .Join(
                    db.ProductStores,
                    p => p.Id,
                    s => s.ProductId,
                    (p, s) => new ProductInfo()
                    {
                        Id = p.Id,
                        BrandId = p.BrandId,
                        Sort_Order = p.Sort_Order,
                        Name = p.Name,
                        CategoryName = p.CategoryName,
                        Slug = p.Slug,
                        Qty = s.Qty,
                        Img = p.Img,
                        Detail = p.Detail,
                        Description = p.Description,
                        Promotion = p.Promotion,
                        Price = p.Price,
                        Created_By = p.Created_By,
                        Created_At = p.Created_At,
                        Updated_By = p.Updated_By,
                        Updated_At = p.Updated_At,
                        Status = p.Status
                    }
                ).Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Products
                            .Join(
                    db.Categories,
                    p => p.CatId,
                    c => c.Id,
                    (p, c) => new
                    {
                        Id = p.Id,
                        CatId = p.CatId,
                        BrandId = p.BrandId,
                        Sort_Order = p.Sort_Order,
                        Name = p.Name,
                        CategoryName = c.Name,
                        Slug = p.Slug,
                        Img = p.Img,
                        Detail = p.Detail,
                        Description = p.Description,
                        Promotion = p.Promotion,
                        Price = p.Price,
                        Created_By = p.Created_By,
                        Created_At = p.Created_At,
                        Updated_By = p.Updated_By,
                        Updated_At = p.Updated_At,
                        Status = p.Status
                    }
                ).Join(db.Brands, p => p.BrandId, b => b.Id, (p, b) => new ProductInfo()
                {
                    Id = p.Id,
                    BrandName = b.Name,
                    BrandId = p.BrandId,
                    Sort_Order = p.Sort_Order,
                    Name = p.Name,
                    CategoryName = p.CategoryName,
                    Slug = p.Slug,
                    Img = p.Img,
                    Detail = p.Detail,
                    Description = p.Description,
                    Promotion = p.Promotion,
                    Price = p.Price,
                    Created_By = p.Created_By,
                    Created_At = p.Created_At,
                    Updated_By = p.Updated_By,
                    Updated_At = p.Updated_At,
                    Status = p.Status
                })
                            .ToList();
                        break;
                    }

            }
            return list;
        }
        public List<ProductSaleInfo> GetProductSale()
        {
            List<ProductSaleInfo> list = db.ProductSales
                .Join(db.Products, s => s.ProductId, p => p.Id, (s, p) => new ProductSaleInfo()
                {
                    Id = p.Id,
                    CatId = p.CatId,
                    BrandId = p.BrandId,
                    Sort_Order = p.Sort_Order,
                    Name = p.Name,
                    Slug = p.Slug,
                    Img = p.Img,
                    Detail = p.Detail,
                    Price = p.Price,
                    Promotion = p.Promotion,
                    Description = p.Description,
                    Created_By = p.Created_By,
                    Created_At = p.Created_At,
                    Updated_By = p.Updated_By,
                    Updated_At = p.Updated_At,
                    Status = p.Status,
                    ProductId = s.ProductId,
                    PriceSale = s.PriceSale,
                    DateStart = s.DateStart,
                    DateEnd = s.DateEnd
                })
                .Where(m => m.Promotion == true)
                .ToList();
            return list;
        }
        public List<ProductSaleInfo> GetProductSale(int? id)
        {
            List<ProductSaleInfo> list = db.Products
                .Join(db.ProductSales, p => p.Id, s => s.ProductId, (p, s) => new ProductSaleInfo()
                {
                    Id = p.Id,
                    CatId = p.CatId,
                    BrandId = p.BrandId,
                    Sort_Order = p.Sort_Order,
                    Name = p.Name,
                    Slug = p.Slug,
                    Img = p.Img,
                    Detail = p.Detail,
                    Price = p.Price,
                    Promotion = p.Promotion,
                    Description = p.Description,
                    Created_By = p.Created_By,
                    Created_At = p.Created_At,
                    Updated_By = p.Updated_By,
                    Updated_At = p.Updated_At,
                    Status = p.Status,
                    ProductId = s.ProductId,
                    PriceSale = s.PriceSale,
                    DateStart = s.DateStart,
                    DateEnd = s.DateEnd
                }) 
                .Where(m => m.Promotion == true)
                .ToList();
            return list;
        }
        public Product getRow(int? id)
        {
            if (id == null)
            { return null; }
            else
            {
                return db.Products.Find(id);
            }
        }
        public Product getRow(string slug)
        {
            return db.Products.
                Where(m => m.Slug == slug && m.Status == 1).FirstOrDefault();
        }
        public int Insert(Product row)
        {
            db.Products.Add(row);
            return db.SaveChanges();
        }
        public int Update(Product row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        public int Delete(Product row)
        {
            db.Products.Remove(row);
            return db.SaveChanges();
        }
    }
}
