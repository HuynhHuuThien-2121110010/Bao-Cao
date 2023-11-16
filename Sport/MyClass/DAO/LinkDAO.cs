using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class LinkDAO
    {
        private SportDbContext db = new SportDbContext();
        //Trả về danh sách mẫu tin
        //public List<Link> getList(string status = "All")
        //{
        //    List<Link> list = null;
        //    switch (status)
        //    {
        //        case "Index":
        //            {
        //                list = db.Links.Where(m => m.Status != 0).ToList();
        //                break;
        //            }
        //        case "Trash":
        //            {
        //                list = db.Links.Where(m => m.Status == 0).ToList();
        //                break;
        //            }
        //        default:
        //            {
        //                list = db.Links.ToList();
        //                break;
        //            }
        //    }
        //    return list;
        //}
        //Trả về 1 mẫu tin
        public Link getRow(int? id)
        {
            return db.Links.Find(id);
        }
        public Link getRow(string slug)
        {
            return db.Links.Where(m => m.Slug==slug).FirstOrDefault();
        }
        public Link getRow(int tablid, string typelink)
        {
            return db.Links.Where(m => m.TableId == tablid && m.TypeLink == typelink).FirstOrDefault();
        }
        //Thêm mẫu tin
        public int Insert(Link row)
        {
            db.Links.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật mẫu tin
        public int Update(Link row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Xóa mẫu tin
        public int Delete(Link row)
        {
            db.Links.Remove(row);
            return db.SaveChanges();
        }
    }
}
