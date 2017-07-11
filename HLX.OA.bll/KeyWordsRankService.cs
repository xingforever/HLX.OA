
using HLX.OA.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLX.OA.BLL
{
    public partial class KeyWordsRankService : BaseService<Model.KeyWordsRank>,IBLL.IKeyWordsRankService
    {
      

        /// <summary>
        /// 删除汇总中的数据
        /// </summary>
        /// <returns></returns>
        public bool DeleteKeyWordsRank()
        {
            string sql = "truncate table keywordsRank";//快速删除数据

            return this.CurrentDBSession.ExecuteSql(sql) > 0;

        }

        /// <summary>
        /// 将统计的明细表的数据插入。
        /// </summary>
        /// <returns></returns>
        public bool InsertKeyWordsRank()
        {
            string sql = "insert into KeyWordsRank(Id,KeyWords,SearchCount) select newid(),KeyWords,count(*)  from SearchDetails where DateDiff(day,SearchDetails.SearchDateTime,getdate())<=7 group by SearchDetails.KeyWords";
            return this.CurrentDBSession.ExecuteSql(sql) > 0;
        }
    }
}
