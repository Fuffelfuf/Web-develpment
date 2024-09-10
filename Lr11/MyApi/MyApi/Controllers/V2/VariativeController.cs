using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyApi.Controllers.v2
{
    [ApiController]
    [Route("api/v{version:apiVersion}/variative")]
    [ApiVersion("2.0")]
    public class VariativeController : ControllerBase
    {
        private readonly IVariativeService _variativeService;

        public VariativeController(IVariativeService variativeService)
        {
            _variativeService = variativeService;
        }

        [Authorize]
        [HttpGet("text")]
        public IActionResult GetText()
        {
            return Ok(_variativeService.GetText());
        }
    }
}