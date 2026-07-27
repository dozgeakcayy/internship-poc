namespace InternshipAPI.Models;

public class Notification
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Source { get; set; } = "";

    public string Message { get; set; } = "";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}