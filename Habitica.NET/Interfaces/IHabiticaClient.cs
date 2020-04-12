// Copyright (c) Will Burklund. Licensed under the MIT License.  See LICENSE in the project root for license information.

using System;
using System.Threading.Tasks;

namespace Habitica.NET
{
    public interface IHabiticaClient
    {
        Task<TRes> GetAsync<TRes>(string requestUri);
        Task<TRes> GetAsync<TRes>(Uri requestUri);

        Task<TRes> PostAsync<TReq, TRes>(string requestUri, TReq requestData);
        Task<TRes> PostAsync<TReq, TRes>(Uri requestUri, TReq requestData);
        Task<TRes> PutAsync<TReq, TRes>(string requestUri, TReq requestData);
        Task<TRes> PutAsync<TReq, TRes>(Uri requestUri, TReq requestData);
        Task DeleteAsync(string requestUri);
        Task DeleteAsync(Uri requestUri);
    }
}
