using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AWSLoggingCodehub.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StarWarsController : ControllerBase
    {
        private static readonly HttpClient httpClient_ = new HttpClient();

        private readonly ILogger<StarWarsController> _logger;

        public StarWarsController(ILogger<StarWarsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("getstarwarspeople")]
        public async Task<IActionResult> Get()
        {
            var response = await httpClient_.GetAsync("https://swapi.dev/api/people/");
            var result = await response.Content.ReadAsStringAsync();
            Debug.WriteLine("getstarwarspeople Debug", result);
            _logger.LogInformation("getstarwarspeople _logger", result);
            _logger.LogInformation("andreas test", string.Empty);
            _logger.LogError(0, new Exception("Andreas exception"), "asdf");
            return Ok(result);
        }
    }
}
