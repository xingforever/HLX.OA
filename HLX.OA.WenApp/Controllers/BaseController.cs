using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HLX.OA.Model;

namespace HLX.OA.WenApp.Controllers
{
    public class BaseController : Controller
    {

        public UserInfo LoginUser { get; set; }
        // GET: Base
        /// <summary>
        /// 执行控制器中的方法之前先执行该方法
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (Session["userInfo"] == null) { 
           //  filterContext.HttpContext.Response.Redirect("/Login/Index");
                filterContext.Result = Redirect("/Login/Index");
            }
             else
            {  
             LoginUser =(UserInfo) Session["userInfo"];
             }
        }
    }
}