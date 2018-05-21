using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Services
{
    public class SmsCodeService : ISmsCodeService
    {
        public bool Validate(string phone, string smsCode)
        {
            return true;
        }
    }
}
