// Copyright (c) Will Burklund. Licensed under the MIT License.  See LICENSE in the project root for license information.

using Habitica.NET.Interfaces;
using Habitica.NET.Properties;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Habitica.NET
{
    public class CoreClient : IHttpClient
    {
        private readonly HttpClient httpClient;

        public static CoreClient Create(HabiticaCredentials credentials, HttpMessageHandler handler)
            => Create(credentials, handler, new Uri(Resources.BaseAddress));

        public static CoreClient Create(HabiticaCredentials credentials, HttpMessageHandler handler, Uri baseAddress)
        {
            ValidateCredentials(credentials);
            if (handler == null) throw new ArgumentNullException(nameof(handler));
            if (baseAddress == null) throw new ArgumentNullException(nameof(baseAddress));
            var client = ConfigureClient(credentials, handler, baseAddress);
            return new CoreClient(client);
        }

        private static void ValidateCredentials(HabiticaCredentials credentials)
        {
            if (credentials == null) throw new ArgumentNullException(nameof(credentials));
            if (credentials.ApiToken == default) throw new ArgumentException(Resources.ExceptionApiTokenEmpty);
            if (credentials.ApiUserId == default) throw new ArgumentException(Resources.ExceptionApiUserIdEmpty);
            if (credentials.AppAuthorUserId == default) throw new ArgumentException(Resources.ExceptionAppAuthorUserIdEmpty);
            if (string.IsNullOrWhiteSpace(credentials.AppName)) throw new ArgumentException(Resources.ExceptionAppNameEmpty);
        }

        private static HttpClient ConfigureClient(HabiticaCredentials credentials, HttpMessageHandler handler, Uri baseAddress)
        {
            var client = new HttpClient(handler);
            client.BaseAddress = baseAddress;

            string clientHeader = $"{credentials.AppAuthorUserId}-{credentials.AppName}";

            client.BaseAddress = baseAddress;
            client.DefaultRequestHeaders.Add("x-client", clientHeader);
            client.DefaultRequestHeaders.Add("x-api-user", credentials.ApiUserId.ToString());
            client.DefaultRequestHeaders.Add("x-api-key", credentials.ApiToken.ToString());

            return client;
        }

        private CoreClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public Task<HttpResponseMessage> GetAsync(Uri requestUri) => httpClient.GetAsync(requestUri);

        public Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content) => httpClient.PostAsync(requestUri, content);

        public Task<HttpResponseMessage> PutAsync(Uri requestUri, HttpContent content) => httpClient.PutAsync(requestUri, content);

        public Task<HttpResponseMessage> DeleteAsync(Uri requestUri) => httpClient.DeleteAsync(requestUri);
    }
}
