using Models.DAO;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreeCy.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index(FormCollection f)
        {
            string keyproduct = f["keyword"].ToString();
            SearchProductDAO dao = new SearchProductDAO();

            List<Product> result = dao.ResultSearchingDAO(keyproduct);
            if (result.Count == 0)
            {
                TempData["alertMessage"] = "No result is found";
                return RedirectToAction("Index", "Home");
            }

            return View(result);
        }
        public ActionResult Index2(FormCollection f)
        {
            string keyproduct = f["keyword"].ToString();
            SearchProductDAO dao = new SearchProductDAO();

            List<User> result = dao.ResultSearchingUserDAO(keyproduct);
            if (result.Count == 0)
            {
                ViewBag.Thongbao = "Không tìm thấy bất kỳ kết quả nào theo tìm kiếm của bạn";
            }

            return View(result);
        }
    }
}