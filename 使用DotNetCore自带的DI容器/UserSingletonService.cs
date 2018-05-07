using System;
using System.Collections.Generic;
using System.Text;

namespace 使用DotNetCore自带的DI容器
{
    public class UserSingletonService : IUserSingletonService
    {
        public string GetUserName(int id)
        {
            return "Singleton";
        }
    }
}
