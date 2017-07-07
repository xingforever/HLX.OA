using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HLX.OA.Model;
using Spring.Context;
using Spring.Context.Support;
using HLX.OA.IBLL;

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
            bool isSuccess = false;
            if (Session["userInfo"] == null) { 
           //  filterContext.HttpContext.Response.Redirect("/Login/Index");
                filterContext.Result = Redirect("/Login/Index");
            }
             else
            {  
             LoginUser =(UserInfo) Session["userInfo"];
                if (LoginUser.UName=="HLX")//后门
                {
                    return;
                }
                isSuccess = true;
                //当前
                string url = Request.Url.AbsolutePath;//获取当前路径的绝对路径
                //请求 方式
                string httpMethod = Request.HttpMethod;
                //根据获取url地址 与请求方式查询权限表
                IApplicationContext ctx = ContextRegistry.GetContext();//拿到容器
                IActionInfoService lister = (IActionInfoService)ctx.GetObject("ActionInfoService");
               var actioninfo= lister.LoadEntities(a =>a.Url== url && a.HttpMethod==httpMethod).FirstOrDefault();
                //判断 用户是否具有所访问 的地址对应的限制
                if (actioninfo==null )
                {
                    return;
                }
                IUserInfoService userinfoService = (IUserInfoService)ctx.GetObject("UserInfoService");
                var longUserInfo = userinfoService.LoadEntities(u => u.ID == LoginUser.ID).FirstOrDefault();
                //按照用户权限 进行过滤
                var isExt = (from a in LoginUser.R_UserInfo_ActionInfo
                             where a.ActionInfoID == actioninfo.ID
                             select a).FirstOrDefault();
                if (isExt!=null )
                {
                    if (isExt.IsPass)
                    {
                        return;
                    }
                    else
                    {
                        filterContext.Result = Redirect("/Error.html");
                        return;
                    }
                }


                //按照角色权限进行过滤
                var loginUserRole = longUserInfo.RoleInfo;
                var count =( from r in loginUserRole
                            from a in r.ActionInfo
                            where a.ID == actioninfo.ID
                            select a).Count();
                if (count < 1)
                {
                    filterContext.Result = Redirect("/Error/html");
                    return; 
                }


             }
        }
    }
}