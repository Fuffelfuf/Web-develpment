using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyApi.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/variative")]
    [ApiVersion("1.0", Deprecated = true)]
    public class VariativeController : ControllerBase
    {
        private readonly IVariativeService _variativeService;

        public VariativeController(IVariativeService variativeService)
        {
            _variativeService = variativeService;
        }

        [Authorize]
        [HttpGet("number")]
        public IActionResult GetNumber()
        {
            return Ok(_variativeService.GetNumber());
        }
    }
}