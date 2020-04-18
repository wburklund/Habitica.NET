// Copyright (c) Will Burklund. Licensed under the MIT License.  See LICENSE in the project root for license information.

using Habitica.NET.Data.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Habitica.NET
{
    /// <summary>
    /// Class containing extension methods internal to Habitica.NET.
    /// </summary>
    internal static class Extensions
    {
        /// <summary>
        /// Prevents deadlocks by configuring a task so that continuation does not have to be run in the caller context.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns>The configured task.</returns>
        internal static ConfiguredTaskAwaitable Safe(this Task task) => task.ConfigureAwait(false);

        /// <summary>
        /// Prevents deadlocks by configuring a task so that continuation does not have to be run in the caller context.
        /// </summary>
        /// <typeparam name="T">The type of the data held by the task.</typeparam>
        /// <param name="task">The task.</param>
        /// <returns>The configured task.</returns>
        internal static ConfiguredTaskAwaitable<T> Safe<T>(this Task<T> task) => task.ConfigureAwait(false);

        /// <summary>
        /// Retrieves the body of an HttpResponseMessage as a string.
        /// </summary>
        /// <param name="response">The HttpResponseMessage.</param>
        /// <returns>The body of the message, read as a string.</returns>
        internal static string ToBody(this HttpResponseMessage response) => response.Content.ReadAsStringAsync().Safe().GetAwaiter().GetResult();

        /// <summary>
        /// Converts a string to a <c>System.Uri</c> object (either absolute or relative).
        /// </summary>
        /// <param name="stringToConvert">The string to convert.</param>
        /// <returns>The resultant <c>System.Uri</c> object.</returns>
        internal static Uri ToUri(this string stringToConvert) => new Uri(stringToConvert, UriKind.RelativeOrAbsolute);

        /// <summary>
        /// Takes a NameValueCollection and converts it to a URI query string. Returns an empty string if the collection is empty.
        /// </summary>
        /// <param name="nameValueCollection">The NameValueCollection to convert.</param>
        /// <returns>A query string built from the NameValueCollection.</returns>
        internal static string ToQueryString(this NameValueCollection nameValueCollection)
        {
            if (nameValueCollection.Count == 0) return "";

            var keys = nameValueCollection.AllKeys;
            var pairs = keys.Select(k => new KeyValuePair<string, string>(k, nameValueCollection[k]));
            var parameters = pairs.Select(p => Uri.EscapeUriString(p.Key) + "=" + Uri.EscapeUriString(p.Value));

            return "?" + string.Join("&", parameters);
        }

        /// <summary>
        /// Gets unread notifications from a Habitica response.
        /// </summary>
        /// <typeparam name="T">The type of the Habitica response.</typeparam>
        /// <param name="response">The Habitica response.</param>
        /// <returns>The unread notifications.</returns>
        internal static IEnumerable<Data.Model.Notification> UnreadNotifications<T>(this HabiticaResponse<T> response) => response.Notifications.Where(n => !n.Seen);

        /// <summary>
        /// Deserializes and extracts the data object from a Habitica API response.
        /// </summary>
        /// <typeparam name="T">The data type to deserialize.</typeparam>
        /// <param name="message">The response from which the object will be deserialized.</param>
        /// <returns>The data object.</returns>
        [SuppressMessage("Minor Code Smells", "S4018: Refactor this method to have parameters matching all the type parameters.", Justification = "Type parameter only used for output.")]
        internal static T UnwrapHabiticaResponse<T>(this HttpResponseMessage message)
        {
            string body = message.Content.ReadAsStringAsync().Safe().GetAwaiter().GetResult();
            return body.UnwrapHabiticaResponse<T>();
        }

        /// <summary>
        /// Deserializes and extracts the data object from a Habitica API response body.
        /// </summary>
        /// <typeparam name="T">The data type to deserialize.</typeparam>
        /// <param name="body">The response body from which the object will be deserialized.</param>
        /// <returns>The data object.</returns>
        [SuppressMessage("Minor Code Smells", "S4018: Refactor this method to have parameters matching all the type parameters.", Justification = "Type parameter only used for output.")]
        internal static T UnwrapHabiticaResponse<T>(this string body)
        {
            var response = JsonConvert.DeserializeObject<HabiticaResponse<T>>(body);
            return response.Data;
        }
    }
}
