using System;
using System.Collections.Generic;
using System.Text;

namespace 使用DotNetCore自带的DI容器
{
    class UserScopeService : IUserScopeService
    {
        public string GetUserName(int id)
        {
            return "Scope";
        }
    }
}
