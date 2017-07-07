using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HLX.OA.Model.Enum;
using HLX.OA.Model;

namespace HLX.OA.WenApp.Controllers
{
    public class HomeController : BaseController
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

        #region 过滤登录用户的菜单权限
        /// <summary>
        /// 1: 可以按照用户---角色---权限这条线找出登录用户的权限，放在一个集合中。
        /// 2：可以按照用户---权限这条线找出用户的权限，放在一个集合中。
        /// 3：将这两个集合合并成一个集合。
        /// 4：把禁止的权限从总的集合中清除。
        /// 5：将总的集合中的重复权限清除。
        /// 6：把过滤好的菜单权限生成JSON返回。
        /// </summary>
        /// <returns></returns>
        public ActionResult Getmenus()
        {
            //1: 可以按照用户---角色---权限这条线找出登录用户的权限，放在一个集合中。
            //获取登录用户的信息
            var userInfo = UserInfoService.LoadEntities(u => u.ID == LoginUser.ID).FirstOrDefault();
            //获取登录用户的角色.
            var userRoleInfo = userInfo.RoleInfo;
            //根据登录用户的角色获取对应的菜单权限。
            short actionTypeEnum = (short)ActionTypeEnum.MenumActionType;
            var loginUserMenuActions = (from r in userRoleInfo
                                        from a in r.ActionInfo
                                        where a.ActionTypeEnum == actionTypeEnum
                                        select a).ToList();

            //下面语句是错误的，allUserActions是一个大集合该集合中包含了很多小的集合，所以变量b为集合类型
            //var allUserActions = from r in userRoleInfo
            //                     select r.ActionInfo;
            //var mm = from b in allUserActions
            //         where b.ActionTypeEnum == actionTypeEnum
            //         select b;


            // 2：可以按照用户---权限这条线找出用户的权限，放在一个集合中。
            var userActions = from a in userInfo.R_UserInfo_ActionInfo
                              select a.ActionInfo;

            var userMenuActions = (from a in userActions
                                   where a.ActionTypeEnum == actionTypeEnum
                                   select a).ToList();

            // a.ActionInfo不是一个集合,注意理解权限表与用户权限关系表之间的对应关系
            //var userMenuActionse = from a in userInfo.R_UserInfo_ActionInfo
            //                       from b in a.ActionInfo
            //                       where b.ActionTypeEnum == actionTypeEnum
            //                       select b;



            //3：将这两个集合合并成一个集合。
            loginUserMenuActions.AddRange(userMenuActions);

            //4：把禁止的权限从总的集合中清除。
            var forbidActions = (from a in userInfo.R_UserInfo_ActionInfo
                                 where a.IsPass == false
                                 select a.ActionInfoID).ToList();
            var loginUserAllowActions = loginUserMenuActions.Where(a => !forbidActions.Contains(a.ID));

            //5：将总的集合中的重复权限清除。
            var lastLoginUserActions = loginUserAllowActions.Distinct(new EqualityComparer());
            //6：把过滤好的菜单权限生成JSON返回。
            var temp = from a in lastLoginUserActions
                       select new { icon = a.MenuIcon, title = a.ActionInfoName, url = a.Url };
            return Json(temp, JsonRequestBehavior.AllowGet);
        }
        #endregion
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