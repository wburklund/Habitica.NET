using Habitica.NET.Data.Enum;
using System;

namespace Habitica.NET.Data.Request
{
    public class GetUserTasksRequest
    {
        public TaskListType? TaskType { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
