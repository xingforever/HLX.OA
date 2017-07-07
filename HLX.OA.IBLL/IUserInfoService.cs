using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HLX.OA.Model;
using HLX.OA.Model.Search;

namespace HLX.OA.IBLL
{
  public  partial  interface IUserInfoService:IBaseService<UserInfo>
    {
        bool DeleteEntities(List<int> list);//一次删除多条数据
        IQueryable<UserInfo> LoadSearchEntities(UserInfoSearch userInfoSearch, short delFlag);
        bool SetUserRoleInfo(int userId, List<int> roleIdList);
        bool SetUserActionInfo(int actionId, int userId, bool isPass);
    }
}
