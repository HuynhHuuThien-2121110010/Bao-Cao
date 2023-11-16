using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    public class OrderInfo
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public String DeliveryName { get; set; }
        public String DeliveryGender { get; set; }
        public String DeliveryEmail { get; set; }
        public String DeliveryPhone { get; set; }
        public String DeliveryAddress { get; set; }
        public String Note { get; set; }
        public int? Created_By { get; set; }
        public DateTime? Created_At { get; set; }
        public int? Updated_By { get; set; }
        public DateTime? Updated_At { get; set; }
        public int Status { get; set; }


        public int Order_id { get; set; }
        public int Product_id { get; set; }
        public float Price { get; set; }
        public float Discount { get; set; }
        public float Qty { get; set; }
        public float Amount { get; set; }
    }
}
