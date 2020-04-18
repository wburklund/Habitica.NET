using System;
using System.Collections.Generic;
using System.Text;

namespace Habitica.NET.Tests
{
    public class CoreClientTests
    {
        public static (CoreClient, MockMessageHandler, HabiticaCredentials) InstrumentedCoreClient()
        {
            var handler = new MockMessageHandler();
            var credentials = new HabiticaCredentials(Guid.NewGuid(), "Habitica.NET.Tests", Guid.NewGuid(), Guid.NewGuid());
            var client = CoreClient.Create(credentials, handler);
            return (client, handler, credentials);
        }
    }
}
