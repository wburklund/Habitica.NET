using System;
using System.Collections.Generic;
using System.Net.Http;
using Xunit;

namespace Habitica.NET.Tests.Unit
{
    public class CoreClientTests
    {
        private readonly static HabiticaCredentials ValidHabiticaCredentials = new HabiticaCredentials(Guid.NewGuid(), "Habitica.NET.Tests", Guid.NewGuid(), Guid.NewGuid());

        [Theory]
        [ClassData(typeof(NullCreateData))]
        public void Create_NullArgument_ThrowsArgumentNullException(HabiticaCredentials credentials, HttpMessageHandler handler, Uri baseAddress)
        {
            Assert.Throws<ArgumentNullException>(() => CoreClient.Create(credentials, handler, baseAddress));
        }

        public class NullCreateData : TheoryData<HabiticaCredentials, HttpMessageHandler, Uri>
        {
            public NullCreateData()
            {
                Add(null, new SocketsHttpHandler(), new Uri("https://www.google.com"));
                Add(ValidHabiticaCredentials, null, new Uri("https://www.google.com"));
                Add(ValidHabiticaCredentials, new SocketsHttpHandler(), null);
            }
        }

        [Theory]
        [ClassData(typeof(InvalidCredentialData))]
        public void Create_InvalidCredentials_ThrowsArgumentException(HabiticaCredentials credentials)
        {
            Assert.Throws<ArgumentException>(() => CoreClient.Create(credentials, new SocketsHttpHandler()));
        }

        public class InvalidCredentialData : TheoryData<HabiticaCredentials>
        {
            public InvalidCredentialData()
            {
                Add(new HabiticaCredentials(default, "Habitica.NET.Tests", Guid.NewGuid(), Guid.NewGuid()));
                Add(new HabiticaCredentials(Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid()));
                Add(new HabiticaCredentials(Guid.NewGuid(), "Habitica.NET.Tests", default, Guid.NewGuid()));
                Add(new HabiticaCredentials(Guid.NewGuid(), "Habitica.NET.Tests", Guid.NewGuid(), default));
            }
        }
    }
}
