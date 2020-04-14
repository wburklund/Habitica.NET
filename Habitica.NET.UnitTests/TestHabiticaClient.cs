// Copyright (c) Will Burklund. Licensed under the MIT License.  See LICENSE in the project root for license information.

using Habitica.NET.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Habitica.NET.UnitTests
{
    public class TestHabiticaClient
    {
        [Fact]
        public void Constructor_NullClient_Throws()
        {
            var credentials = GetTestCredentials();
            Assert.Throws<ArgumentNullException>(() => new HabiticaClient(null, credentials));
        }

        [Fact]
        public void Constructor_NullCredentials_Throws()
        {
            var client = new HttpClient();
            Assert.Throws<ArgumentNullException>(() => new HabiticaClient(client, null));
        }

        [Fact]
        public void Constructor_NullHostUrl_Throws()
        {
            var client = new HttpClient();
            var credentials = GetTestCredentials();
            Assert.Throws<ArgumentNullException>(() => new HabiticaClient(client, credentials, null));
        }

        [Theory]
        [MemberData(nameof(GetInvalidCredentials))]
        public void Constructor_InvalidCredentials_Throws(object credentials)
        {
            var client = new HttpClient();
            Assert.Throws<ArgumentException>(() => new HabiticaClient(client, (HabiticaCredentials)credentials));
        }

        [Fact]
        public void Dispose_Called_DisposesMessageHandler()
        {
            (HabiticaClient client, TestHttpMessageHandler handler) = GetTestTools();
            client.Dispose();
            Assert.Equal(1, handler.TimesDisposed);
        }

        [Fact]
        public void Dispose_CalledMultiple_DisposesMessageHandlerOnce()
        {
            (HabiticaClient client, TestHttpMessageHandler handler) = GetTestTools();
            client.Dispose();
            client.Dispose();
            Assert.Equal(1, handler.TimesDisposed);
        }


        [Fact]
        public async Task GetAsync_Called_SendsRequest()
        {
            (HabiticaClient client, TestHttpMessageHandler handler) = GetTestTools();
            await client.GetAsync(new Uri("/foo/bar", UriKind.Relative));
            Assert.NotNull(handler.CapturedRequest);
        }

        [Fact]
        public async Task GetAsync_BadResponse_Throws()
        {
            (HabiticaClient client, TestHttpMessageHandler handler) = GetTestTools();
            handler.ResponseToSend = GetFailResponse();
            await Assert.ThrowsAsync<HttpResponseException>(() => client.GetAsync(new Uri("/foo/bar", UriKind.Relative)));
        }


        [Fact]
        public async Task PostAsync_Called_SendsRequest()
        {
            (HabiticaClient client, TestHttpMessageHandler handler) = GetTestTools();
            await client.PostAsync<object>(new Uri("/foo/bar", UriKind.Relative), default);
            Assert.NotNull(handler.CapturedRequest);
        }

        [Fact]
        public async Task PostAsync_BadResponse_Throws()
        {
            (HabiticaClient client, TestHttpMessageHandler handler) = GetTestTools();
            handler.ResponseToSend = GetFailResponse();
            await Assert.ThrowsAsync<HttpResponseException>(() => client.PostAsync(new Uri("/foo/bar", UriKind.Relative), 1));
        }

        [Fact]
        public async Task PutAsync_Called_SendsRequest()
        {
            (HabiticaClient client, TestHttpMessageHandler handler) = GetTestTools();
            await client.PutAsync<object>(new Uri("/foo/bar", UriKind.Relative), default);
            Assert.NotNull(handler.CapturedRequest);
        }

        [Fact]
        public async Task PutAsync_BadResponse_Throws()
        {
            (HabiticaClient client, TestHttpMessageHandler handler) = GetTestTools();
            handler.ResponseToSend = GetFailResponse();
            await Assert.ThrowsAsync<HttpResponseException>(() => client.PutAsync(new Uri("/foo/bar", UriKind.Relative), 1));
        }

        [Fact]
        public async Task DeleteAsync_Called_SendsRequest()
        {
            (HabiticaClient client, TestHttpMessageHandler handler) = GetTestTools();
            await client.DeleteAsync(new Uri("/foo/bar", UriKind.Relative));
            Assert.NotNull(handler.CapturedRequest);
        }

        [Fact]
        public async Task DeleteAsync_BadResponse_Throws()
        {
            (HabiticaClient client, TestHttpMessageHandler handler) = GetTestTools();
            handler.ResponseToSend = GetFailResponse();
            await Assert.ThrowsAsync<HttpResponseException>(() => client.DeleteAsync(new Uri("/foo/bar", UriKind.Relative)));
        }

        #region Infrastructure

        private (HabiticaClient, TestHttpMessageHandler) GetTestTools()
        {
            var handler = new TestHttpMessageHandler();
            var httpClient = new HttpClient(handler);
            var credentials = GetTestCredentials();
            var client = new HabiticaClient(httpClient, credentials);
            return (client, handler);
        }

        private HabiticaCredentials GetTestCredentials()
            => new HabiticaCredentials(Guid.NewGuid(), "Test", Guid.NewGuid(), Guid.NewGuid());

        public static IEnumerable<object[]> GetInvalidCredentials()
        {
            yield return new object[] { new HabiticaCredentials(Guid.Empty, "Test", Guid.NewGuid(), Guid.NewGuid()) };
            yield return new object[] { new HabiticaCredentials(Guid.NewGuid(), null, Guid.NewGuid(), Guid.NewGuid()) };
            yield return new object[] { new HabiticaCredentials(Guid.NewGuid(), "Test", Guid.Empty, Guid.NewGuid()) };
            yield return new object[] { new HabiticaCredentials(Guid.NewGuid(), "Test", Guid.NewGuid(), Guid.Empty) };
        }

        private class TestHttpMessageHandler : HttpMessageHandler
        {
            public HttpRequestMessage CapturedRequest { get; set; }
            public HttpResponseMessage ResponseToSend { get; set; } = GetSuccessResponse();
            public int TimesDisposed { get; set; }
            public int NumberOfRequests { get; set; }
            protected override void Dispose(bool disposing)
            {
                if (TimesDisposed == 0)
                {
                    base.Dispose(disposing);
                }

                TimesDisposed += 1;
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                NumberOfRequests += 1;
                CapturedRequest = request;
                return Task.FromResult(ResponseToSend);
            }
        }

        private static HttpResponseMessage GetSuccessResponse()
        {
            var testResponse = new HttpResponseMessage(HttpStatusCode.OK);
            testResponse.Content = new StringContent(@"{""data"":""{}"", ""success"":true}");
            return testResponse;
        }

        private static HttpResponseMessage GetFailResponse()
        {
            var testResponse = new HttpResponseMessage(HttpStatusCode.BadRequest);
            testResponse.Content = new StringContent(@"{""data"":""{}"", ""success"":true}");
            return testResponse;
        }

        #endregion

    }
}
