// Copyright (c) Will Burklund. Licensed under the MIT License.  See LICENSE in the project root for license information.

using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Habitica.NET.Interfaces
{
    public interface ICoreClient
    {
        Task<HttpResponseMessage> GetAsync(Uri requestUri);
        Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content);
        Task<HttpResponseMessage> PutAsync(Uri requestUri, HttpContent content);
        Task<HttpResponseMessage> DeleteAsync(Uri requestUri);
    }
}
