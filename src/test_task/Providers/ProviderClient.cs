using Microsoft.Extensions.Options;
using System.Net.Http;
using System;
using TestTask.Data;
using TestTask.Data.DTO.Request;
using TestTask.Data.DTO.Response;
using TestTask.Exceptions;
using TestTask.Providers.Two;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace TestTask.Providers
{
    public class ProviderClient<TRequest, TResponse, TOptions> : IProviderClient<TRequest, TResponse> where TOptions: IProviderOptions
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly string uri;

        public ProviderClient(IHttpClientFactory httpClientFactory, IOptionsMonitor<TOptions> options)
        {
            this.httpClientFactory = httpClientFactory;
            this.uri = options.CurrentValue.Url ?? 
                throw new ProviderSettingsException(
                    $"{nameof(ProviderClient<TRequest, TResponse, TOptions>)}");
        }

        public async Task<bool> Ping(CancellationToken cancellationToken)
        {
            using (var httpClient = httpClientFactory.CreateClient(nameof(TRequest)))
            {
                var response = await httpClient.GetAsync(uri, cancellationToken);
                return response.IsSuccessStatusCode;
            }
        }

        public async Task<TResponse> Search(TRequest request, CancellationToken cancellationToken)
        {
            using (var httpClient = httpClientFactory.CreateClient(nameof(TRequest)))
            {
                var req = new StringContent( JsonConvert.SerializeObject(request), new MediaTypeHeaderValue("application/json"));

                var httpResponseMessage = await httpClient.PostAsync(uri, req, cancellationToken);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var content = await httpResponseMessage.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<TResponse>(content)
                        ?? throw new InvalidDataException(
                            $"returned data for {typeof(TRequest)} is null or not valid for {typeof(TResponse)} ");
                }

                //  throw new ApiException(httpResponseMessage.StatusCode, uri);
                return default;
            }
        }

    }
}
