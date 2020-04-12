using Habitica.NET.Data.Enum;
using System;
using System.Collections.Generic;

namespace Habitica.NET.Data.Model
{
    public class Task
    {
        public Guid _Id { get; set; }
        public CharacterAttribute Attribute { get; set; }
        public bool ByHabitica { get; set; }
        public object Challenge { get; set; }
        public DateTime CreatedAt { get; set; }
        public GroupTaskData Group { get; set; }
        public Guid Id { get; set; }
        public string Notes { get; set; }
        public double Priority { get; set; }
        public IEnumerable<Reminder> Reminders { get; set; }
        public IEnumerable<Guid> Tags { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid UserId { get; set; }
        public double Value { get; set; }
    }
}
