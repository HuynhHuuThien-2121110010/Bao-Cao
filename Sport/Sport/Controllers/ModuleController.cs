using MyClass.DAO;
using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace Sport.Controllers
{
    public class ModuleController : Controller
    {
        private MenuDAO menuDAO = new MenuDAO();
        private ProductDAO productDAO = new ProductDAO();
        private SliderDAO sliderDAO = new SliderDAO();
        private CategoryDAO categoryDAO = new CategoryDAO();
        private BrandDAO brandDAO = new BrandDAO();

        // GET: Module
        public ActionResult MainMenu()
        {
            List<Menu> list = menuDAO.getListByParentId("mainmenu",0);
            return View("MainMenu",list);
        }
        public ActionResult MainMenuSub(int id)
        {
            Menu menu = menuDAO.getRow(id);
            List<Menu> list = menuDAO.getListByParentId("mainmenu", id);
            if(list.Count == 0)
            {
                return View("MainMenuSub1", menu); // Không có con
            }    
            else
            {
                ViewBag.Menu = menu;
                return View("MainMenuSub2", list);// Có con
            }    
        }
        //Slidershow
        public ActionResult Slidershow()
        {
            List<Slider> list = sliderDAO.getListByPosition("Slidershow");
            return View("Slidershow",list);
        }
        //ListCategory
        public ActionResult ListCategory()
        {
            List<Category> list = categoryDAO.getListByParentId(0);
            return View("ListCategory",list);
        }
        public ActionResult ListBrand()
        {
            List<Brand> list = brandDAO.getListImg();
            return View("ListBrand", list);
        }
        public ActionResult NewProduct()
        {
            return View("NewProduct");
        }
        public ActionResult FootterMenu()
        {
            List<Menu> list = menuDAO.getListByParentId("footermenu", 0);
            return View("FootterMenu",list);
        }
    }
}