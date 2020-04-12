// Copyright (c) Will Burklund. Licensed under the MIT License.  See LICENSE in the project root for license information.

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
