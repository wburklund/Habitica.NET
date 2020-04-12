// Copyright (c) Will Burklund. Licensed under the MIT License.  See LICENSE in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Habitica.NET
{
    [SuppressMessage("Minor Code Smell", "S3997:Refactor this method so it invokes the overload accepting a 'System.Uri' parameter.", Justification = "Default interface implementations are not available in C# 7.3.")]
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
