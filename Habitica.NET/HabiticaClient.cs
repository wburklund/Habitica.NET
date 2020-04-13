// Copyright (c) Will Burklund. Licensed under the MIT License.  See LICENSE in the project root for license information.

using Habitica.NET.Data.Request;
using Habitica.NET.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
using Habitica.NET.Properties;
using System.Text;
using System.Resources;
using System.Globalization;

[assembly: System.Runtime.InteropServices.ComVisible(false)]
[assembly: CLSCompliant(true)]
[assembly: NeutralResourcesLanguage("en")]
namespace Habitica.NET
{
    /// <summary>
    /// Core Habitica client. Handles authentication and configuration.
    /// </summary>
    public sealed class HabiticaClient : IHabiticaClient, IDisposable
    {
        /// <summary>
        /// The inner HttpClient used for making requests.
        /// </summary>
        private readonly HttpClient httpClient;

        /// <summary>
        /// Constructor. Takes in HttpClient and HabiticaCredentials objects, setting the host URL to the default.
        /// </summary>
        /// <param name="client">The HttpClient.</param>
        /// <param name="credentials">The Habitica credentials.</param>
        public HabiticaClient(HttpClient client, HabiticaCredentials credentials) : this(client, credentials, new Uri(Resources.HostUrl)) { }

        /// <summary>
        /// Constructor. Takes in HttpClient and HabiticaCredentials objects and sets the host URL to the specified URL.
        /// </summary>
        /// <param name="client">The HttpClient.</param>
        /// <param name="credentials">The Habitica credentials.</param>
        /// <param name="hostUrl">The Habitica host URL.</param>
        public HabiticaClient(
            HttpClient client,
            HabiticaCredentials credentials,
            Uri hostUrl)
        {
            if (credentials == null) throw new ArgumentNullException(nameof(credentials));
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (hostUrl == null) throw new ArgumentNullException(nameof(hostUrl));
            ValidateCredentials(credentials);

            ConfigureHttpClient(client, credentials, hostUrl);
            this.httpClient = client;
        }

        /// <summary>
        /// Called during construction. Verifies that all credential fields have been populated.
        /// </summary>
        /// <param name="credentials">The credentials to validate.</param>
        private static void ValidateCredentials(HabiticaCredentials credentials)
        {
            if (credentials.AppAuthorUserId == default) throw new ArgumentException(Resources.ExceptionAppAuthorUserIdEmpty);
            if (string.IsNullOrWhiteSpace(credentials.AppName)) throw new ArgumentException(Resources.ExceptionAppNameEmpty);
            if (credentials.ApiUserId == default) throw new ArgumentException(Resources.ExceptionApiUserIdEmpty);
            if (credentials.ApiToken == default) throw new ArgumentException(Resources.ExceptionApiTokenEmpty);
        }

        /// <summary>
        /// Called during construction. Configures the HttpClient's base address and authentication headers.
        /// </summary>
        /// <param name="client">The HttpClient.</param>
        /// <param name="credentials">The Habitica credentials.</param>
        /// <param name="hostUrl">The Habitica host URL.</param>
        private static void ConfigureHttpClient(
            HttpClient client,
            HabiticaCredentials credentials,
            Uri hostUrl)
        {
            string clientHeader = $"{credentials.AppAuthorUserId}-{credentials.AppName}";
            client.BaseAddress = hostUrl;
            client.DefaultRequestHeaders.Add("x-client", clientHeader);
            client.DefaultRequestHeaders.Add("x-api-user", credentials.ApiUserId.ToString());
            client.DefaultRequestHeaders.Add("x-api-key", credentials.ApiToken.ToString());
        }

        ///<inheritdoc/>
        public async Task<string> GetAsync(Uri url)
        {
            var httpResponse = await httpClient.GetAsync(url).Safe();
            if (!httpResponse.IsSuccessStatusCode) throw new HttpResponseException(httpResponse);
            return httpResponse.ToBody();
        }

        ///<inheritdoc/>
        public async Task<string> PostAsync<T>(Uri url, T data)
        {
            string json = JsonConvert.SerializeObject(data);
            var requestBody = new StringContent(json, Encoding.UTF8, "application/json");

            var httpResponse = await httpClient.PostAsync(url, requestBody).Safe();
            if (!httpResponse.IsSuccessStatusCode) throw new HttpResponseException(httpResponse);
            return httpResponse.ToBody();
        }

        ///<inheritdoc/>
        public async Task<string> PutAsync<T>(Uri url, T data)
        {
            string json = JsonConvert.SerializeObject(data);
            var requestBody = new StringContent(json, Encoding.UTF8, "application/json");

            var httpResponse = await httpClient.PutAsync(url, requestBody).Safe();
            if (!httpResponse.IsSuccessStatusCode) throw new HttpResponseException(httpResponse);
            return httpResponse.ToBody();
        }

        ///<inheritdoc/>
        public async Task DeleteAsync(Uri url)
        {
            var httpResponse = await httpClient.DeleteAsync(url).Safe();
            if (!httpResponse.IsSuccessStatusCode) throw new HttpResponseException(httpResponse);
        }
        
        public async Task<IEnumerable<Data.Model.Task>> GetUserTasksAsync(GetUserTasksRequest request)
        {
            const string endpoint = "/api/v3/tasks/user";

            NameValueCollection queryParameters = new NameValueCollection();
            if (request.TaskType.HasValue) queryParameters.Add("type", request.TaskType.Value.ToString("G"));
            if (request.DueDate.HasValue) queryParameters.Add("dueDate", request.DueDate.Value.ToString("o", DateTimeFormatInfo.InvariantInfo));

            string path = endpoint + queryParameters.ToQueryString();
            Uri uri = path.ToUri();
            var res = await GetAsync(uri).Safe();

            return res.UnwrapHabiticaResponse<IEnumerable<Data.Model.Task>>();
        }

        #region IDisposable Support
        private bool disposedValue;

        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing) httpClient.Dispose();

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
