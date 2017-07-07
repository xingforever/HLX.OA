using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using HLX.OA.Model.Enum;
using HLX.OA.Model;
using  HLX.OA.Model.Search;

namespace HLX.OA.WenApp.Controllers
{
    public class UserInfoController : BaseController
    {
        IBLL.IUserInfoService UserInfoService { get; set; }
        IBLL.IRoleInfoService RoleInfoService { get; set; }
        IBLL.IActionInfoService ActionInfoService { get; set; }
        IBLL.IR_UserInfo_ActionInfoService R_UserInfo_ActionInfoService { get; set; }

      
        public ActionResult Index()
        {

            return View();
        }
        #region 获取用户数据
        public ActionResult GetUserInfoList()
        {
            //获取页码
            int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 5;
            //接受搜索条件
            string userName = Request["name"];
            string userRemark = Request["remark"];
             int totalCount=0;
            //构建搜索条件
            UserInfoSearch userInfoSearch = new UserInfoSearch()
            {
                UserName = userName,
                UserRemark = userRemark,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount
            };


          short delFlag= (short) DeleteEnumType.Normal;
            var userinfoList = UserInfoService.LoadSearchEntities(userInfoSearch, delFlag);

        //    var UserInfoList = UserInfoService.LoadPageEntities<int>(pageIndex, pageSize, out totalCount, c => c.DelFlag == delFlag, c => c.ID, true);

            var temp = from u in userinfoList
                       select new
                       {
                           ID=u.ID ,
                           UName = u.UName,
                           UPwd = u.UPwd,
                           Remark = u.Remark,
                           SubTime = u.SubTime

                       };
            //JSon 固定要求
            return Json(new { rows=temp,total= userInfoSearch.TotalCount }, JsonRequestBehavior.AllowGet);//属性名师固定的 
            
            

        }
        #endregion

        #region 删除用户数据
        public ActionResult DeleteUserInfo() {

            string strId = Request["strID"];
            string[] strIds = strId.Split(',');
            List<int> list = new List<int>();
            foreach (var item in strIds)
            {
                list.Add(Convert.ToInt32(item));
            }
            //删除操作
            if (UserInfoService.DeleteEntities(list))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }


        }
        #endregion

        #region 添加用户数据

        public  ActionResult AddUserInfo(UserInfo userInfo)
        {
            userInfo.DelFlag = 0;
            userInfo.ModiedOn = DateTime.Now;
            userInfo.SubTime = DateTime.Now;
            
            UserInfoService.AddEntity(userInfo);
            return Content("ok");
        }
        #endregion

        #region 展示要修改数据

        public ActionResult ShowEditInfo()
        {
            int id = int.Parse(Request["id"]);
          var userinfo=  UserInfoService.LoadEntities(u => u.ID == id).FirstOrDefault();
            return Json(userinfo, JsonRequestBehavior.AllowGet);
            
        }
        #endregion

        public  ActionResult EditUserInfo( UserInfo UserInfo)
        {
            UserInfo.ModiedOn = DateTime.Now;
            if (UserInfoService.EditEntity(UserInfo))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
           

        }
        #region 展示用户已经有的角色
        public ActionResult ShowUserRoleInfo()
        {
            int id = int.Parse(Request["id"]);
            var userInfo = UserInfoService.LoadEntities(u => u.ID == id).FirstOrDefault();
            ViewBag.UserInfo = userInfo;
            //查询所有的角色.
            short delFlag = (short)DeleteEnumType.Normal;
            var allRoleList = RoleInfoService.LoadEntities(r => r.DelFlag == delFlag).ToList();
            //查询一下要分配角色的用户以前具有了哪些角色编号。
            var allUserRoleIdList = (from r in userInfo.RoleInfo
                                     select r.ID).ToList();
            ViewBag.AllRoleList = allRoleList;
            ViewBag.AllUserRoleIdList = allUserRoleIdList;
            return View();
        }

        #endregion

        #region 完成用户角色的分配
        public ActionResult SetUserRoleInfo()
        {
            int userId = int.Parse(Request["userId"]);
            string[] allKeys = Request.Form.AllKeys;//获取所有表单元素name属性值。
            List<int> roleIdList = new List<int>();
            foreach (string key in allKeys)
            {
                if (key.StartsWith("cba_"))
                {
                    string k = key.Replace("cba_", "");
                    roleIdList.Add(Convert.ToInt32(k));
                }
            }
            if (UserInfoService.SetUserRoleInfo(userId, roleIdList))//设置用户的角色
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }

        #endregion

        #region 展示用户权限
        public ActionResult ShowUserAction()
        {
            int userId = int.Parse(Request["UserId"]);
            var userInfo = UserInfoService.LoadEntities(u => u.ID == userId).FirstOrDefault(); ;
            ViewBag.userInfo = userInfo;
            //获取所有权限
            short delFlag = (short)DeleteEnumType.Normal;
            var allActionList = ActionInfoService.LoadEntities(a => a.DelFlag == delFlag).ToList();
            //获取用户已经有的权限
            var allActionIdList = (from a in userInfo.R_UserInfo_ActionInfo
                                   select a).ToList();
            ViewBag.AllActionList = allActionList;
            ViewBag.AllactionIdList = allActionIdList;
            return View();

        }
        #endregion



        #region 完成用户权限分配
        public ActionResult SetUserAction()
        {
            int actionId = int.Parse(Request["actionId"]);
            int userInfoId = int.Parse(Request["userId"]);
            bool isPass = Request["isPass"] == "true" ? true : false;
            if (UserInfoService.SetUserActionInfo(actionId, userInfoId, isPass))
            {
                return Content("ok");

            }
            else
            {
                return Content("no");
            }

        }
        #endregion

        #region 清除用户权限
        public ActionResult ClearUserAction()
        {

            int actionId = int.Parse(Request["actionId"]);
            int userInfoId = int.Parse(Request["userId"]);
            var r_UserInfo_actionInfo = R_UserInfo_ActionInfoService.LoadEntities(r => r.ActionInfoID == actionId && r.UserInfoID == userInfoId).FirstOrDefault();
            //删除
            if (r_UserInfo_actionInfo != null)
            {
                if (R_UserInfo_ActionInfoService.DeleteEntity(r_UserInfo_actionInfo))
                {
                    return Content("ok:删除成功!!");
                }
                else
                {
                    return Content("no:删除失败!!");
                }
            }
            else
            {
                return Content("no:数据不存在！！");
            }


        } 
        #endregion



    }
}