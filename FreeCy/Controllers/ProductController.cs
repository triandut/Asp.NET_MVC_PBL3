using FreeCy.Code;
using FreeCy.Models;
using Models;
using Models.Common;
using Models.DAO;
using Models.Framework;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FreeCy.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index(int? page)
        {
            int iduser = (int)((UserLogin)Session[CommonConstant.USER_SESSION]).ID_User;
            var dao = new ProductDAO();
            User user = new UserDAO().ViewDetails(iduser);
            List<Product> list = dao.ListProduct(-1, user);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pageSize));
        }

        // GET: Product/Details/5
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
            ViewBag.Seller = new UserDAO().ViewDetails(product.ID_User);
            return View(product);
        }

        public SelectList GetCategoryDropDown()
        {
            List<Category> l = new ProductCategoryDAO().ListAll();
            List<CategoryDL> list = new List<CategoryDL>();
            foreach (var i in l)
            {
                list.Add(new CategoryDL
                {
                    ID = i.ID_Category,
                    Name = i.Name
                });
            }
            SelectList catelist = new SelectList(list, "ID", "Name", "ID");
            return catelist;
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            
            if(((UserLogin)Session[CommonConstant.USER_SESSION]) != null)
            {
                ViewBag.Category = GetCategoryDropDown();
                return View();
            }
            else
            {
                TempData["alertMessage"] = "Bạn cần phải đăng nhập trước!!";
                return RedirectToAction("Login", "User");
            }
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(HttpPostedFileBase image, Product product)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                var dao = new ProductDAO();
                //int id_user = SessionHelper.GetID();
                int id_user = (int)((UserLogin)Session[CommonConstant.USER_SESSION]).ID_User;
                if (id_user > 0)
                {

                    int id = dao.Create(product, id_user);
                    if (id > 0)
                    {
                        Product p = dao.ViewDetail(id);
                        var extension = Path.GetExtension(image.FileName);
                        string newFileName = "";
                        newFileName = p.ID_Product.ToString() + extension;
                        var path = Path.Combine(Server.MapPath("~/assets2/images/featured-job/"), newFileName);
                        image.SaveAs(path);
                        p.Image = newFileName;
                        dao.Update(p);
                        return RedirectToAction("Index", "Product");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Them khong thanh cong");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Product");
                }
            }
            return View("Index");
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            Product product = new ProductDAO().ViewDetail(id);
            ViewBag.Category = GetCategoryDropDown();
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (new ProductDAO().Update(product))
                    {
                        return RedirectToAction("Index", "Product");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Edit khong thanh cong");
                    }
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    return RedirectToAction("Home", "Product");
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Product product = new ProductDAO().ViewDetail((int)id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var dao = new ProductDAO();
            Product product = dao.ViewDetail(id);
            string path = product.Image;
            if (dao.Delete(product))
            {
                string fullPath = Request.MapPath("~" + path);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
