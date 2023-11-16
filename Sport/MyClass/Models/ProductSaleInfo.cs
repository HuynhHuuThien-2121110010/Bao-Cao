using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    public class ProductSaleInfo
    {
        public int Id { get; set; }
        public int CatId { get; set; }
        public int? BrandId { get; set; }
        public int? Sort_Order { get; set; }
        public String Name { get; set; }
        public String Slug { get; set; }
        public String Img { get; set; }
        public String Detail { get; set; }
        public float Price { get; set; }
        public String Description { get; set; }
        public bool Promotion { get; set; }
        public int? Created_By { get; set; }
        public DateTime? Created_At { get; set; }
        public int? Updated_By { get; set; }
        public DateTime? Updated_At { get; set; }
        public int Status { get; set; }

        public int ProductId { get; set; }
        public float PriceSale { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
    }
}
