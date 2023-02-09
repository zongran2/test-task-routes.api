using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Reflection;
using TestTask.Data.DTO.Request;
using TestTask.Data.DTO.Response;
using TestTask.Providers;
using Route = TestTask.Data.Route;

namespace TestTask.Services
{
    public class SearchService : ISearchService
    {
        private readonly IProviderClient<ProviderOneSearchRequest, ProviderOneSearchResponse> providerOneClient;
        private readonly IProviderClient<ProviderTwoSearchRequest, ProviderTwoSearchResponse> providerTwoClient;
        private readonly LocalStorageService storageService;
        private readonly IMapper mapper;

        public SearchService(IProviderClient<ProviderOneSearchRequest, ProviderOneSearchResponse> providerOneClient, 
                             IProviderClient<ProviderTwoSearchRequest, ProviderTwoSearchResponse> providerTwoClient,
                             LocalStorageService storageService, IMapper mapper) 
        {
            this.providerOneClient = providerOneClient;
            this.providerTwoClient = providerTwoClient;
            this.storageService = storageService;
            this.mapper = mapper;
        }
        public async Task<bool> IsAvailableAsync(CancellationToken cancellationToken)
        {
            return await providerOneClient.Ping(cancellationToken) && 
                   await providerTwoClient.Ping(cancellationToken);
        }

        public async Task<SearchResponse> SearchAsync(SearchRequest request, CancellationToken cancellationToken)
        {

            var isCached = request.Filters?.OnlyCached ?? false;
            Func<IEnumerable<Route>, IEnumerable<Route>> applyFilters = (results) =>
            {
                if (request.Filters != null)
                {
                    var filters = request.Filters;

                    if (filters.MinTimeLimit != null)
                        results = results.Where(o => o.TimeLimit >= filters.MinTimeLimit);
                    if (filters.MaxPrice != null)
                        results = results.Where(o => o.Price <= filters.MaxPrice);
                    if (filters.DestinationDateTime != null)
                        results = results.Where(o => o.DestinationDateTime <= filters.DestinationDateTime);
                }

                return results;
            };

            if(isCached)
            {
               return mapper.Map <SearchResponse>(applyFilters(storageService));
            }

            var oneData = providerOneClient.Search(
                new ProviderOneSearchRequest 
                {
                    DateFrom = request.OriginDateTime,
                    From = request.Origin,
                    To = request.Destination,
                    
                }, cancellationToken);

            var twoData = providerTwoClient.Search(
                 new ProviderTwoSearchRequest
                 {
                     Departure = request.Origin,
                     Arrival = request.Destination,
                     DepartureDate = request.OriginDateTime
                 }, cancellationToken);

            Task.WaitAny(new Task[] { oneData, twoData }, cancellationToken );

            var results = (await oneData).Routes.Select(o => new TestTask.Data.Route
            {

                Origin = o.From,
                OriginDateTime = o.DateFrom,
                Destination = o.To,
                DestinationDateTime = o.DateTo,
                Price = o.Price,
                TimeLimit = o.TimeLimit,

            }).Union(
                 (await twoData).Routes.Select(o => new TestTask.Data.Route
                 {

                     Origin = o.Departure.Point,
                     OriginDateTime = o.Departure.Date,
                     Destination = o.Arrival.Point,
                     DestinationDateTime = o.Arrival.Date,
                     Price = o.Price,
                     TimeLimit = o.TimeLimit,
                 })
             );

            


            results = storageService.AddRange(results).ToArray();

            return mapper.Map<SearchResponse>(results);
        }   
    }
}
