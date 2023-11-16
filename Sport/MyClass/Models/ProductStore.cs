using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("ProductStores")]
    public class ProductStore
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        public float Price { get; set; }
        public float Qty { get; set; }
        public int? Created_By { get; set; }
        public DateTime? Created_At { get; set; }
        public int? Updated_By { get; set; }
        public DateTime? Updated_At { get; set; }
    }
}
