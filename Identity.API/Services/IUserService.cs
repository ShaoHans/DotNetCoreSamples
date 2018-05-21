using Identity.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Services
{
    public interface IUserService
    {
        /// <summary>
        /// 根据手机号码获取用户，如果不存在则注册
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <returns></returns>
        Task<User> GetOrCreateAsync(string phone);
    }
}
