// Copyright (c) Will Burklund. Licensed under the MIT License.  See LICENSE in the project root for license information.

using Moq;
using Moq.Protected;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Habitica.NET.UnitTests
{
    public class TestHabiticaClient
    {
        private readonly HttpResponseMessage response = GetTestResponse();
        private HttpRequestMessage capturedRequest;

        [Fact]
        public async Task HabiticaClient_GetAsync_SendsRequest()
        {
            (HabiticaClient client, Mock<HttpMessageHandler> mock) = GetTestTools();
            await client.GetAsync(new Uri("/foo/bar", UriKind.Relative));
            Assert.NotNull(capturedRequest);
        }

        [Fact]
        public async Task HabiticaClient_PostAsync_SendsRequest()
        {
            (HabiticaClient client, Mock<HttpMessageHandler> mock) = GetTestTools();
            await client.PostAsync<object>(new Uri("/foo/bar", UriKind.Relative), default);
            Assert.NotNull(capturedRequest);

        }

        [Fact]
        public async Task HabiticaClient_PutAsync_SendsRequest()
        {
            (HabiticaClient client, Mock<HttpMessageHandler> mock) = GetTestTools();
            await client.PutAsync<object>(new Uri("/foo/bar", UriKind.Relative), default);
            Assert.NotNull(capturedRequest);
        }

        [Fact]
        public async Task HabiticaClient_DeleteAsync_SendsRequest()
        {
            (HabiticaClient client, Mock<HttpMessageHandler> mock) = GetTestTools();
            await client.DeleteAsync(new Uri("/foo/bar", UriKind.Relative));
            Assert.NotNull(capturedRequest);
        }

        #region Infrastructure

        private static HttpResponseMessage GetTestResponse()
        {
            var testResponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
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
