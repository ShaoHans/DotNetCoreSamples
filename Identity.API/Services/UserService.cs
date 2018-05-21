using Identity.API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Identity.API.Services
{
    public class UserService : IUserService
    {
        // Member.API 服务项目的Host地址
        private readonly string _userServiceHostUrl = "http://localhost:5000";

        public async Task<User> GetOrCreateAsync(string phone)
        {
            var paras = new Dictionary<string, string>
            {
                { "phone",phone}
            };
            FormUrlEncodedContent content = new FormUrlEncodedContent(paras);

            HttpClient httpClient = new HttpClient();
            var response = await httpClient.PostAsync(_userServiceHostUrl + "/api/user/get-or-create", content);
            if(response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
            }

            return null;
        }
    }
}
