using System;

namespace Habitica.NET.Tests
{
    public class CoreClientTests
    {
        public static (CoreClient, StubMessageHandler, HabiticaCredentials) InstrumentedCoreClient()
        {
            var handler = new StubMessageHandler();
            var credentials = new HabiticaCredentials(Guid.NewGuid(), "Habitica.NET.Tests", Guid.NewGuid(), Guid.NewGuid());
            var client = CoreClient.Create(credentials, handler);
            return (client, handler, credentials);
        }
    }
}
