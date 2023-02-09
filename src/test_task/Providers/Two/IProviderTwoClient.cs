using TestTask.Data.DTO.Request;
using TestTask.Data.DTO.Response;

namespace TestTask.Providers.Two
{
    public interface IProviderTwoClient
    {
        public Task<bool> Ping();
        public Task<ProviderTwoSearchResponse> Search(ProviderTwoSearchRequest request);
    }
}
