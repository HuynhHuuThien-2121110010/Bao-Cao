using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Exports")]
    public class Export
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên sản phẩm không được để trống!")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Ngày xuất không được để trống!")]
        public DateTime ExportDate { get; set; }
        [Required(ErrorMessage = "Số lượng không được để trống!")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Tên khách hàng không được để trống!")]
        public string Customer {get; set;}
    }
}
