// Copyright (c) Will Burklund. Licensed under the MIT License.  See LICENSE in the project root for license information.

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
