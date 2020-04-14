// Copyright (c) Will Burklund. Licensed under the MIT License.  See LICENSE in the project root for license information.

using Habitica.NET.Exceptions;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Habitica.NET.UnitTests
{
    public class TestHttpResponseException
    {
        private static string bodyString = @"{""data"":""{}"", ""success"":true}";

        [Fact]
        public void ResponseProperty_Get_ReturnsProvidedResponse()
        {
            var response = GetTestResponse();
            var ex = new HttpResponseException(response);
            Assert.Equal(response, ex.Response);
        }

        [Fact]
        public async Task DefaultConstructor_Called_Succeeds()
        {
            await Assert.ThrowsAsync<HttpResponseException>(() =>
            {
                throw new HttpResponseException();
            });
        }

        [Fact]
        public async Task StringConstructor_Called_Succeeds()
        {
            await Assert.ThrowsAsync<HttpResponseException>(() =>
            {
                throw new HttpResponseException();
            });
        }

        [Fact]
        public async Task StringExceptionConstructor_Called_Succeeds()
        {
            await Assert.ThrowsAsync<HttpResponseException>(() =>
            {
                throw new HttpResponseException("Test string");
            });
        }


        [Fact]
        public async Task MessageConstructor_Called_Succeeds()
        {
            await Assert.ThrowsAsync<HttpResponseException>(() =>
            {
                throw new HttpResponseException("Test string", new Exception());
            });
        }

        [Fact]
        public async Task MessageExceptionConstructor_Called_Succeeds()
        {
            await Assert.ThrowsAsync<HttpResponseException>(() =>
            {
                throw new HttpResponseException(GetTestResponse(), new Exception());
            });
        }

        private static HttpResponseMessage GetTestResponse()
        {
            var testResponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            testResponse.Content = new StringContent(bodyString);
            return testResponse;
        }
    }
}
