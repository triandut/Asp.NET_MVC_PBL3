using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models;
using Models.Common;
using Models.DAO;
using Models.Framework;

namespace FreeCy.Areas.Admin.Controllers
{
    
    public class UsersController : Controller
    {

        private FreeCyDB db = new FreeCyDB();

        [HasCredential(RoleID = "VIEW_USER")]
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            var usDAO = new UserDAO();
            var user = new User();
            ViewBag.Userss = usDAO.ListUsers(pageSize, page);
            //var model = listproductDAO.ListAllPaging(page, pageSize);
            int totalRecord = usDAO.GetTotalRecords();

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


        // GET: Admin/Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Admin/Users/Create
      
        public ActionResult Create()
        {
            ViewBag.ID_User = new SelectList(db.Conversations, "ID_User", "ID_User");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserName,Password,GroupID,Email,Phone,Birthday,isFreeLancer,isEmployer,ResetPasswordCode,isEmailVerify,Address,WorkTime,CreateAt,Image")] User user)
        {

            bool Status = false;
            string Message = "";
            user.Status = true;
            //Model Validation
            if (ModelState.IsValid)
            {
                
                if (UserDAO.Instance.CheckUserName(user.UserName))
                {
                    ModelState.AddModelError("", "*Tên đăng nhập tồn tại!");
                }
               
                else if (UserDAO.Instance.CheckUserEmail(user.Email))
                {
                    ModelState.AddModelError("", "*Email đã tồn tại !");
                }
                else
                {

                    user.Password = Encryptor.MD5Hash(user.Password);

                    user.isEmailVerify = false;

                    user.Status = true;
                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
            }
            ViewBag.Message = Message;
            ViewBag.Status = Status;
            return View();

        }

        // GET: Admin/Users/Edit/5
        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        
        [HttpPost]
    
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_User,UserName,Password,GroupID,Status,Email,Phone,Birthday,isFreeLancer,isEmployer,ActivationCode,ConfirmPassword,Name,Skill,Educate,Introduce,ExpertTime,SalaryUser,Address,WorkTime,CreateAt,Exper,Image")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Admin/Users/Delete/5
       
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {         
            var dao = new UserDAO();
            User user = dao.ViewDetails(id);
            dao.DeleteUser(user);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
