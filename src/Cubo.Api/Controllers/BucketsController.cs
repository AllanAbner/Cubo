using Cubo.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cubo.Api.Controllers
{
    [Route("api/[controller]")]
    public class BucketsController : Controller
    {
        private readonly IBucketService _bucketService;

        public BucketsController(IBucketService bucketService)
        {
            _bucketService = bucketService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var buckets = await _bucketService.GetNameAsync();

            if (buckets == null) return NotFound();

            return Json(buckets);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            var buckets = await _bucketService.GetAsync(name);

            if (buckets == null) return NotFound();

            return Json(buckets);
        }

        [HttpPost("{name}")]
        public async Task<IActionResult> PostAsync(string name)
        {
            await _bucketService.AddAsync(name);

            return Created($"buckets/{name}", null);
        }

        [HttpDelete("{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            await _bucketService.RemoveAsync(name);
            return NoContent();
        }
    }
}