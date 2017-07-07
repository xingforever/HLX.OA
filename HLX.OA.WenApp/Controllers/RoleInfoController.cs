using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HLX.OA.Model.Enum;
using HLX.OA.Model;

namespace HLX.OA.WenApp.Controllers
{
    public class RoleInfoController : Controller
    {
        IBLL.IRoleInfoService RoleInfoService { get; set; }
        // GET: Role
        public ActionResult Index()
        {
            return View();
        }

        #region 获取角色列表

        public ActionResult GetRoleInfoList() {
            int pageIndex = (Request["page"]) != null ? int.Parse(Request["page"]) : 1;
            int pageSize = (Request["rows"]) != null ? int.Parse(Request["rows"]) : 5;
            int totalCount;
            short delFlag = (short)DeleteEnumType.Normal;
            var roleInfoList = RoleInfoService.LoadPageEntities<int>(pageIndex, pageSize, out totalCount, u => u.DelFlag == delFlag, u => u.ID, true);
            var temp = from r in roleInfoList
                       select new { ID = r.ID, RoleName = r.RoleName, Sort = r.Sort, SubTime = r.SubTime, Remark = r.Remark };
            return Json(new { rows = temp, total = totalCount }, JsonRequestBehavior.AllowGet);


        }

        #endregion



        public ActionResult ShowRoleInfo()
        {
            return View();

        }

        public ActionResult AddRoleInfo(RoleInfo roleInfo)
        {
            roleInfo.ModifiedOn = DateTime.Now;
            roleInfo.SubTime = DateTime.Now;
            roleInfo.DelFlag = 0;
            RoleInfoService.AddEntity(roleInfo);
            return Content("OK");

        }
    }
}