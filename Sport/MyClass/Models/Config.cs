using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Configs")]
    public class Config
    {
        [Key]
        public int Id { get; set; }
        public String Author { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }
        public String Zalo { get; set; }
        public String Facebook { get; set; }
        public String Address { get; set; }
        public String Youtobe { get; set; }
        public String MetaDesc{ get; set; }
        public String MetaKey { get; set; }
        public int? Created_By { get; set; }
        public DateTime? Created_At { get; set; }
        public int? Updated_By { get; set; }
        public DateTime? Updated_At { get; set; }
        public int Status { get; set; }
    }
}
