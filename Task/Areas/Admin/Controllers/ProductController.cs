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

namespace Task.Areas.Admin.Controllers
{
    [AuthControl]
    public class ProductController : Controller
    {
        private IRepository<Photos> _photos;
        private IProductService _product;
        public ProductController(IRepository<Photos> _Photos, IProductService _Product)
        {
            _photos = _Photos;
            _product = _Product;
        }
        public ActionResult Products()
        {
            List<object> objects = new List<object>();
            List<Product> MdList = _product.GetAll(g=>g.Flag != status.deleted);
            objects.Add(MdList);
            return View(objects);
        }

        public ActionResult ProductAdd(string url = "")
        {
            List<object> objects = new List<object>();
            Product Md = new Product();
            List<Photos> photoList = new List<Photos>();
            if (url != "")
            {
                Md = _product.GetFirstOrDefault(g=>g.Url == url);
                photoList = _photos.GetAll(g => g.ProductId == Md.Id && g.Flag != status.deleted);
            }
            objects.Add(Md);
            objects.Add(photoList);
            return View(objects);
        }

        [HttpPost]
        public JsonResult ProductAdd(List<HttpPostedFileBase> Photo)
        {
            Response saveModel = _product.SaveProduct(Request, Server, Photo);
            return Json(saveModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ProductDelete(int id)
        {
            Response response = _product.ProductDelete(id);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult PhotoDelete(int id)
        {
            Response response = _product.PhotoDelete(id);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}