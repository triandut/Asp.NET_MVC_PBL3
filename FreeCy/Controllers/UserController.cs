using Models.Common;
using Models.DAO;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Facebook;
using System.Configuration;
using System.Data.Entity;

namespace FreeCy.Views.Home
{
    public class UserController : Controller
    {
        
        // GET: User
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Exclude = "EmailConfirm, ActivationCode")] Register model)
        {
            bool Status = false;
            string Message = "";   
            //Model Validation
            if (ModelState.IsValid)
            {
                //username already exists
                if (UserDAO.Instance.CheckUserName(model.UserName))
                {
                    ModelState.AddModelError("", "*Tên đăng nhập tồn tại!");
                }
                //email already exists
                else if (UserDAO.Instance.CheckUserEmail(model.Email))
                {
                    ModelState.AddModelError("", "*Email đã tồn tại !");
                }
                else
                {
                    var user = new User();

                    model.ActivationCode = Guid.NewGuid();
                    user.ActivationCode = model.ActivationCode;
                    user.UserName = model.UserName;
                    user.Password = Encryptor.MD5Hash(model.Password);
                    user.Email = model.Email;
                    user.isEmailVerify = false;
                    user.Status = true;

                    var result = UserDAO.Instance.Insert(user);
                    if (result > 0)
                    {
                        SendVerificationLinkEmail(user.Email, user.ActivationCode.ToString());
                        Message = "Đăng ký thành công. Tài khoản đã được kích hoạt" +
                            " Link kích hoạt đã được gửi qua email của bạn: " + user.Email;
                        Status = true;
                        model = new Register();
                    }
                    else
                    {
                        Message = "Yêu cầu không hợp lệ!";
                    }
                }
            }
            ViewBag.Message = Message;
            ViewBag.Status = Status;
            return View();
        }

        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool Status = false;
            UserDAO.Instance.ValidateOnSaveEnabled();
            var user = UserDAO.Instance.CheckUserByActivationCode(new Guid(id));

            if (user != null)
            {
                user.isEmailVerify = true;
                UserDAO.Instance.SaveChanges();
                Status = true;
            }
            else
            {
                ViewBag.Message = "Yêu cầu không hợp lệ!";
            }

            ViewBag.Status = Status;
            return View();
        }

        [NonAction]
        public void SendVerificationLinkEmail(string Email, string activationCode, string emailFor = "VerifyAccount")
        {
            var verifyUrl = "/User/" + emailFor + "/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("trian7391@gmail.com");
            var toEmail = new MailAddress(Email);
            var fromEmailPassword = "annt@832001"; 
            string subject = "";
            string body = "";
            if (emailFor == "VerifyAccount")
            {
                subject = "You FreeCy account is successfully created!";

                body = "<br/><br/> Chúng tôi vui mừng thông báo với bạn rằng tài khoản FreeCy của bạn " +
                   "đã được kích hoạt thành công ^^. Hãy nhấn vào link bên dưới để xác nhận tài khoản." +
                   "<br/><br/><a href =" + link + ">" + link + "</a>";
            }
            else if (emailFor == "ResetPassword")
            {
                subject = "Reset Password";
                body = "Hi, <br/><br/>Chúng tôi đã nhận được yêu cầu đặt lại mật khẩu tài khoản của bạn. Vui lòng nhấp vào liên kết dưới đây để thiết lập lại" +
                    "<br/><br/><a href =" + link + ">Đặt lại liên kết mật khẩu</a>";
            }

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword),
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            })
                smtp.Send(message);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.Password));
                if (result == 1)
                {
                    var user = dao.GetById(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.ID_User = user.ID_User;
                    TempData["UserName"] = user.UserName;
                    dao.SaveUserId(userSession.ID_User);                       
                    Session.Add(CommonConstant.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản bị khóa");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng");
                }
                else
                {
                    ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng");
                }
            }
            return View(model);
        }
        
        
        public ActionResult Logout()
        {
            Session[CommonConstant.USER_SESSION] = null;
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string Email)
        {
            //xác nhận email
            //generate reset password link
            //gửi email
            string message = "";
            bool status = false;
            var user = UserDAO.Instance.GetUserByEmail(Email);
            if (user != null)
            {
                //send email for reset pass
                string resetCode = Guid.NewGuid().ToString();
                SendVerificationLinkEmail(user.Email, resetCode, "ResetPassword");
                user.ResetPasswordCode = resetCode;
                UserDAO.Instance.ValidateOnSaveEnabled();
                UserDAO.Instance.SaveChanges();
                status = true;
                message = "Kiểm tra email của bạn để đặt lại mật khẩu";
            }
            else
            {
                message = "Tài khoản không tìm thấy!";
            }
            ViewBag.Status = status;
            ViewBag.Message = message;
            return View();
        }
        [HttpGet]
        public ActionResult ResetPassword(string id)
        {
            //xác nhận link reset
            //tìm account
            
            if (UserDAO.Instance.GetUserByResetCode(id) != null)
            {               
                ResetPasswordModel model = new ResetPasswordModel();
                model.ResetCode = id;
                return View(model);
            }
            else
            {
                return HttpNotFound();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            bool status = false;
            string message = "";
            if (ModelState.IsValid)
            { 
                var user = UserDAO.Instance.GetUserByResetCode(model.ResetCode);
                if (user != null)
                {
                    user.Password = Encryptor.MD5Hash(model.NewPassword);
                    user.ResetPasswordCode = "";
                    UserDAO.Instance.ValidateOnSaveEnabled();
                    UserDAO.Instance.SaveChanges();
                    message = "Mật khẩu mới cập nhật thành công!";
                    status = true;
                }
            }
            else
            {
                message = "Đã xảy ra sự cố!";
            }
            ViewBag.Message = message;
            ViewBag.Status = status;
            return View(model);
        }

        
        public ActionResult ListUser(int page = 1, int pageSize = 9)
        {
            var UsDao = new UserDAO();
            User user = new User();
            ViewBag.Users = UsDao.ListUsers(pageSize, page);
            //var model = listproductDAO.ListAllPaging(page, pageSize);
            int totalRecord = UsDao.GetTotalRecords();
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
        public ActionResult ViewProfile()
        {
            
            var user = new UserDAO();
            var ID_User = (int)(((UserLogin)Session[CommonConstant.USER_SESSION]).ID_User);
            ViewBag.User = user.ViewProfile(ID_User);
            return View();
            
        }
        public ActionResult ViewProfileCandidate(int iduser)
        {

            if (iduser == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dao = new UserDAO();
            var product = new UserDAO().ViewDetails(iduser);

            if (product == null)
            {
                return HttpNotFound();
            }
            

            return View(product);

        }



        private FreeCyDB db = new FreeCyDB();
        // GET: Admin/Users/Edit/5
        public ActionResult Edit()
        {
            string UserFullName = UserDAO.Instance.GetUserFullName(UserDAO.Instance.GetUserId());
                ViewBag.UserFullName = UserFullName;
                var user = UserDAO.Instance.ViewDetails(UserDAO.Instance.GetUserId());

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_User,UserName,Password,Email,Phone,Birthday,isFreeLancer,isEmployer,Skill,Educate,ExpertTime,SalaryUser,Address,WorkTime,CreateAt,Exper,Image")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

    }
}