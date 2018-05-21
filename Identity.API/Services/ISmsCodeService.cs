using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Services
{
    public interface ISmsCodeService
    {
        /// <summary>
        /// 验证手机短信验证码是否正确
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <param name="smsCode">短信验证码</param>
        /// <returns></returns>
        bool Validate(string phone, string smsCode);
    }
}
