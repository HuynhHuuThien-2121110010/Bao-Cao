﻿using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class OrderDetailDAO
    {
        private SportDbContext db = new SportDbContext();
        public List<OrderDetail> getList(int? orderid)
        {
            return db.OrderDetails.Where(m=>m.Order_id==orderid).ToList();
        }
        ////Trả về 1 mẫu tin
        //public Contact getRow(int? id)
        //{
        //    if (id == null)
        //    { return null; }
        //    else
        //    {
        //        return db.Contacts.Find(id);
        //    }
        //}
        public int Insert(OrderDetail row)
        {
            db.OrderDetails.Add(row);
            return db.SaveChanges();
        }
        public int Update(OrderDetail row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        public int Delete(OrderDetail row)
        {
            db.OrderDetails.Remove(row);
            return db.SaveChanges();
        }
    }
}
