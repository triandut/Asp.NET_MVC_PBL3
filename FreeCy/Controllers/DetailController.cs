using Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models.Framework;
namespace FreeCy.Controllers
{
    public class DetailController : Controller
    {
        
        // GET: Detail
        [HttpPost]
        public ActionResult ProductDetail()
        {
            return View();
        }
        
    }
}