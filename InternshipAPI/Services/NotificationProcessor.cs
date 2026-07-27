using InternshipAPI.Models;

namespace InternshipAPI.Services;

public class NotificationProcessor
{
    private readonly NotificationStoreService _store;
    private readonly MessageBufferService _buffer;

    public NotificationProcessor(
        NotificationStoreService store,
        MessageBufferService buffer)
    {
        _store = store;
        _buffer = buffer;
    }

    public Task Process(NotificationEnvelope notification)
    {
        Console.WriteLine("================================");
        Console.WriteLine($"Source : {notification.Source}");
        Console.WriteLine($"Message: {notification.Message}");
        Console.WriteLine($"Received: {notification.ReceivedAt}");
        Console.WriteLine("================================");

        var item = new Notification
        {
            Source = notification.Source,
            Message = notification.Message
        };

        _store.Add(item);

        _buffer.Enqueue(new RawMessage
        {
            Adapter = notification.Source,
            Payload = notification.Message
        });

        return Task.CompletedTask;
    }
}