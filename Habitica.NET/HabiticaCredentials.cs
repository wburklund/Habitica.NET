// Copyright (c) Will Burklund. Licensed under the MIT License.  See LICENSE in the project root for license information.

using System;

namespace Habitica.NET
{
    /// <summary>
    /// Habitica credentials object. Contains information on the current user as well as the application.
    /// </summary>
    public class HabiticaCredentials
    {
        /// <summary>
        /// The application author's Habitica user ID.
        /// </summary>
        public Guid AppAuthorUserId { get; set; }
        /// <summary>
        /// The application's name.
        /// </summary>
        public string AppName { get; set; }
        /// <summary>
        /// The ID of the user for which requests will be made.
        /// </summary>
        public Guid ApiUserId { get; set; }
        /// <summary>
        /// The API token of the user for which requests will be made.
        /// </summary>
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
