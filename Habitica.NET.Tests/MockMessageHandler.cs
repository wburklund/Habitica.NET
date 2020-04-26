using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Habitica.NET.Tests
{
    public class MockMessageHandler : HttpMessageHandler
    {
        public HttpRequestMessage LastRequest { get; private set; }
        public string LastRequestStringContent { get; private set; }
        public HttpResponseMessage Response { get; set; }

        public MockMessageHandler() { }

        public MockMessageHandler(HttpResponseMessage response)
        {
            Response = response;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            LastRequest = request;
            LastRequestStringContent = request.Content.ReadAsStringAsync().Result;
            return Task.FromResult(Response);
        }
    }
}
