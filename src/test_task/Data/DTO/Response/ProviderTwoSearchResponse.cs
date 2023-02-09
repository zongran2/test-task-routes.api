namespace TestTask.Data.DTO.Response;

public class ProviderTwoSearchResponse
{
    // Mandatory
    // Array of routes
    public ProviderTwoRoute[] Routes { get; set; }
}

// HTTP GET http://provider-two/api/v1/ping
//      - HTTP 200 if provider is ready
//      - HTTP 500 if provider is down