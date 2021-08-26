using Models.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.DAO;

namespace FreeCy.Controllers
{
    public class FileController : Controller
    {
        // GET: File
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase namefile)
        {
            try
            {
                if (namefile.ContentLength > 0)
                {
                    var filename = Path.GetFileName(namefile.FileName);
                    var path = Path.Combine(Server.MapPath("~/assets2/files"), filename);
                    namefile.SaveAs(path);
                    ViewBag.success = "Upload success!";
                }
                else
                {
                    ViewBag.error = "you need select file!";
                }
            }
            catch (Exception e)
            {
                ViewBag.error = "Error! " + e;
            }
            return View();
        }

        public ActionResult UploadFile(int order_id)
        {
            ViewBag.ID = order_id;
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(int order_id, HttpPostedFileBase namefile, UploadFile file)
        {
            try
            {
                if (namefile.ContentLength > 0)
                {
                    //var filename = Path.GetFileName(namefile.FileName);
                    var extension = Path.GetExtension(namefile.FileName);
                    string newFileName = "";
                    newFileName = file.Filename + extension;
                    var path = Path.Combine(Server.MapPath("~/assets2/files"), newFileName);
                    namefile.SaveAs(path);
                    file.ID_Order = order_id;
                    file.Filename = newFileName;
                    file.Path = path;
                    new UploadFileDAO().Create(file);
                    ViewBag.success = "Upload success!";
                }
                else
                {
                    ViewBag.error = "you need select file!";
                }
            }
            catch (Exception e)
            {
                ViewBag.error = "Error! " + e;
                Console.WriteLine("Error " + e.StackTrace);
            }
            return RedirectToAction("ReceivedOrders", "Order");
        }

        public ActionResult DownloadFile(int id)
        {
            var dao = new UploadFileDAO();
            var filePath = dao.GetPath(id);
            var fileName = dao.GetFileName(id);
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }


        string GetVirtualPath(string path)
        {
            string szRoot = Server.MapPath("~");

            if (path.StartsWith(szRoot))
            {
                return ("~/" + path.Replace(szRoot, string.Empty).Replace(@"\", "/"));
            }
            else
            {
                return path;
            }
        }
    }
}