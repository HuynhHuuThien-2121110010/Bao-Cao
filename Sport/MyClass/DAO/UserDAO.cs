using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class UserDAO
    {
        private SportDbContext db = new SportDbContext();
        public List<User> getList(string status = "All")
        {
            List<User> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Users.Where(m => m.Status != 0&&m.Roles!=0).ToList();
                        break;
                    }
                case "Custommer":
                    {
                        list = db.Users.Where(m => m.Status != 0 && m.Roles == 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Users.Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Users.ToList();
                        break;
                    }
            }
            return list;
        }
        public User getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Users.Find(id);
            }
        }
        public User getRow(string username,int roles)
        {
            return db.Users.Where(m =>m.Status == 1 && (m.UserName == username||m.Email==username) && m.Roles== roles).FirstOrDefault();
        }
        public int Insert(User row)
        {
            db.Users.Add(row);
            return db.SaveChanges();
        }
        public int Update(User row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        public int Delete(User row)
        {
            db.Users.Remove(row);
            return db.SaveChanges();
        }
    }
}
