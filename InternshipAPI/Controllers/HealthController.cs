using InternshipAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace InternshipAPI.Controllers;

[ApiController]
[Route("health")]
public class HealthController : ControllerBase
{
    private readonly HealthCheckService _health;

    public HealthController(HealthCheckService health)
    {
        _health = health;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var redis = await _health.CheckRedisAsync();
        var rabbit = _health.CheckRabbitMq();

        return Ok(new
        {
            Redis = redis ? "Connected" : "Disconnected",
            RabbitMQ = rabbit ? "Connected" : "Disconnected",
            Status = (redis && rabbit) ? "Healthy" : "Unhealthy",
            Time = DateTime.Now
        });
    }
}