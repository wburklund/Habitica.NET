// Copyright (c) Will Burklund. Licensed under the MIT License.  See LICENSE in the project root for license information.

using System;

namespace Habitica.NET.Data.Model
{
    /// <summary>
    /// Notifications are included in every Habitica response.
    /// </summary>
    public class Notification
    {
        /// <summary>
        /// Gets or sets the ID of the notification.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the inner data of the notification. TODO: Add inner data models.
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the notification has been seen.
        /// </summary>
        public bool Seen { get; set; }

        /// <summary>
        /// Gets or sets the type of the notification.
        /// </summary>
        public string Type { get; set; }
    }
}
