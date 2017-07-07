using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using HLX.OA.IDAL;
using System.Reflection;

namespace HLX.OA.DALFactory
{
    /// <summary>
    /// 通过反射的形式创建类的实例
    /// </summary>
  public   partial class AbstractFactory
    {
        
        private static readonly string Assemblypath = ConfigurationManager.AppSettings["AssemblyPath"];
        private static readonly string NameSpace = ConfigurationManager.AppSettings["NameSpace"];
        //public static IUserInfoDal CreatUserInfoDal() {
        //    string fullClassName = NameSpace + ".UserInfoDal";
        //  return   CreateInstance(fullClassName) as IUserInfoDal;

        //}

        private static object CreateInstance(string className)
        {
         var assembly=   Assembly.Load(Assemblypath);
            return assembly.CreateInstance(className);
        }
    }
}
