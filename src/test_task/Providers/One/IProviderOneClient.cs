using TestTask.Data.DTO.Request;
using TestTask.Data.DTO.Response;

namespace TestTask.Providers.One
{
    public interface IProviderOneClient
    {
        public Task<bool> Ping();
        public Task<ProviderOneSearchResponse> Search(ProviderOneSearchRequest request);
    }
}
