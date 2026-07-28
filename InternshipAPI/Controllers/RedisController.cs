using InternshipAPI.Models;
using InternshipAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace InternshipAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RedisController : ControllerBase
{
    private readonly RedisPublisherService _publisher;

    public RedisController(RedisPublisherService publisher)
    {
        _publisher = publisher;
    }

    [HttpPost("publish")]
    public async Task<IActionResult> Publish([FromBody] PublishRequest request)
    {
        await _publisher.PublishAsync(request.Message);

        return Ok(new
        {
            Message = "Published successfully",
            Payload = request.Message
        });
    }
}