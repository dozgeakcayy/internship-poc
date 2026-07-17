using InternshipAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace InternshipAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WebhookController : ControllerBase
{
    private readonly WebhookAdapter _webhookAdapter;

    public WebhookController(WebhookAdapter webhookAdapter)
    {
        _webhookAdapter = webhookAdapter;
    }

    [HttpPost]
    public async Task<IActionResult> Receive([FromBody] string message)
    {
        await _webhookAdapter.ReceiveAsync(message);

        return Ok("Webhook received successfully.");
    }
}