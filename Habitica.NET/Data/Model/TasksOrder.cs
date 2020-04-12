using System;
using System.Collections.Generic;

namespace Habitica.NET.Data.Model
{
    public class TasksOrder
    {
        public IEnumerable<Guid> Dailys { get; set; }
        public IEnumerable<Guid> Habits { get; set; }
        public IEnumerable<Guid> Rewards { get; set; }
        public IEnumerable<Guid> Todos { get; set; }
    }
}
