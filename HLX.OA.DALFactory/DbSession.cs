using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HLX.OA.IDAL;
using HLX.OA.Model;
using HLX.OA.DAL;
using System.Data.Entity;

namespace HLX.OA.DALFactory
{
    /// <summary>
    /// 数据会话层：工厂类 负责完成所有数据操作类的实例创建，然后通过业务层 获取操作类
    /// 提供一个方法：完成所有数据的保存。
    /// </summary>
   public  partial  class DbSession:IDBSession
    {
       // OAEntities1 Db = new OAEntities1();
        public DbContext Db { get { return DBContextFactory.CreateContext(); } }//获取Db
      //  private IUserInfoDal _UserInfoDal;
        //public IUserInfoDal UserInfoDal
        //{
        //    get
        //    {
        //        if (_UserInfoDal==null )
        //        {
        //            _UserInfoDal = AbstractFactory.CreatUserInfoDal();//通过抽象工厂 创建类的实例 封装了
        //        }
        //        return _UserInfoDal;
        //    }
        //    set
        //    {
        //        _UserInfoDal = value;
        //    }
        //}

     



        /// <summary>
        /// 一个业务中经常涉及到对多张表操作，我们希望连接一次表，完成对表数据的操作
        /// </summary>
        /// <returns></returns>
        public bool SaveChanges()
        {
            return Db.SaveChanges() > 0;

        }
    }
}
