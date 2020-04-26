using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using Xunit;

namespace Habitica.NET.Tests.Unit
{
    public class ExtensionsTests
    {
        [Fact]
        public void ToQueryString_EmptyCollection_ReturnsEmptyString()
        {
            var collection = new NameValueCollection();
            Assert.Equal("", collection.ToQueryString());
        }
    }
}
