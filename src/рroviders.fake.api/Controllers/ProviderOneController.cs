using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TestTask.Data.DTO;
using TestTask.Data.DTO.Request;
using TestTask.Data.DTO.Response;

namespace рroviders.fake.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderOneController : ControllerBase
    {
        [HttpPost]
        [Route("search")]
        public async Task<ActionResult<ProviderOneSearchResponse>> Search(ProviderOneSearchRequest request)
        {
            
            return WildTicketGenerator.Generate(request);
        }
    }


}
