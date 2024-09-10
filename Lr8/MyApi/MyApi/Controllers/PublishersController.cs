using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MyApi.Models;
using MyApi.Services;
using Microsoft.AspNetCore.Authorization;

namespace MyApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly IPublisherService _publisherService;

        public PublishersController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublisher(int id)
        {
            var result = await _publisherService.GetPublisherAsync(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePublisher([FromBody] Publisher model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _publisherService.CreatePublisherAsync(model);
            return CreatedAtAction(nameof(GetPublisher), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePublisher(int id, [FromBody] Publisher model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _publisherService.UpdatePublisherAsync(id, model);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            var result = await _publisherService.DeletePublisherAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}