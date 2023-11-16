using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    public class MyViewModel
    {
        public IEnumerable<MyClass.Models.Category> Categories { get; set; }
        public IEnumerable<MyClass.Models.ProductSaleInfo> Products { get; set; }
    }
}
