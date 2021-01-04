using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Task.DAL;
using Task.DAL.Models;

namespace Task.BLL.Abstract
{
    public interface IProductService:IRepository<Product>
    {
        Response SaveProduct(HttpRequestBase Request, HttpServerUtilityBase Server, List<HttpPostedFileBase> postedFiles);
        Response ProductSaveDatabase(Product ProductMd, HttpRequestBase Request, HttpServerUtilityBase Server, List<HttpPostedFileBase> postedFiles, out int ProductVid);
        Response ProductDelete(int id);
        Response PhotoDelete(int id);
    }
}
