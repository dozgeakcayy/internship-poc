namespace InternshipAPI.Models;

public class NotificationEnvelope
{
    public string Source { get; set; } = "";

    public string Message { get; set; } = "";

    public DateTime ReceivedAt { get; set; } = DateTime.UtcNow;
}