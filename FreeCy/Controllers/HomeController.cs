using FreeCy.Code;

using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.DAO;
using Models.Framework;
using System.Net;
using Models.Common;
using System.Net.Mail;
using System.Web.SessionState;

namespace FreeCy.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            var productDao = new ProductDAO();
            var user = new User();
            ViewBag.Products = productDao.ListProduct(4, user);
            ViewBag.RelatedProducts = new ProductDAO().ListFeatureProduct(4, user);
            return View();
        }

        public ActionResult ListProduct()
        {
            var listproductDAO = new ProductDAO();
            User user = new User();
            ViewBag.Products = listproductDAO.ListProduct(10, user);
            return View();
        }

        public ActionResult ListProduct2(int page = 1, int pageSize = 7, string sort = "")
        {
            var listproductDAO = new ProductDAO();
            User user = new User();
            Product product = new Product();
            ViewBag.Products = listproductDAO.ListProduct(pageSize, page, user, sort);
            //var model = listproductDAO.ListAllPaging(page, pageSize);
            ViewBag.Cate = listproductDAO.ListProduct(pageSize, page, user, sort);
            int totalRecord = listproductDAO.GetTotalRecords();
            ViewBag.sort = sort;
            ViewBag.Total = totalRecord;
            ViewBag.Page = page;
            ViewBag.Size = pageSize;
            int maxPage = 5;
            int totalPage = 0;
            totalPage = (int)(totalRecord / pageSize) + 1;
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            return View();
        }
        public ActionResult ListProductByCategory(int cateId, int page = 1, int pageSize = 1)
        {
            var category = new CategoryDAO().ViewDetail(cateId);
            User user = new User();
            ViewBag.Category = category;
            int totalRecord = 0;
            var listproductDAO = new ProductDAO();
            var model = listproductDAO.ListByCategoryId(user, cateId, ref totalRecord, page, pageSize);
            ViewBag.Categories = CategoryDAO.Instance.ListAllCategoty();

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            int maxPage = 5;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            return View(model);

        }

        public ActionResult Search(string keyword, int page = 1, int pageSize = 7)
        {
            int totalRecord = 0;
            var model = new ProductDAO().Search(keyword, ref totalRecord, page, pageSize);

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;
            ViewBag.Keyword = keyword;
            int maxPage = 5;
            int totalPage = 0;

            totalPage = (int)(totalRecord / pageSize) + 1;
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            return View(model);
        }
        public ActionResult Detail(int productID)
        {

            if (productID == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var listproductDAO = new ProductDAO();
            var product = new ProductDAO().ViewDetail(productID);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.Category = listproductDAO.ViewDetail(product.ID_Product);

            return View(product);
        }
    }
}