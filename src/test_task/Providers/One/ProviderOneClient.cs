using Microsoft.Extensions.Options;
using System.Net.Http;
using System;
using TestTask.Data;
using TestTask.Data.DTO.Request;
using TestTask.Data.DTO.Response;
using TestTask.Exceptions;
using TestTask.Providers.Two;
using Newtonsoft.Json;

namespace TestTask.Providers.One
{
    public class ProviderOneClient : IProviderOneClient
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly string uri;

        public ProviderOneClient(IHttpClientFactory httpClientFactory, IOptionsMonitor<ProviderOneOptions> options)
        {
            this.httpClientFactory = httpClientFactory;
            this.uri = options.CurrentValue.Url ?? throw new ProviderSettingsException(nameof(ProviderOneClient));
        }

        public async Task<bool> Ping()
        {
            using (var httpClient = httpClientFactory.CreateClient(nameof(ProviderOneClient)))
            {
                var response = await httpClient.GetAsync(uri);
                return response.IsSuccessStatusCode;
            }
        }

        public async Task<ProviderOneSearchResponse> Search(ProviderOneSearchRequest request)
        {
            using (var httpClient = httpClientFactory.CreateClient(nameof(ProviderOneClient)))
            {
                var httpResponseMessage = await httpClient.GetAsync(uri);
                var content = await httpResponseMessage.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ProviderOneSearchResponse>(content);
            }
        }
    }
}
