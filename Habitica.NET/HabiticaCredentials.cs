using System;

namespace Habitica.NET
{
    public class HabiticaCredentials
    {
        public Guid AppAuthorUserId { get; set; }
        public string AppName { get; set; }
        public Guid ApiUserId { get; set; }
        public Guid ApiKey { get; set; }

        public HabiticaCredentials() { }

        public HabiticaCredentials(Guid appAuthorUserId, string appName, Guid apiUserId, Guid apiKey)
        {
            this.AppAuthorUserId = appAuthorUserId;
            this.AppName = appName;
            this.ApiUserId = apiUserId;
            this.ApiKey = apiKey;
        }
    }
}