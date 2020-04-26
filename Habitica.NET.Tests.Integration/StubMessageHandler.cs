using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Habitica.NET.Tests.Integration
{
    public class StubMessageHandler : HttpMessageHandler
    {
        public HttpRequestMessage LastRequest { get; private set; }
        public string LastRequestStringContent { get; private set; }
        public HttpResponseMessage Response { get; set; } = new HttpResponseMessage();

        public StubMessageHandler() { }

        public StubMessageHandler(HttpResponseMessage response)
        {
            Response = response;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            LastRequest = request;
            LastRequestStringContent = request.Content?.ReadAsStringAsync().Result;
            return Task.FromResult(Response);
        }
    }
}
