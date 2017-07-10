using HLX.OA.WenApp.Models;
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

        public ActionResult TestCreate()
        {
            Model.Books model = new Model.Books();
            model.AurhorDescription = "jlkfdjf";
            model.Author = "asfasd";
            model.CategoryId = 1;
            model.Clicks = 1;
            model.ContentDescription = "Ajax高级编程";
            model.EditorComment = "adfsadfsadf";
            model.ISBN = "111111111111111111";
            model.PublishDate = DateTime.Now;
            model.PublisherId = 72;
            model.Title = "Ajax";
            model.TOC = "aaaaaaaaaaaaaaaa";
            model.UnitPrice = 22.3m;
            model.WordsCount = 1234;
            //1.将数据先存储到数据库中。获取刚插入的数据的主键ID值。
            IndexManager.GetInstance().AddQueue(9999, model.Title, model.ContentDescription);//向队列中添加
            return Content("ok");
        }

    }
}