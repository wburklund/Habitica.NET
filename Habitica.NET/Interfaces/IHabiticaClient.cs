// Copyright (c) Will Burklund. Licensed under the MIT License.  See LICENSE in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Habitica.NET
{
    [SuppressMessage("Minor Code Smell", "S4018:Generic methods should provide type parameters.", Justification = "Impractical with current design")]
    public interface IHabiticaClient
    {
        /// <summary>
        /// Sends a GET request to the given URL.
        /// </summary>
        /// <param name="url">The URL to which the request will be sent.</param>
        /// <returns>The response body.</returns>
        Task<string> GetAsync(Uri url);

        /// <summary>
        /// Sends a POST request to the given URL with the given data.
        /// </summary>
        /// <typeparam name="T">The type of the object that will be sent as the body.</typeparam>
        /// <param name="url">The URL to which the request will be sent.</param>
        /// <param name="data">The object that will be serialized into the body.</param>
        /// <returns>The response body.</returns>
        Task<string> PostAsync<T>(Uri url, T data);

        /// <summary>
        /// Sends a PUT request to the given URL with the given data.
        /// </summary>
        /// <typeparam name="T">The type of the object that will be sent as the body.</typeparam>
        /// <param name="url">The URL to which the request will be sent.</param>
        /// <param name="data">The object that will be serialized into the body.</param>
        /// <returns>The response body.</returns>
        Task<string> PutAsync<T>(Uri url, T data);

        /// <summary>
        /// Sends a DELETE request to the given URL. 
        /// </summary>
        /// <param name="url">The URL to which the request will be sent.</param>
        /// <returns>A task.</returns>
        Task DeleteAsync(Uri url);
    }
}
