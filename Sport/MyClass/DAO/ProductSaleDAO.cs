using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class ProductSaleDAO
    {
        private SportDbContext db = new SportDbContext();
        public List<ProductSale> getList(string status = "All")
        {
            List<ProductSale> list = null;
            //switch (status)
            //{
            //    case "Index":
            //        {
            //            list = db.ProductSales.Where(m => m.Status != 0).ToList();
            //            break;
            //        }
            //    case "Trash":
            //        {
            //            list = db.ProductSales.Where(m => m.Status == 0).ToList();
            //            break;
            //        }
            //    default:
            //        {
            //            list = db.ProductSales.ToList();
            //            break;
            //        }
            //}
            return list;
        }
        public ProductSale getRow(int? id)
        {
            if (id == null)
            { return null; }
            else
            {
                return db.ProductSales.Find(id);
            }
        }
        public int Insert(ProductSale row)
        {
            db.ProductSales.Add(row);
            return db.SaveChanges();
        }
        public int Update(ProductSale row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        public int Delete(ProductSale row)
        {
            db.ProductSales.Remove(row);
            return db.SaveChanges();
        }
    }
}
