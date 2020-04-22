using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Habitica.NET.Data.Request
{
    public class CreateUserTask : CreateTask
    {
        [JsonProperty("tags")]
        public IEnumerable<Guid> Tags { get; set; }
    }
}
