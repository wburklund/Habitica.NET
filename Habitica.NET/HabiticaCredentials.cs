// Copyright (c) Will Burklund. Licensed under the MIT License.  See LICENSE in the project root for license information.

using System;

namespace Habitica.NET
{
    public class HabiticaCredentials
    {
        public Guid AppAuthorUserId { get; set; }
        public string AppName { get; set; }
        public Guid ApiUserId { get; set; }
        public Guid ApiToken { get; set; }

        public HabiticaCredentials() { }

        public HabiticaCredentials(Guid appAuthorUserId, string appName, Guid apiUserId, Guid apiToken)
        {
            this.AppAuthorUserId = appAuthorUserId;
            this.AppName = appName;
            this.ApiUserId = apiUserId;
            this.ApiToken = apiToken;
        }
    }
}
