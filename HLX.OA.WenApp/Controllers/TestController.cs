using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HLX.OA.WenApp.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ShowResult()
        {
            int a = 2;
            int b = 0;
         int c = a / b;
            return Content(c.ToString());
        }
    }
}