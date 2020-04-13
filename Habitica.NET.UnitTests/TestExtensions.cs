using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Habitica.NET.UnitTests
{
    public class TestExtensions
    {
        [Fact]
        public void Safe_Invoked_ConfiguresAwait()
        {
            var task = Task.CompletedTask;
            var safe = task.Safe();
            Assert.Equal(task.ConfigureAwait(false), safe);
        }

        [Fact]
        public void SafeTyped_Invoked_ConfiguresAwait()
        {
            var task = Task.FromResult(1);
            var safe = task.Safe();
            Assert.Equal(task.ConfigureAwait(false), safe);
        }

        [Fact]
        public void ToBody_Invoked_ReturnsMessageBody()
        {
            var message = new HttpResponseMessage();
            message.Content = new StringContent("Foo");
            Assert.Equal("Foo", message.ToBody());
        }

        [Fact]
        public void ToUri_AbsolutePath_AbsoluteUri()
        {
            var absString = "https://www.google.com";
            var absUri = absString.ToUri();
            Assert.True(absUri.IsAbsoluteUri);
        }

        [Fact]
        public void ToUri_RelativePath_RelativeUri()
        {
            var relString = "/foo/bar";
            var relUri = relString.ToUri();
            Assert.False(relUri.IsAbsoluteUri);
        }

        [Fact]
        public void ToQueryString_EmptyCollection_EmptyString()
        {
            Assert.Equal("", new NameValueCollection().ToQueryString());
        }

        [Fact]
        public void ToQueryString_PopulatedCollection_ReturnsQueryString()
        {
            var collection = new NameValueCollection();
            collection.Add("test1", "foo");
            collection.Add("test2", "bar");
            Assert.Equal("?test1=foo&test2=bar", collection.ToQueryString());
        }

        [Fact]
        public void UnwrapHabiticaResponse_ValidResponseBody_ReturnsData()
        {
            string body = @"{""data"":""1""}";
            Assert.Equal(1, body.UnwrapHabiticaResponse<int>());
        }
    }
}
