// Copyright (c) Will Burklund. Licensed under the MIT License.  See LICENSE in the project root for license information.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Habitica.NET.Data.Request
{
    public class CreateUserTask : CreateTask
    {
        [JsonProperty("tags")]
        public IEnumerable<Guid> Tags { get; set; }
    }
}
