using TestTask.Data.DTO.Request;
using TestTask.Data.DTO.Response;

namespace TestTask.Providers
{
    public interface IProviderClient<TRequest, TResponse>
    {
        public Task<bool> Ping(CancellationToken cancellationToken);
        public Task<TResponse> Search(TRequest request, CancellationToken cancellationToken);
    }
}
