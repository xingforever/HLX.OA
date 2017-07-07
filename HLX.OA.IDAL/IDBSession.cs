using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLX.OA.IDAL
{
    /// <summary>
    /// 业务层调用数据会话层的接口
    /// </summary>
     public  partial  interface IDBSession
    {
         DbContext Db { get;  }
        // IUserInfoDal UserInfoDal { get; set; }
         bool SaveChanges();
    }
}
