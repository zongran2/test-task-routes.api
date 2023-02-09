using Microsoft.Extensions.Options;
using TestTask.Data;
using TestTask.Data.DTO.Request;
using TestTask.Data.DTO.Response;
using TestTask.Exceptions;

namespace TestTask.Providers.Two
{
    public class ProviderTwoClient : IProviderTwoClient
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly string url;

        public ProviderTwoClient(IHttpClientFactory httpClientFactory, IOptionsMonitor<ProviderTwoOptions> options)
        {
            this.httpClientFactory = httpClientFactory;
            this.url = options.CurrentValue.Url ?? throw new ProviderSettingsException(nameof(ProviderTwoClient));
        }

        public Task<bool> Ping()
        {
            throw new NotImplementedException();
        }

        public Task<ProviderTwoSearchResponse> Search(ProviderTwoSearchRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
