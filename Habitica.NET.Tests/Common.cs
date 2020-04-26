using System;
using System.Collections.Generic;
using System.Net.Http;
using Xunit;

namespace Habitica.NET.Tests
{
    public class Common
    {
        private readonly static HabiticaCredentials ValidHabiticaCredentials = new HabiticaCredentials(Guid.NewGuid(), "Habitica.NET.Tests", Guid.NewGuid(), Guid.NewGuid());

        public static (CoreClient, StubMessageHandler, HabiticaCredentials) InstrumentedCoreClient()
        {
            var handler = new StubMessageHandler();
            var credentials = ValidHabiticaCredentials;
            var client = CoreClient.Create(credentials, handler);
            return (client, handler, credentials);
        }

    }
}
