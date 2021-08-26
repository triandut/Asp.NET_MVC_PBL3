using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreeCy.Areas.Admin.Controllers
{
    public class UserAreasController : Controller
    {
        // GET: Admin/User
        [HasCredential(RoleID = "VIEW_USER")]
        public ActionResult Index()
        {
            return View();
        }
        //[HasCredential(RoleID = "ADD_USER")]
        //[HasCredential(RoleID = "EDIT_USER")]
        //[HasCredential(RoleID = "DELETE_USER")]
    }
}