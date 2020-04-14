// Copyright (c) Will Burklund. Licensed under the MIT License.  See LICENSE in the project root for license information.

namespace Habitica.NET.Data.Enum
{
    /// <summary>
    /// List types describing what kinds of tasks should be returned in a query.
    /// 
    /// CompletedTodos returns only the 30 last completed todos.
    /// </summary>
    public enum TaskListType
    {
        Habits,
        Dailys,
        Todos,
        Rewards,
        CompletedTodos
    }
}
