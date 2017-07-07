using HLX.OA.WenApp.Models;
using System.Web;
using System.Web.Mvc;

namespace HLX.OA.WenApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //必须添加自己的异常处理
            filters.Add(new MyExceptionAttribute());
        }
    }
}
