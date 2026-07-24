namespace InternshipAPI.Models;

public class RawMessage
{
    public string Adapter { get; set; } = "";
    public string Payload { get; set; } = "";

    public DateTime ReceivedAt { get; set; } = DateTime.Now;
}