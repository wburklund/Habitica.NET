using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Habitica.NET
{
    internal static class Extensions
    {
        internal static string ToQueryString(this NameValueCollection nameValueCollection)
        {
            if (nameValueCollection == null) throw new ArgumentNullException(nameof(nameValueCollection));
            if (nameValueCollection.Count == 0) return "";

            var keys = nameValueCollection.AllKeys;
            var pairs = keys.Select(k => new KeyValuePair<string, string>(k, nameValueCollection[k]));
            var parameters = pairs.Select(p => Uri.EscapeUriString(p.Key) + "=" + Uri.EscapeUriString(p.Value));

            return "?" + string.Join("&", parameters);
        }

        internal static string ToBody(this HttpResponseMessage response) => response.Content.ReadAsStringAsync().Await();

        internal static T Await<T>(this Task<T> task) => task.ConfigureAwait(false).GetAwaiter().GetResult();
    }
}
