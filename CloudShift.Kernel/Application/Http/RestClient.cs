using CloudShift.Kernel.Application.Dto;
using CloudShift.Kernel.Application.Enum;
using System.Text.Json;

namespace CloudShift.Kernel.Application.Http
{
    public class RestClient : IRestClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _client;

        public RestClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _client = CreateClient();
        }

        public async Task<ResponseWrapper<T>> GetAsync<T>(string url, Dictionary<string, string>? headers = null)
        {
            return await SendAsync<T>(client => client.GetAsync(url), headers);

        }

        private HttpClient CreateClient()
        {
            var client = _httpClientFactory.CreateClient();
            client.Timeout = TimeSpan.FromMinutes(20);
            return client;
        }

        private async Task<ResponseWrapper<T>> SendAsync<T>(Func<HttpClient, Task<HttpResponseMessage>> senderFunc, Dictionary<string, string>? headers = null)
        {
            AppendHeaders(headers);
            var result = await senderFunc(_client);
            _client.DefaultRequestHeaders.Clear();

            if (result.IsSuccessStatusCode)
            {
                try
                {
                    return new ResponseWrapper<T> { IsSucceeded = true, Data = JsonSerializer.Deserialize<T>(await result.Content.ReadAsStringAsync()) };
                }
                catch (Exception)
                {
                    object data = await result.Content.ReadAsStringAsync();
                    return new ResponseWrapper<T> { IsSucceeded = true, Data = (T)data };
                }
            }
            else
            {
                var error = await result.Content.ReadAsStringAsync();
                return new ResponseWrapper<T> { ErrorCode = (ErrorCode)result.StatusCode, Message = error };
            }
        }

        private void AppendHeaders(Dictionary<string, string>? headers)
        {
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    _client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }
        }
    }
}
