using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class OrderDAO
    {
        private SportDbContext db = new SportDbContext();
        public List<Order> getList(string status = "All")
        {
            List<Order> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Orders.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Orders.Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Orders.ToList();
                        break;
                    }
            }
            return list;
        }
        public List<OrderInfo> getListJoin(string status = "All")
        {
            List<OrderInfo> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Orders
                            .Join(
                            db.OrderDetails,
                            o => o.Id,
                            od => od.Order_id,
                            (o, od) => new OrderInfo
                            {
                                Id = o.Id,
                                User_Id = o.User_Id,
                                DeliveryName = o.DeliveryName,
                                DeliveryGender = o.DeliveryGender,
                                DeliveryEmail = o.DeliveryEmail,
                                DeliveryPhone = o.DeliveryPhone,
                                DeliveryAddress = o.DeliveryAddress,
                                Note = o.Note,
                                Created_By = o.Created_By,
                                Created_At = o.Created_At,
                                Updated_By = o.Updated_By,
                                Updated_At = o.Updated_At,
                                Status = o.Status,
                                Order_id = od.Order_id,
                                Product_id = od.Product_id,
                                Price = od.Price,
                                Discount = od.Discount,
                                Qty = od.Qty,
                                Amount = od.Amount
                            }
                            )
                            .Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Orders
                            .Join(
                            db.OrderDetails,
                            o => o.Id,
                            od => od.Order_id,
                            (o, od) => new OrderInfo
                            {
                                Id = o.Id,
                                User_Id = o.User_Id,
                                DeliveryName = o.DeliveryName,
                                DeliveryGender = o.DeliveryGender,
                                DeliveryEmail = o.DeliveryEmail,
                                DeliveryPhone = o.DeliveryPhone,
                                DeliveryAddress = o.DeliveryAddress,
                                Note = o.Note,
                                Created_By = o.Created_By,
                                Created_At = o.Created_At,
                                Updated_By = o.Updated_By,
                                Updated_At = o.Updated_At,
                                Status = o.Status,
                                Order_id = od.Order_id,
                                Product_id = od.Product_id,
                                Price = od.Price,
                                Discount = od.Discount,
                                Qty = od.Qty,
                                Amount = od.Amount
                            })
                            .Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Orders
                            .Join(
                            db.OrderDetails,
                            o => o.Id,
                            od => od.Order_id,
                            (o, od) => new OrderInfo
                            {
                                Id = o.Id,
                                User_Id = o.User_Id,
                                DeliveryName = o.DeliveryName,
                                DeliveryGender = o.DeliveryGender,
                                DeliveryEmail = o.DeliveryEmail,
                                DeliveryPhone = o.DeliveryPhone,
                                DeliveryAddress = o.DeliveryAddress,
                                Note = o.Note,
                                Created_By = o.Created_By,
                                Created_At = o.Created_At,
                                Updated_By = o.Updated_By,
                                Updated_At = o.Updated_At,
                                Status = o.Status,
                                Order_id = od.Order_id,
                                Product_id = od.Product_id,
                                Price = od.Price,
                                Discount = od.Discount,
                                Qty = od.Qty,
                                Amount = od.Amount
                            })
                            .ToList();
                        break;
                    }
            }
            return list;
        }
        //Trả về 1 mẫu tin
        public Order getRow(int? id)
        {
            if (id == null)
            { return null; }
            else
            {
                return db.Orders.Find(id);
            }
        }
        public int Insert(Order row)
        {
            db.Orders.Add(row);
            return db.SaveChanges();
        }
        public int Update(Order row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        public int Delete(Order row)
        {
            db.Orders.Remove(row);
            return db.SaveChanges();
        }
    }
}
