using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestTask.Data.DTO;
using TestTask.Data.DTO.Request;
using TestTask.Data.DTO.Response;

namespace рroviders.fake.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderTwoController : ControllerBase
    {
        [HttpPost]
        [Route("search")]
        public async Task<ActionResult<ProviderTwoSearchResponse>> Search(ProviderTwoSearchRequest request)
        {

            return WildTicketGenerator.Generate(request);
        }
    }
}
