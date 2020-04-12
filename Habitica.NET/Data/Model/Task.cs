// Copyright (c) Will Burklund. Licensed under the MIT License.  See LICENSE in the project root for license information.

using Habitica.NET.Data.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Habitica.NET.Data.Model
{
    public class Task
    {
        public CharacterAttribute Attribute { get; set; }
        public bool ByHabitica { get; set; }
        public object Challenge { get; set; }
        [JsonProperty("_id")]
        public Guid ChallengeLeaderId { get; set; }
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
