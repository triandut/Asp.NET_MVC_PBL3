using Models.Common;
using Models.DAO;
using Models.Framework;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreeCy.Controllers
{
    public class CartController : Controller
    {
        private const string CartSession = "CartSession";
        // GET: Cart
        public ActionResult Index()
        {
            var item = (CartItem)Session[CartSession];
            ViewBag.CartItem = item;
            return View();
        }

        public ActionResult AddItem(int product_id)
        {
            var cart = Session[CartSession];
            var product = new ProductDAO().ViewDetail(product_id);
            if(((UserLogin)Session[CommonConstant.USER_SESSION]) != null)
            {
                if (cart == null)
                {
                    var item = new CartItem()
                    {
                        Product = product,
                        Quantity = 1
                    };
                    Session[CartSession] = item;
                }
                else
                {
                    Session[CartSession] = null;
                    var item = new CartItem()
                    {
                        Product = product,
                        Quantity = 1
                    };
                    Session[CartSession] = item;
                }
                return RedirectToAction("Index");
            }
            else
            {
                TempData["alertMessage"] = "Bạn cần phải đăng nhập trước!!";
                return RedirectToAction("Login", "User");
            }  
            
        }

        public ActionResult SetQuantity(string qty)
        {
            var item = (CartItem)Session[CartSession];
            item.Quantity = int.Parse(qty);
            return RedirectToAction("Index");
        }

        public ActionResult Order()
        {
            var item = (CartItem)Session[CartSession];
            int iduser = (int)((UserLogin)Session[CommonConstant.USER_SESSION]).ID_User;
            var user = new UserDAO().ViewDetails(iduser);
            var listproduct = new ProductDAO().ListProduct(-1,user);
            foreach(var i in listproduct)
            {
                if(i.ID_Product == item.Product.ID_Product)
                {
                    TempData["alertMessage"] = "Không thể thêm sản phẩm của chính mình!!";
                    return RedirectToAction("Index", "Home");
                }
            }

            if (item != null)
            {
                Order request = new Order
                {
                    ID_Product = item.Product.ID_Product,
                    ID_User = user.ID_User,
                    Quantity = item.Quantity,
                    Price = ((item.Product.Price * item.Quantity)),
                    isFinish = false,
                    isCancel = false,
                    CreatedAt = DateTime.Now
                };
                if (new OrderDAO().Create(request) > 0)
                {
                    return RedirectToAction("SendOrders", "Order");
                }
                else
                {
                    TempData["alertMessage"] = "Them order khong thanh cong";
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Order");
        }
       
    }
}