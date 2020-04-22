using Habitica.NET.Data.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Habitica.NET.Data.Request
{
    public class CreateTask
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("alias")]
        public string Alias { get; set; }
        
        // TODO: Serialize from enum
        [JsonProperty("attribute")]
        public string Attribute { get; set; }

        [JsonProperty("collapseChecklist")]
        public bool CollapseChecklist { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        // TODO: Serialize from Datetime
        [JsonProperty("date")]
        public string Date { get; set; }

        // TODO: Serialize from enum
        [JsonProperty("priority")]
        public double Priority { get; set; }

        [JsonProperty("reminders")]
        public IEnumerable<Reminder> Reminders { get; set; }

        // TODO: Serialize from enum
        [JsonProperty("frequency")]
        public string Frequency { get; set; }

        // TODO: Serialize from object
        [JsonProperty("repeat")]
        public string Repeat { get; set; }

        [JsonProperty("everyX")]
        public int EveryX { get; set; }

        [JsonProperty("streak")]
        public int Streak { get; set; }

        [JsonProperty("daysOfMonth")]
        public IEnumerable<int> DaysOfMonth { get; set; }

        [JsonProperty("weeksOfMonth")]
        public IEnumerable<int> WeeksOfMonth { get; set; }

        [JsonProperty("up")]
        public bool Up { get; set; }

        [JsonProperty("down")]
        public bool Down { get; set; }

        [JsonProperty("value")]
        public double Value { get; set; }


    }
}
