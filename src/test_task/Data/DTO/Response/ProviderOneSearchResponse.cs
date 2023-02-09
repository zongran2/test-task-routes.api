using TestTask.Providers.One;

namespace TestTask.Data.DTO.Response;

public class ProviderOneSearchResponse
{
    // Mandatory
    // Array of routes
    public ProviderOneRoute[] Routes { get; set; }
}

// HTTP GET http://provider-one/api/v1/ping
//      - HTTP 200 if provider is ready
//      - HTTP 500 if provider is down