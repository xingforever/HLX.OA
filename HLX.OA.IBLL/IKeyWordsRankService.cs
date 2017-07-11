using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLX.OA.IBLL
{
    public partial interface IKeyWordsRankService : IBaseService <Model. KeyWordsRank>
    {
          bool InsertKeyWordsRank();

        bool DeleteKeyWordsRank();
    }
}
