using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sport
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImg { get; set; }
        public float ProductPrice { get; set; }
        public int Qty { get; set; }
        public float Amount { get; set; }
        public CartItem() { }
        public CartItem(int productid, string name, string img, float price,int qty) {
            this.ProductId = productid;
            this.ProductName = name;
            this.ProductImg = img;
            this.ProductPrice = price;
            this.Qty = qty;
            this.Amount = price*qty;
        }
    }
}