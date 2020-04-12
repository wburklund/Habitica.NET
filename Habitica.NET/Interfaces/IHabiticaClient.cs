// Copyright (c) Will Burklund. Licensed under the MIT License.  See LICENSE in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Habitica.NET
{
    [SuppressMessage("Minor Code Smell", "S4018:Generic methods should provide type parameters.", Justification = "Impractical with current design")]
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
