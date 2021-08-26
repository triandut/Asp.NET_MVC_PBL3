using Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreeCy.Areas.Admin.Controllers
{
    public class FeedbackController : Controller
    {
        // GET: Admin/Feedback
        public ActionResult Index()
        {
            var feedback = new FeedbackDAO();
            ViewBag.Feedbacks = feedback.ListAllFeedback();
            return View();
        }
    }
}