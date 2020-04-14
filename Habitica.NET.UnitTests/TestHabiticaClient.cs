// Copyright (c) Will Burklund. Licensed under the MIT License.  See LICENSE in the project root for license information.

using Habitica.NET.Exceptions;
using Moq;
using Moq.Protected;
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
        private HttpResponseMessage response = GetSuccessResponse();
        private HttpRequestMessage capturedRequest;

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
        public async Task GetAsync_Called_SendsRequest()
        {
            (HabiticaClient client, Mock<HttpMessageHandler> mock) = GetTestTools();
            await client.GetAsync(new Uri("/foo/bar", UriKind.Relative));
            Assert.NotNull(capturedRequest);
        }

        [Fact]
        public async Task GetAsync_BadResponse_Throws()
        {
            (HabiticaClient client, Mock<HttpMessageHandler> mock) = GetTestTools();
            this.response = GetFailResponse();
            await Assert.ThrowsAsync<HttpResponseException>(() => client.GetAsync(new Uri("/foo/bar", UriKind.Relative)));
        }


        [Fact]
        public async Task PostAsync_Called_SendsRequest()
        {
            (HabiticaClient client, Mock<HttpMessageHandler> mock) = GetTestTools();
            await client.PostAsync<object>(new Uri("/foo/bar", UriKind.Relative), default);
            Assert.NotNull(capturedRequest);
        }

        [Fact]
        public async Task PostAsync_BadResponse_Throws()
        {
            (HabiticaClient client, Mock<HttpMessageHandler> mock) = GetTestTools();
            this.response = GetFailResponse();
            await Assert.ThrowsAsync<HttpResponseException>(() => client.PostAsync(new Uri("/foo/bar", UriKind.Relative), 1));
        }

        [Fact]
        public async Task PutAsync_Called_SendsRequest()
        {
            (HabiticaClient client, Mock<HttpMessageHandler> mock) = GetTestTools();
            await client.PutAsync<object>(new Uri("/foo/bar", UriKind.Relative), default);
            Assert.NotNull(capturedRequest);
        }

        [Fact]
        public async Task PutAsync_BadResponse_Throws()
        {
            (HabiticaClient client, Mock<HttpMessageHandler> mock) = GetTestTools();
            this.response = GetFailResponse();
            await Assert.ThrowsAsync<HttpResponseException>(() => client.PutAsync(new Uri("/foo/bar", UriKind.Relative), 1));
        }

        [Fact]
        public async Task DeleteAsync_Called_SendsRequest()
        {
            (HabiticaClient client, Mock<HttpMessageHandler> mock) = GetTestTools();
            await client.DeleteAsync(new Uri("/foo/bar", UriKind.Relative));
            Assert.NotNull(capturedRequest);
        }

        [Fact]
        public async Task DeleteAsync_BadResponse_Throws()
        {
            (HabiticaClient client, Mock<HttpMessageHandler> mock) = GetTestTools();
            this.response = GetFailResponse();
            await Assert.ThrowsAsync<HttpResponseException>(() => client.DeleteAsync(new Uri("/foo/bar", UriKind.Relative)));
        }

        #region Infrastructure

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

        private (HabiticaClient, Mock<HttpMessageHandler>) GetTestTools()
        {
            var mockHandler = GetMockHandler();
            var httpClient = new HttpClient(mockHandler.Object);
            var credentials = GetTestCredentials();
            var client = new HabiticaClient(httpClient, credentials);
            return (client, mockHandler);
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

        private Mock<HttpMessageHandler> GetMockHandler()
        {
            var mock = new Mock<HttpMessageHandler>();

            mock.Protected().As<IMockableHandler>()
                .Setup(m => m.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
                .Returns((HttpRequestMessage request, CancellationToken token) => {
                    this.capturedRequest = request;
                    return Task.FromResult(this.response);
                });

            return mock;
        }

        internal interface IMockableHandler
        {
            Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token);
        }

        #endregion

    }
}
