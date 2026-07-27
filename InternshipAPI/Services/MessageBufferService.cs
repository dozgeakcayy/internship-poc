using InternshipAPI.Models;
using System.Collections.Concurrent;

namespace InternshipAPI.Services;

public class MessageBufferService
{
    private readonly ConcurrentQueue<RawMessage> _queue = new();

    public void Enqueue(RawMessage message)
    {
        _queue.Enqueue(message);

        Console.WriteLine($"Message buffered. Queue Size: {_queue.Count}");
    }

    public bool TryDequeue(out RawMessage? message)
    {
        return _queue.TryDequeue(out message);
    }

    public int Count => _queue.Count;
}