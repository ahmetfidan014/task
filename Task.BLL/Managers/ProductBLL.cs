using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Task.BLL.Abstract;
using Task.BLL.Tools;
using Task.DAL.Models;
using Task.DAL.Repository;

namespace Task.BLL.Managers
{
    public class ProductBLL : GenericRepository<Product>,IProductService
    {
        private PhotosBLL _photos = new PhotosBLL();
        public Response SaveProduct(HttpRequestBase Request, HttpServerUtilityBase Server, List<HttpPostedFileBase> postedFiles)
        {
            try
            {
                string url = Request["Product_url"];
                Product ProductMd = GetFirstOrDefault(e => e.Url.Equals(url));
                if (ProductMd == null || url == "")
                {
                    ProductMd = new Product();
                }
                foreach (var property in ProductMd.GetType().GetProperties())
                {
                    try
                    {
                        var response = Request[property.Name];
                        if (response == null && property.PropertyType != typeof(int))
                        {
                            if (response == null)
                            {
                                continue;
                            }
                        }
                        else
                        {
                            PropertyInfo propertyS = ProductMd.GetType().GetProperty(property.Name);
                            if (property.PropertyType == typeof(decimal))
                            {
                                propertyS.SetValue(ProductMd, Convert.ChangeType(Decimal.Parse(response.Replace('.', ',')), property.PropertyType), null);
                            }
                            else if (property.PropertyType == typeof(int))
                            {
                                if (response != null)
                                {
                                    propertyS.SetValue(ProductMd, Convert.ChangeType(Decimal.Parse(response.Replace('.', ',')), property.PropertyType), null);
                                }

                            }
                            else
                            {
                                propertyS.SetValue(ProductMd, Convert.ChangeType(response, property.PropertyType), null);
                            }
                        }
                    }

                    catch (System.Exception ex)
                    { }
                }
                int productvid = 0;
                return ProductSaveDatabase(ProductMd, Request, Server, postedFiles, out productvid);
            }
            catch (System.Exception ex)
            {
                return responseBLL.create(false, "Ürün eklenirken bir hata oluştu. Lütfen tekrar deneyiniz.", null);
            }
        }

        public Response ProductSaveDatabase(Product ProductMd, HttpRequestBase Request, HttpServerUtilityBase Server, List<HttpPostedFileBase> postedFiles, out int ProductVid)
        {
            try
            {
                if (ProductMd.Id == 0)//Id 0 geliyorsa ilk kez ekleniyor demektir.
                {
                    int sort = 1;
                    int vid = 1;
                    if (Count() != 0)
                    {
                        vid = GetMax("Vid", "Product") + 1;
                        sort = GetMax("Sort", "Product") + 1;
                    }
                    ProductMd.Datetime = DateTime.Now;
                    ProductMd.Flag = status.active;
                    ProductMd.Vid = vid;
                    ProductVid = vid;
                    ProductMd.Sort = sort;
                    ProductMd.Url = stringformatter.OnlyEnglishChar(ProductMd.ProductName + DateTime.Now.ToString("MMssyyhhddmm"));
                    ProductMd.ByWhoId = AuthBLL.GetUser().Id;
                    InsertOrUpdate(ProductMd, ProductMd.Id);
                    Save();


                    string productname = stringformatter.OnlyEnglishChar(ProductMd.ProductName);
                    controlfiles.directory("~/public/upload/img/" + productname);

                    string path = "";
                    string uzantisiz = "";
                    string uzanti = "";
                    var supportedTypes = new[] { ".jpg", ".jpeg", ".png", ".JPG", ".JPEG", ".PNG" };
                    var RequestPhotos = Request.Files["Photo"];
                    if (RequestPhotos != null && RequestPhotos.ContentLength > 0)
                    {
                        foreach (HttpPostedFileBase file in postedFiles)
                        {
                            uzantisiz = stringformatter.OnlyEnglishChar(Path.GetFileNameWithoutExtension(file.FileName));
                            uzanti = Path.GetExtension(file.FileName);
                            string pathUzantsiz = Path.Combine(Server.MapPath("~/public/upload/img/" + productname), uzantisiz);
                            path = Path.Combine(Server.MapPath("~/public/upload/img/" + productname), uzantisiz + uzanti);
                            if (!supportedTypes.Contains(uzanti))
                            {
                                string ErrorMessage = "Yüklemeye Çalıştığunız Dosya Uzantısı Geçerli Değil - Sadece JPEG/JPG/PNG Geçerli";
                                return responseBLL.create(false, ErrorMessage, null);
                            }
                            #region PhotoSaving
                            Photos PhotoMd = new Photos();
                            int sortP = 1;
                            int vidP = 1;
                            if (_photos.Count() != 0)
                            {
                                vidP = GetMax("Vid", "Photos") + 1;
                                sortP = GetMax("Sort", "Photos") + 1;
                            }
                            PhotoMd.Datetime = DateTime.Now;
                            PhotoMd.Flag = status.active;
                            PhotoMd.Vid = vidP;
                            PhotoMd.Sort = sortP;
                            PhotoMd.Url = stringformatter.OnlyEnglishChar(uzantisiz + DateTime.Now.ToString("MMssyyhhddmm"));
                            int j = 1;
                            bool Pbulundu = false;
                            do
                            {
                                if (System.IO.File.Exists(path))
                                {
                                    path = pathUzantsiz + "(" + j + ")" + uzanti;
                                    j++;
                                }
                                else
                                {
                                    file.SaveAs(path);
                                    Pbulundu = true;
                                }

                            } while (!Pbulundu);

                            if (j != 1)
                            {
                                j--;
                                PhotoMd.Path = "/public/upload/img/" + productname + "/" + uzantisiz + "(" + j + ")" + uzanti;
                            }
                            else
                            {
                                PhotoMd.Path = "/public/upload/img/" + productname + "/" + uzantisiz + uzanti;
                            }

                            PhotoMd.ProductId = GetFirstOrDefault(f => f.Vid == vid).Id;
                            _photos.InsertOrUpdate(PhotoMd, PhotoMd.Id);
                            _photos.Save();
                            #endregion
                        }
                    }
                    else
                    {
                        return responseBLL.create(true, "Ürün eklendi. Ancak Fotoğraf yüklenmedi.", null);
                    }

                    return responseBLL.create(true, "Ürün eklendi.", null);
                }
                else //0 gelmiyorsa ilgili model güncelleniyor demektir.
                {
                    string url = Request["Product_url"];
                    ProductVid = ProductMd.Vid;
                    ProductMd.Url = url;
                    Save();
                    string productname = stringformatter.OnlyEnglishChar(ProductMd.ProductName);
                    controlfiles.directory("~/public/upload/img/" + productname);
                    string path = "";
                    string uzantisiz = "";
                    string uzanti = "";
                    var supportedTypes = new[] { ".jpg", ".jpeg", ".png", ".JPG", ".JPEG", ".PNG" };
                    var RequestPhotos = Request.Files["Photo"];
                    if (RequestPhotos != null && RequestPhotos.ContentLength > 0)
                    {
                        foreach (HttpPostedFileBase file in postedFiles)
                        {
                            uzantisiz = stringformatter.OnlyEnglishChar(Path.GetFileNameWithoutExtension(file.FileName));
                            uzanti = Path.GetExtension(file.FileName);
                            string pathUzantsiz = Path.Combine(Server.MapPath("~/public/upload/img/" + productname), uzantisiz);
                            path = Path.Combine(Server.MapPath("~/public/upload/img/" + productname), uzantisiz + uzanti);
                            if (!supportedTypes.Contains(uzanti))
                            {
                                string ErrorMessage = "Yüklemeye Çalıştığunız Dosya Uzantısı Geçerli Değil - Sadece JPEG/JPG/PNG Geçerli";
                                return responseBLL.create(false, ErrorMessage, null);
                            }

                            #region PhotoSaving
                            Photos PhotoMd = new Photos();
                            int sortP = 1;
                            int vidP = 1;
                            if (_photos.Count() != 0)
                            {
                                vidP = GetMax("Vid", "Photos") + 1;
                                sortP = GetMax("Sort", "Photos") + 1;
                            }
                            PhotoMd.Datetime = DateTime.Now;
                            PhotoMd.Flag = status.active;
                            PhotoMd.Vid = vidP;
                            PhotoMd.Sort = sortP;
                            PhotoMd.Url = stringformatter.OnlyEnglishChar(uzantisiz + DateTime.Now.ToString("MMssyyhhddmm"));
                            int j = 1;
                            bool pathexist = false;
                            do
                            {
                                if (System.IO.File.Exists(path))
                                {
                                    path = pathUzantsiz + "(" + j + ")" + uzanti;
                                    j++;
                                }
                                else
                                {
                                    file.SaveAs(path);
                                    pathexist = true;
                                }

                            } while (!pathexist);

                            if (j != 1)
                            {
                                j--;
                                PhotoMd.Path = "/public/upload/img/" + productname + "/" + uzantisiz + "(" + j + ")" + uzanti;
                            }
                            else
                            {
                                PhotoMd.Path = "/public/upload/img/" + productname + "/" + uzantisiz + uzanti;
                            }
                            PhotoMd.ProductId = GetFirstOrDefault(f => f.Url == url).Id;
                            _photos.InsertOrUpdate(PhotoMd, PhotoMd.Id);
                            _photos.Save();
                            #endregion
                        }
                    }

                    else
                    {
                        return responseBLL.create(true, "Ürün güncellendi. Fotoğraf yüklenmedi.", null);
                    }

                    return responseBLL.create(true, "Ürün güncellendi.", null);
                }

            }

            catch (Exception ex)
            {
                ProductVid = 0;
                return responseBLL.create(false, "Ürün eklenirken bir hata oluştu. Lütfen tekrar deneyiniz.", null);
            }
        }
        public Response ProductDelete(int id)
        {
            try
            {
                Product model = GetFirstOrDefault(g => g.Id == id);
                List<Photos> PhotoList = _photos.GetAll(g => g.ProductId == model.Id);
                foreach (var photo in PhotoList)
                {
                    photo.Flag = status.deleted;
                    _photos.InsertOrUpdate(photo, photo.Id);
                    _photos.Save();
                }
                model.Flag = status.deleted;
                InsertOrUpdate(model, model.Id);
                Save();
            }
            catch (Exception ex)
            {
                return responseBLL.create(false, "Öğe Silinirken Hata Oluştu.", null);
            }
            return responseBLL.create(true, "Öğe Silindi.", null);
        }

        public Response PhotoDelete(int id)
        {
            try
            {
                Photos model = _photos.GetFirstOrDefault(g => g.Id == id);
                model.Flag = status.deleted;
                _photos.InsertOrUpdate(model, model.Id);
                _photos.Save();
            }
            catch (Exception ex)
            {
                return responseBLL.create(false, "Öğe Silinirken Hata Oluştu.", null);
            }
            return responseBLL.create(true, "Öğe Silindi.", null);
        }
    }
}
