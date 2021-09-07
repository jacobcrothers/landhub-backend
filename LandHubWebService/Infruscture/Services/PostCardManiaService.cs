using Domains.ConfigSetting;
using Domains.ConstModels;
using Domains.PostMania;

using Microsoft.Extensions.Options;

using Newtonsoft.Json;

using RestSharp;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace PropertyHatchCoreService.Services
{
    public class PostCardManiaService
    {
        private readonly PostCardManiaSetting _postCardManiaSetting;
        private readonly PostCardManiaUrl _postCardManiaUrl;

        public PostCardManiaService(IOptions<PostCardManiaSetting> postCardSetting, IOptions<PostCardManiaUrl> postCardManiaUrl)
        {
            _postCardManiaSetting = postCardSetting.Value;
            _postCardManiaUrl = postCardManiaUrl.Value;
        }

        public async Task<string> GetAccessToken()
        {
            var client = new RestClient(_postCardManiaUrl.LoginUrl);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var body = JsonConvert.SerializeObject(_postCardManiaSetting);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(response.Content);
                return loginResponse.AccessToken;
            }
            return "";
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            var token = await GetAccessToken();
            var orderResponse = await GetRequestToPostCardManiaApiAsync(_postCardManiaUrl.AllOrdersUrl, null, token);
            var orderList = JsonConvert.DeserializeObject<List<Order>>(orderResponse);
            return orderList;
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            var token = await GetAccessToken();
            var orderResponse = await GetRequestToPostCardManiaApiAsync(string.Format(_postCardManiaUrl.OrderDetailsUrl, orderId), null, token);
            var order = JsonConvert.DeserializeObject<Order>(orderResponse);
            return order;
        }

        public string PostRequestToPostCardManiaApi(string url, object parameter, string token)
        {
            return "";
        }

        private async Task<string> GetRequestToPostCardManiaApiAsync(string url, object parameter, string token)
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"bearer {token}");
            IRestResponse response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Content;
            }
            return "";
        }

    }
}
