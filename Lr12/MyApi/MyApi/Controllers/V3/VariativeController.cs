using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyApi.Controllers.v3
{
    [ApiController]
    [Route("api/v{version:apiVersion}/variative")]
    [ApiVersion("3.0")]
    public class VariativeController : ControllerBase
    {
        private readonly IVariativeService _variativeService;

        public VariativeController(IVariativeService variativeService)
        {
            _variativeService = variativeService;
        }

        [Authorize]
        [HttpGet("excel")]
        public IActionResult GetExcelFile()
        {
            var fileContent = _variativeService.GenerateExcelFile();
            return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "GeneratedFile.xlsx");
        }
    }
}