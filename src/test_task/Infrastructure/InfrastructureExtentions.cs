using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using TestTask.Data;
using TestTask.Data.DTO.Request;
using TestTask.Data.DTO.Response;
using TestTask.Providers;
using TestTask.Providers.One;
using TestTask.Providers.Two;
using TestTask.Services;

namespace TestTask.Infrastructure
{
    public static class InfrastructureExtentions
    {
        public static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder app)
        {
            app.Services.AddHttpClient();
            app.Services.AddMemoryCache();
            app.Services.AddAutoMapper(typeof(Root).Assembly);

            app.Services.AddSingleton<LocalStorageService>();

            app.Services.Configure<ProviderOneOptions>(
            app.Configuration.GetSection("ProviderOne"));

            app.Services.Configure<ProviderTwoOptions>(
            app.Configuration.GetSection("ProviderTwo"));

            app.Services.AddTransient<IProviderClient<ProviderOneSearchRequest, ProviderOneSearchResponse>, ProviderClient<ProviderOneSearchRequest, ProviderOneSearchResponse, ProviderOneOptions>>();
            app.Services.AddTransient<IProviderClient<ProviderTwoSearchRequest, ProviderTwoSearchResponse>, ProviderClient<ProviderTwoSearchRequest, ProviderTwoSearchResponse, ProviderTwoOptions>>();

            app.Services.AddTransient<ISearchService, SearchService>();

            return app;
        }

        public static WebApplication UseApplicationServices(this WebApplication app)
        {
            return app;
        }
    }
}
