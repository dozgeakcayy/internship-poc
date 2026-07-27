using InternshipAPI.Models;

namespace InternshipAPI.Services;

public class NotificationStoreService
{
    private readonly List<Notification> _notifications = new();

    public void Add(Notification notification)
    {
        if (_notifications.Any(x =>
            x.Source == notification.Source &&
            x.Message == notification.Message))
        {
            Console.WriteLine("Duplicate notification ignored.");
            return;
        }

        _notifications.Add(notification);
    }

    public List<Notification> GetAll()
    {
        return _notifications;
    }
}