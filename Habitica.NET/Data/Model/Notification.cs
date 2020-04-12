using System;

namespace Habitica.NET.Data.Model
{
    public class Notification
    {
        public Guid Id { get; set; }
        public NotificationData Data { get; set; }
        public bool Seen { get; set; }
        public string Type { get; set; }
    }
}
