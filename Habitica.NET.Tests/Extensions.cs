using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Habitica.NET.Tests
{
    internal static class Extensions
    {
        internal static HttpResponseMessage WrapResponseBody(this string body) => body.WrapResponseBody(HttpStatusCode.OK);

        internal static HttpResponseMessage WrapResponseBody(this string body, HttpStatusCode statusCode)
        {
            var message = new HttpResponseMessage(statusCode);
            message.Content = new StringContent(body);
            return message;
        }
    }
}
