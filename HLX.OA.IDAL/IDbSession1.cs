﻿ 

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLX.OA.IDAL
{
	public partial interface IDBSession
    {

	
		IActionInfoDal ActionInfoDal{get;set;}
	
		IBooksDal BooksDal{get;set;}
	
		IDepartmentDal DepartmentDal{get;set;}
	
		IKeyWordsRankDal KeyWordsRankDal{get;set;}
	
		IR_UserInfo_ActionInfoDal R_UserInfo_ActionInfoDal{get;set;}
	
		IRoleInfoDal RoleInfoDal{get;set;}
	
		ISearchDetialsDal SearchDetialsDal{get;set;}
	
		IUserInfoDal UserInfoDal{get;set;}
	}	
}