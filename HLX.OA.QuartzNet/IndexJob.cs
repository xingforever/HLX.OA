using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLX.OA.QuartzNet
{
    public class IndexJob : IJob
    {
        IBLL.IKeyWordsRankService bll = new BLL.KeyWordsRankService();
        /// <summary>
        /// 将明细表插入汇总表
        /// </summary>
        /// <param name="context"></param>
        public void Execute(JobExecutionContext context)
        {
            bll.DeleteKeyWordsRank();
            bll.InsertKeyWordsRank();
        }
    }
}
