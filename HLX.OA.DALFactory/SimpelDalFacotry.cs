 

using HLX.OA.IDAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HLX.OA.DALFactory
{
    public partial class AbstractFactory
    {
      
   
		
	    public static IActionInfoDal CreateActionInfoDal()
        {

		 string fullClassName = NameSpace + ".ActionInfoDal";
          return CreateInstance(fullClassName) as IActionInfoDal;

        }
		
	    public static IDepartmentDal CreateDepartmentDal()
        {

		 string fullClassName = NameSpace + ".DepartmentDal";
          return CreateInstance(fullClassName) as IDepartmentDal;

        }
		
	    public static IR_UserInfo_ActionInfoDal CreateR_UserInfo_ActionInfoDal()
        {

		 string fullClassName = NameSpace + ".R_UserInfo_ActionInfoDal";
          return CreateInstance(fullClassName) as IR_UserInfo_ActionInfoDal;

        }
		
	    public static IRoleInfoDal CreateRoleInfoDal()
        {

		 string fullClassName = NameSpace + ".RoleInfoDal";
          return CreateInstance(fullClassName) as IRoleInfoDal;

        }
		
	    public static IUserInfoDal CreateUserInfoDal()
        {

		 string fullClassName = NameSpace + ".UserInfoDal";
          return CreateInstance(fullClassName) as IUserInfoDal;

        }
	}
	
}