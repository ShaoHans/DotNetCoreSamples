using DnsClient;
using Identity.API.Models;
using Microsoft.Extensions.Options;
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
        private string _userServiceHostUrl = "";
        private readonly IDnsQuery _dns;
        private readonly ServiceDiscoveryOptions _options;

        public UserService(IDnsQuery dns, IOptions<ServiceDiscoveryOptions> options)
        {
            _dns = dns;
            _options = options.Value;
            SetUserServiceHostUrl().Wait();
        }

        private async Task SetUserServiceHostUrl()
        {
            var result = await _dns.ResolveServiceAsync("service.consul", _options.ServiceName);
            var firstHost = result.First();
            var address = firstHost.AddressList.Any() ? firstHost.AddressList.FirstOrDefault().ToString() : firstHost.HostName;
            var port = firstHost.Port;
            _userServiceHostUrl = $"http://{address}:{port}";
        }

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
