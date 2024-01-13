using CloudShift.Application.Services;
using CloudShift.Kernel.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace CloudShift.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuidEntitiesController(IGuidEntitiesService guidEntitiesService) : ApiControllerBase
    {
        [HttpGet("single")]
        public async Task<IActionResult> GetGuid()
        {
            var result = await guidEntitiesService.GetGuid();
            return ProcessResponse(result);
        }
    }
}
