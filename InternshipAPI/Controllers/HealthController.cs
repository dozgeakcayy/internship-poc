using InternshipAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace InternshipAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    private readonly HealthCheckService _health;
    private readonly MessageBufferService _buffer;

    public HealthController(
        HealthCheckService health,
        MessageBufferService buffer)
    {
        _health = health;
        _buffer = buffer;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            status = _health.Status,
            redis = _health.RedisConnected,
            rabbitMq = _health.RabbitMqConnected,
            webSocket = _health.WebSocketConnected,
            webhook = _health.WebhookConnected,
            bufferedMessages = _buffer.Count,
            timestamp = DateTime.UtcNow
        });
    }
}