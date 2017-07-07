using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Messaging;
using HLX.OA.Model;

namespace HLX.OA.DAL
{
    /// <summary>
    /// 负责创建EF数据操作上下文 ，必须保证线程内唯一
    /// </summary>
    public  class DBContextFactory
    {
        public static DbContext CreateContext()
        {
            DbContext dbContext = (DbContext)CallContext.GetData("dbContext");//通过CallContext保证线程内唯一
            if (dbContext==null)
            {
                dbContext = new HLXOAEntities2();
                CallContext.SetData("dbContext", dbContext);
            }
            return dbContext;

        }

    }
}
