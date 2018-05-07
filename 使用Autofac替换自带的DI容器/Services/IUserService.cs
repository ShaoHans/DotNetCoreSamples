using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace 使用Autofac替换自带的DI容器.Services
{
    public interface IUserService
    {
        string GetName(int id);
    }
}
