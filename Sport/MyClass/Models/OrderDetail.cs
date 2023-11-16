using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClass.Models
{
    [Table("OrderDetails")]
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }
        public int Order_id { get; set; }
        public int Product_id { get; set; }
        public float Price { get; set; }
        public float Discount { get; set; }
        public float Qty { get; set; }
        public float Amount { get; set; }
    }
}
