using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Spring.Web.Mvc;
using System.Web.Http;
using System.Threading;
using HLX.OA.WenApp.Models;
using System.IO;
using log4net;
namespace HLX.OA.WenApp
{
    public class MvcApplication :SpringMvcApplication
        // System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();//读取了配置文件中关于Log4Net配置信息.
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //开启一个线程，扫描异常信息队列
            string FilePath = Server.MapPath("/Log/");//拿到日志文件夹
            ThreadPool.QueueUserWorkItem((a)=> {
                while (true)
                {
                    //判断队列中是否有数据
                    if (MyExceptionAttribute.ExecptionQueue.Count()>0)
                    {
                      Exception ex=   MyExceptionAttribute.ExecptionQueue.Dequeue();//出队
                        if (ex!=null)
                        {
                            //////将异常信息写到日志文件
                            //string fileName = DateTime.Now.ToString("yyyy-MM-dd");
                            //File.AppendAllText(FilePath + fileName + ".txt", ex.ToString(), System.Text.Encoding.UTF8);//创建文件 ， 追加数据
                            ILog logger = LogManager.GetLogger("errorMsg");//log
                            logger.Error(ex.ToString());//整个对象存入异常日志
                            
                        }
                        else
                        {
                            Thread.Sleep(3000);//休息30
                        }
                    }
                    else
                    {
                        Thread.Sleep(3000);//千万不能少 线程休息 ，后扫描错误
                    }
                }


            },FilePath);

        }
    }
}
