using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using TestTask.Data.DTO.Request;
using TestTask.Data.DTO.Response;
using TestTask.Services;

namespace TestTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        private readonly ISearchService searchService;
        private readonly LocalStorageService storageService;
        private readonly IMapper mapper;
        private readonly int timeoutDelay = 3000000;
        public RoutesController(ISearchService searchService, LocalStorageService storageService, IMapper mapper)
        {
            (this.searchService, this.storageService, this.mapper) = (searchService, storageService, mapper);
        }

        [HttpPost]
        [ProducesResponseType(typeof(SearchResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<SearchResponse>> Search(SearchRequest request)
        {
            var token = new CancellationTokenSource(timeoutDelay);
            var response = await searchService.SearchAsync(request, token.Token);
            
            return response;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public string Ping()
        {
            return "pong";
            throw new Exception();
        }
    }
}
