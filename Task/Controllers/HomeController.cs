using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task.BLL.Abstract;
using Task.BLL.Managers;
using Task.BLL.Tools;
using Task.DAL;
using Task.DAL.Models;

namespace Task.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Product> _product;
        public HomeController(IRepository<Product> _Product)
        {
            _product = _Product;
        }
        public ActionResult Products()
        {
            List<object> objects = new List<object>();
            List<Product> productList = _product.GetAll(g=>g.Flag != status.deleted);
            objects.Add(productList);
            return View(objects);
        }
    }
}