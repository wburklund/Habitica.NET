// Copyright (c) Will Burklund. Licensed under the MIT License.  See LICENSE in the project root for license information.

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
