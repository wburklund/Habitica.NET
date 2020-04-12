// Copyright (c) Will Burklund. Licensed under the MIT License.  See LICENSE in the project root for license information.

using System;
using System.Net.Http;

namespace Habitica.NET.Exceptions
{
    [Serializable]
    public class HttpResponseException : Exception
    {
        public HttpResponseMessage Response { get; set; }
        public HttpResponseException() { }
        public HttpResponseException(string message) : base(message) { }
        public HttpResponseException(string message, Exception inner) : base(message, inner) { }
        public HttpResponseException(HttpResponseMessage response) : base(response.ToBody())
        {
            this.Response = response;
        }
        public HttpResponseException(HttpResponseMessage response, Exception inner) : base(response.ToBody(), inner)
        {
            this.Response = response;
        }
        protected HttpResponseException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
