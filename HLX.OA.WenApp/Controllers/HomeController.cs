using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HLX.OA.WenApp.Controllers
{
    public class HomeController : Controller
    {
        IBLL.IUserInfoService UserInfoService { get; set; }
        public ActionResult Index()
        {
           
            return View();
        }

        public ActionResult HomePage()
        {

            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}