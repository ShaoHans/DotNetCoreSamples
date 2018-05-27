using Identity.API.Models;
using Identity.API.Services;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Identity.API
{
    public class SmsCodeAuthGrantValidator : IExtensionGrantValidator
    {
        private readonly IUserService _userService;
        private readonly ISmsCodeService _smsCodeService;

        public SmsCodeAuthGrantValidator(IUserService userService, ISmsCodeService smsCodeService)
        {
            _userService = userService;
            _smsCodeService = smsCodeService;
        }


        public string GrantType => "sms_code";

        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            string phone = context.Request.Raw["phone"];
            string smsCode = context.Request.Raw["sms_code"];
            GrantValidationResult errorResult = new GrantValidationResult(TokenRequestErrors.InvalidGrant);

            if(string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(smsCode))
            {
                context.Result = errorResult;
                return;
            }

            if(!_smsCodeService.Validate(phone,smsCode))
            {
                context.Result = errorResult;
                return;
            }

            User user = await _userService.GetOrCreateAsync(phone);
            if (user == null)
            {
                context.Result = errorResult;
                return;
            }

            var claims = new Claim[]
            {
                new Claim("name",user.Name),
                new Claim("company",user.Company),
                new Claim("position",user.Position),
                new Claim("phone",user.Phone),

            };
            context.Result = new GrantValidationResult(user.Id.ToString(), GrantType, claims);

        }
    }
}
