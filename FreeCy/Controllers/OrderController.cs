using Models.Common;
using Models.DAO;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreeCy.Controllers
{
    public class OrderController : Controller
    {
        private const string CartSession = "CartSession";
        // GET: Order/5
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SendOrders()
        {
            if (((UserLogin)Session[CommonConstant.USER_SESSION]) != null)
            {
                int user_id = (int)((UserLogin)Session[CommonConstant.USER_SESSION]).ID_User;
                List<Order> list = new OrderDAO().GetSendOrders(user_id);
                return View(list);
            }
            else
            {
                TempData["alertMessage"] = "Bạn cần phải đăng nhập trước!!";
                return RedirectToAction("Login", "User");
            }

        }

        public ActionResult ReceivedOrders()
        {
            
            if (((UserLogin)Session[CommonConstant.USER_SESSION]) != null)
            {
                int user_id = (int)((UserLogin)Session[CommonConstant.USER_SESSION]).ID_User;
                List<Order> list = new OrderDAO().GetReceivedOrders(user_id);
                return View(list);
            }
            else
            {
                TempData["alertMessage"] = "Bạn cần phải đăng nhập trước!!";
                return RedirectToAction("Login", "User");
            }
        }

        public ActionResult Detail(int order_id)
        {
            var item = new OrderDAO().ViewDetail(order_id);
            if (item != null)
            {
                ViewBag.Files = new UploadFileDAO().GetUploadFiles(order_id);
                return View(item);
            }
            else
            {
                return HttpNotFound();
            }
        }

        public ActionResult ConfirmCompleted(int order_id)
        {
            if (new OrderDAO().SetCompleted(order_id))
            {
                return RedirectToAction("SendOrders", "Order");
            }
            else
            {
                ViewBag.Error = "Xác nhận không thành công";
                return View("Index");
            }
        }
       
    }
}