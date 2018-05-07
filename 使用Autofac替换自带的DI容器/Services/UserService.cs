using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace 使用Autofac替换自带的DI容器.Services
{
    public class UserService : IUserService
    {

        private readonly Dictionary<int, string> _users;

        public UserService()
        {
            _users = new Dictionary<int, string>();
            _users.Add(1, "shz");
            _users.Add(2, "sye");
            _users.Add(3, "hah");
        }

        public string GetName(int id)
        {
            if(_users.ContainsKey(id))
            {
                return _users[id];
            }
            else
            {
                return "不存在的用户";
            }
        }
    }
}
