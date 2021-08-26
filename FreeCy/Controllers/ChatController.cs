using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Models.Common;
//using FreeCy.Models;
using Models.DAO;
using Models.Framework;

namespace FreeCy.Controllers
{
    public class ChatController : Controller
    {


        // GET: Chat
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PostMessage(Message obj, int Iduser)// thay bằng id room
        {
            var MesDao = new MessRoomsDAO();
            obj.CreatedAt = DateTime.Now;
            var id = ((UserLogin)Session[CommonConstant.USER_SESSION]).ID_User;
            obj.ID_User = (int)id;//// lấy ra session.id_user
            MesDao.AddMess(obj);
            MesDao.Update(obj.ID_MessRoom);
            var Iduserr = MesDao.GetIdByRoom(obj.ID_MessRoom, obj.ID_User);

            // tìm iduser của room khác với id của session

            string ac = "Details/" + Iduser.ToString();

            return RedirectToAction(ac);
        }
       
        // GET: Chat/Details/5
       
        public ActionResult Details(int iduser = 0)
        {
            if (((UserLogin)Session[CommonConstant.USER_SESSION]) != null)
            {
                var MesDao = new MessRoomsDAO();

                var UsDao = new UserDAO();
                var id = (int)(((UserLogin)Session[CommonConstant.USER_SESSION]).ID_User);

                if (UsDao.CheckId(iduser) && iduser != id)// check khác session
                {

                    string a = MesDao.GetIDRoom(id, iduser);
                    var msss = MesDao.ListAllMess(a);
                    List<MessList> l1 = (MesDao.ListMess(id));
                    l1.Sort((x, y) => y.UpdateTime.CompareTo(x.UpdateTime));
                    ViewBag.ListMess = l1;
                    ViewBag.IdRoom = a;
                    ViewBag.idus = iduser;
                    ViewBag.Name = UsDao.GetNameById(iduser);
                    ViewBag.Total = MesDao.TotalMess(a);
                    return View(msss);
                }
            }
            else
            {
                TempData["alertMessage"] = "Bạn cần phải đăng nhập trước!!";
                return RedirectToAction("Login", "User");
            }

            return HttpNotFound();



        }

        public ActionResult Erorr()
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Chat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Chat/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Chat/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Chat/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Chat/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Chat/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}
