using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên loại sản phẩm không được để trống!")]
        [StringLength(255)]
        [Display(Name="Tên danh mục")]
        public String Name { get; set; }
        public String Slug { get; set; }
        public int? ParentId { get; set; }
        public int? Orders { get; set; }
        [Required(ErrorMessage = "Mô tả không được để trống!")]
        public String Description { get; set; }
        public int? Created_By{ get; set; }
        public DateTime? Created_At{ get; set; }
        public int? Updated_By{ get; set; }
        public DateTime? Updated_At { get; set; }
        [Required(ErrorMessage = "Trạng thái không được để trống!")]
        public int Status { get; set; }

    }
}
