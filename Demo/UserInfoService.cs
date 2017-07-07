using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    public class UserInfoService : IUserInfoService
    {

        public string UserName { get; set; }
        public Person person { get; set; }

        public string showMsg()
        {
            return "Hello World "+UserName+" 年龄是："+person.Age;  
        }
    }
}
