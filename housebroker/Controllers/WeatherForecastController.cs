using application.DTO;
using domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace housebroker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }


        [HttpGet("CustomerTest")]
        [Authorize(Roles = nameof(UserRole.Customer))]
        public async Task<IActionResult> CustomerTest()
        {
            return Ok("Hello Customer");
        }

        [HttpGet("BrokerTest")]
        [Authorize(Roles = nameof(UserRole.Broker))]
        public async Task<IActionResult> BrokerTest()
        {
            return Ok("Hello Broker");
        }

        [HttpGet("CommonTest")]
        [Authorize]
        public async Task<IActionResult> CommonTest()
        {
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;

            if (roleClaim == nameof(UserRole.Broker))
                return Ok("Hello Broker Again");
            else if (roleClaim == nameof(UserRole.Customer))
                return Ok("Hello Customer Again");
            else
                return Unauthorized("Unknown role");
        }


    }
}
