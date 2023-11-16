using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sport
{
    public class XCart
    {
        public List<CartItem> AddCart(int productid,CartItem cartitem)
        {
            List<CartItem> listcart;
            if (System.Web.HttpContext.Current.Session["MyCart"].Equals(""))
            {
                listcart = new List<CartItem>();
                listcart.Add(cartitem);
                System.Web.HttpContext.Current.Session["MyCart"] = listcart;
            }
            else
            {
                listcart = (List<CartItem>)System.Web.HttpContext.Current.Session["MyCart"];// ép keie
                //Kiểm tra sản phẩm có chưa
                if (listcart.Where(m => m.ProductId == productid).Count() != 0)
                {
                    int vt = 0;
                    cartitem.Qty += 1;
                    foreach (var item in listcart)
                    {
                        if (item.ProductId == productid)
                        {
                            listcart[vt].Qty += 1;
                            listcart[vt].Amount = (listcart[vt].Qty* listcart[vt].ProductPrice);
                        }
                        vt++;
                    }
                    System.Web.HttpContext.Current.Session["MyCart"] = listcart;
                }  //chưa có 
                else
                {
                    listcart.Add(cartitem);
                    System.Web.HttpContext.Current.Session["MyCart"] = listcart;
                }
            }
            return listcart;
        }
        public void UpdateCart(string[] listarr)
        {
            List<CartItem> listcart = this.GetCart();
            int vt = 0;
            foreach (CartItem cartItem in listcart)
            {
                if (listarr[vt].Equals("0"))
                {
                    this.DeleteCart(cartItem.ProductId);
                }
                else
                {
                    listcart[vt].Qty = int.Parse(listarr[vt]);
                    listcart[vt].Amount = (listcart[vt].Qty * listcart[vt].ProductPrice);
                    
                }
                vt++;
            }
            System.Web.HttpContext.Current.Session["MyCart"] = listcart;
        }
        public void DeleteCart(int? productid=null)
        {
            if(productid!=null)
            {
                if (!System.Web.HttpContext.Current.Session["MyCart"].Equals(""))
                {
                    List<CartItem> listcart = (List<CartItem>)System.Web.HttpContext.Current.Session["MyCart"];
                    int vt = 0;
                    foreach (var item in listcart)
                    {
                        if (item.ProductId == productid)
                        {
                            listcart.RemoveAt(vt);
                            break;
                        }
                        vt++;
                    }
                    System.Web.HttpContext.Current.Session["MyCart"] = listcart;
                }
            }
            else
            {
                System.Web.HttpContext.Current.Session["MyCart"] = "";
            }    
        }
        public List<CartItem> GetCart()
        {
            if (System.Web.HttpContext.Current.Session["MyCart"].Equals(""))
            {
                return null;
            }    
            else
                return (List<CartItem>)System.Web.HttpContext.Current.Session["MyCart"];
        }
    }
}