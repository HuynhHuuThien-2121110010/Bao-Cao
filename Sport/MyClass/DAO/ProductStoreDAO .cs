using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class ProductStoreDAO
    {
        private SportDbContext db = new SportDbContext();
        public List<ProductStore> getList(string status = "All")
        {
            List<ProductStore> list = null;
            //switch (status)
            //{
            //    case "Index":
            //        {
            //            list = db.ProductStores.Where(m => m.Status != 0).ToList();
            //            break;
            //        }
            //    case "Trash":
            //        {
            //            list = db.ProductStores.Where(m => m.Status == 0).ToList();
            //            break;
            //        }
            //    default:
            //        {
            //            list = db.ProductStores.ToList();
            //            break;
            //        }
            //}
            return list;
        }
        public ProductStore getRow(int? id)
        {
            if (id == null)
            { return null; }
            else
            {
                return db.ProductStores.Find(id);
            }
        }
        public int Insert(ProductStore row)
        {
            db.ProductStores.Add(row);
            return db.SaveChanges();
        }
        public int Update(ProductStore row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        public int Delete(ProductStore row)
        {
            db.ProductStores.Remove(row);
            return db.SaveChanges();
        }
    }
}
