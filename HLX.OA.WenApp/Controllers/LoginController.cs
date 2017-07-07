using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace HLX.OA.WenApp.Controllers
{
    public class LoginController : Controller
    {
        IBLL.IUserInfoService UserinfoService { get; set; }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }


        #region 完成用户登陆
        public ActionResult UserLogin() {
            string validateCode = Session["validateCode"] != null ? Session["validateCode"].ToString() : string.Empty;
            if (string.IsNullOrEmpty(validateCode))
            {
                return Content("no:验证码错误！！");
            }
            Session["validateCode"] = null;
            string txtCode = Request["Vcode"];
            if (!validateCode.Equals(txtCode,StringComparison.InvariantCultureIgnoreCase))
            {
                return Content("no:验证码错误");
            }
            string userName = Request["LoginCode"];
            string userPwd = Request["LoginPwd"];
          var userinfo=  UserinfoService.LoadEntities(u => u.UName == userName && u.UPwd == userPwd).FirstOrDefault();
            if (userinfo!=null)
            {
                Session["userInfo"] = userinfo;
                return Content("ok:登录成功");
            }
            else
            {
                
                return Content("no:用户名或密码错误");
            }

        }





        #endregion

        #region 显示验证码
        public ActionResult ShowValidateCode()
        {
            Common.ValidateCode validateCode = new Common.ValidateCode();
            string code= validateCode.CreateValidateCode(4);//4位数验证码
            Session["validateCode"] = code;//将验证码存在Session中
          byte[]buffer=  validateCode.CreateValidateGraphic(code);//将验证码绘制再画布中
            return File(buffer, "image/jpeg");

        #endregion
        }
    }
}