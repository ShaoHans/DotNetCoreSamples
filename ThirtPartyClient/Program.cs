using System;
using System.Net.Http;
using IdentityModel.Client;

namespace ThirtPartyClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=================客户端模式=====================");
            ClientCredenticalMode();
            Console.WriteLine("=================用户名密码模式=====================");
            OwnerPasswordMode();

            Console.ReadKey();
        }

        /// <summary>
        /// 客户端模式（ClientCredentials）
        /// </summary>
        static void ClientCredenticalMode()
        {
            // 1.先从认证服务器获取AccessToken
            DiscoveryResponse dr = DiscoveryClient.GetAsync("http://localhost:5000").Result;
            if (dr.IsError)
            {
                Console.WriteLine(dr.Error);
                Console.ReadKey();
                return;
            }

            TokenClient tokenClient = new TokenClient(dr.TokenEndpoint, "client_001", "secret_001");
            TokenResponse tokenResponse = tokenClient.RequestClientCredentialsAsync("userdeal-api").Result;
            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                Console.ReadKey();
                return;
            }
            else
            {
                Console.WriteLine(tokenResponse.Json);
            }

            // 2.根据获取到的AccessToken从资源服务器获取接口数据
            HttpClient httpClient = new HttpClient();
            httpClient.SetBearerToken(tokenResponse.AccessToken);
            HttpResponseMessage httpResponse = httpClient.GetAsync("http://localhost:5001/api/values").Result;
            if (httpResponse.IsSuccessStatusCode)
            {
                Console.WriteLine(httpResponse.Content.ReadAsStringAsync().Result);
            }
            else
            {
                Console.WriteLine(httpResponse.StatusCode);
            }
        }

        /// <summary>
        /// 用户名密码模式
        /// </summary>
        static void OwnerPasswordMode()
        {
            // 1.先从认证服务器获取AccessToken
            DiscoveryResponse dr = DiscoveryClient.GetAsync("http://localhost:5000").Result;
            if (dr.IsError)
            {
                Console.WriteLine(dr.Error);
                Console.ReadKey();
                return;
            }

            TokenClient tokenClient = new TokenClient(dr.TokenEndpoint, "client_002", "secret_002");
            TokenResponse tokenResponse = tokenClient.RequestResourceOwnerPasswordAsync("shz", "123456", "userdeal-api").Result;
            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                Console.ReadKey();
                return;
            }
            else
            {
                Console.WriteLine(tokenResponse.Json);
            }

            // 2.根据获取到的AccessToken从资源服务器获取接口数据
            HttpClient httpClient = new HttpClient();
            httpClient.SetBearerToken(tokenResponse.AccessToken);
            HttpResponseMessage httpResponse = httpClient.GetAsync("http://localhost:5001/api/values").Result;
            if (httpResponse.IsSuccessStatusCode)
            {
                Console.WriteLine(httpResponse.Content.ReadAsStringAsync().Result);
            }
            else
            {
                Console.WriteLine(httpResponse.StatusCode);
            }
        }
    }
}
