﻿// Copyright (c) Will Burklund. Licensed under the MIT License.  See LICENSE in the project root for license information.

using Habitica.NET.Data.Enum;
using Habitica.NET.Data.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Habitica.NET.Data.Response
{
    /// <summary>
    /// A task that can be completed by players.
    /// </summary>
    public class GetTask
    {
        /// <summary>
        /// Gets or sets the character attribute this task is associated with.
        /// </summary>
        public CharacterAttribute Attribute { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this is a built-in task.
        /// </summary>
        public bool ByHabitica { get; set; }

        /// <summary>
        /// Challenge task data. TODO: Implement
        /// </summary>
        public object Challenge { get; set; }
        
        /// <summary>
        /// User ID of the challenge leader.
        /// </summary>
        [JsonProperty("_id")]
        public Guid ChallengeLeaderId { get; set; }

        /// <summary>
        /// When this task was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Group task data. TODO: Implement.
        /// </summary>
        public object Group { get; set; }

        /// <summary>
        /// Task ID.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Notes associated with this task.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Difficulty of a task, represented by different values for trivial, easy, medium, and hard.
        /// TODO: Investigate deserialization to enum.
        /// </summary>
        public double Priority { get; set; }

        /// <summary>
        /// Reminders setup for this task.
        /// </summary>
        public IEnumerable<Reminder> Reminders { get; set; }

        /// <summary>
        /// IDs of tags associated with this task.
        /// </summary>
        public IEnumerable<Guid> Tags { get; set; }

        /// <summary>
        /// The text to be displayed for the task.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The type of the task.
        /// </summary>
        public TaskType Type { get; set; }

        /// <summary>
        /// When the task was last updated.
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// The user ID of the task owner.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// The cost in gold of a reward task.
        /// </summary>
        public double Value { get; set; }
    }
}
