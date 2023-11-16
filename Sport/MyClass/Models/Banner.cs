using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Banners")]
    public class Banner
    {
        [Key]
        public int Id { get; set; }
        public String Name { get; set; }
        public String Img { get; set; }
        public String link { get; set; }
        public String Position { get; set; }
        public String Description { get; set; }
        public int? Created_By { get; set; }
        public DateTime? Created_At { get; set; }
        public int? Updated_By { get; set; }
        public DateTime? Updated_At { get; set; }
        public int Status { get; set; }
    }
}
