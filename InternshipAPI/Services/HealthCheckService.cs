namespace InternshipAPI.Services;

public class HealthCheckService
{
    public bool RedisConnected { get; set; }

    public bool RabbitMqConnected { get; set; }

    public bool WebSocketConnected { get; set; }

    public bool WebhookConnected { get; set; }

    public string Status =>
        RedisConnected ||
        RabbitMqConnected ||
        WebSocketConnected ||
        WebhookConnected
            ? "Healthy"
            : "Unhealthy";
}