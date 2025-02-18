using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Bdb.Curso.HttpApi.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly TelemetryClient _telemetryClient;


        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
             TelemetryClient telemetryClient
            )
        {
            _logger = logger;

            _telemetryClient = telemetryClient;

        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {

            _telemetryClient.TrackEvent("MyCustomEvent");

            var i = 0;
            var y = 10;

            try
            {
                var z = y / i;

            }
            catch (Exception eex)
            {
                Log.Error(eex,"Error consumo de datos del clima" );

                _telemetryClient.TrackException(eex);
           
            }




            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
