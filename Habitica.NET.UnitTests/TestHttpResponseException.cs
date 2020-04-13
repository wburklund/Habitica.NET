using Habitica.NET.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Habitica.NET.UnitTests
{
    public class TestHttpResponseException
    {
        private static string bodyString = @"{""data"":""{}"", ""success"":true}";

        [Fact]
        public async Task HttpResponseException_ThrowWithMessage_Succeeds()
        {
            await Assert.ThrowsAsync<HttpResponseException>(() =>
            {
                throw new HttpResponseException(GetTestResponse());
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
