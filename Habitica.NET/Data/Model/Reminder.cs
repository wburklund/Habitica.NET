using System;

namespace Habitica.NET.Data.Model
{
    public class Reminder
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime Time { get; set; }
    }
}