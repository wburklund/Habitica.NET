// Copyright (c) Will Burklund. Licensed under the MIT License.  See LICENSE in the project root for license information.

using Habitica.NET.Data.Response;
using Habitica.NET.Data.Request;
using Habitica.NET.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
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
    [SuppressMessage("Minor Code Smell", "S4018:Generic methods should provide type parameters", Justification = "Impractical with current design")]
    public sealed class HabiticaClient : IHabiticaClient, IDisposable
    {
        private readonly HttpClient httpClient;

        public HabiticaClient(HttpClient client, HabiticaCredentials credentials) : this(client, credentials, new Uri(Resources.HostUrl)) { }

        public HabiticaClient(
            HttpClient client,
            HabiticaCredentials credentials,
            Uri hostUrl)
        {
            if (credentials == null) throw new ArgumentNullException(nameof(credentials));
            if (client == null) throw new ArgumentNullException(nameof(client));
            ValidateCredentials(credentials);

            ConfigureHttpClient(client, credentials, hostUrl);
            this.httpClient = client;
        }

        private static void ValidateCredentials(HabiticaCredentials credentials)
        {
            if (credentials.AppAuthorUserId == default) throw new ArgumentException(Resources.ExceptionAppAuthorUserIdEmpty);
            if (string.IsNullOrWhiteSpace(credentials.AppName)) throw new ArgumentException(Resources.ExceptionAppNameEmpty);
            if (credentials.ApiUserId == default) throw new ArgumentException(Resources.ExceptionApiUserIdEmpty);
            if (credentials.ApiToken == default) throw new ArgumentException(Resources.ExceptionApiTokenEmpty);
        }

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

        public Task<TReturn> GetAsync<TReturn>(string requestUri) => GetAsync<TReturn>(requestUri.ToUri());

        public async Task<TReturn> GetAsync<TReturn>(Uri requestUri)
        {
            var httpResponse = await httpClient.GetAsync(requestUri).Safe();
            if (!httpResponse.IsSuccessStatusCode) throw new HttpResponseException(httpResponse);

            string content = await httpResponse.Content.ReadAsStringAsync().Safe();
            var response = JsonConvert.DeserializeObject<HabiticaResponse<TReturn>>(content);
            return response.Data;
        }

        public Task<TReturn> PostAsync<TRequest, TReturn>(string requestUri, TRequest requestData)
            => PostAsync<TRequest, TReturn>(requestUri.ToUri(), requestData);

        public async Task<TReturn> PostAsync<TRequest, TReturn>(Uri requestUri, TRequest requestData)
        {
            string json = JsonConvert.SerializeObject(requestData);
            var requestBody = new StringContent(json, Encoding.UTF8, "application/json");

            var httpResponse = await httpClient.PostAsync(requestUri, requestBody).Safe();
            if (!httpResponse.IsSuccessStatusCode) throw new HttpResponseException(httpResponse);

            string body = httpResponse.ToBody();
            var response = JsonConvert.DeserializeObject<HabiticaResponse<TReturn>>(body);
            return response.Data;
        }
        public Task<TReturn> PutAsync<TRequest, TReturn>(string requestUri, TRequest requestData) =>
            PutAsync<TRequest, TReturn>(requestUri.ToUri(), requestData);

        public async Task<TReturn> PutAsync<TRequest, TReturn>(Uri requestUri, TRequest requestData)
        {
            string json = JsonConvert.SerializeObject(requestData);
            var requestBody = new StringContent(json, Encoding.UTF8, "application/json");

            var httpResponse = await httpClient.PutAsync(requestUri, requestBody).Safe();
            if (!httpResponse.IsSuccessStatusCode) throw new HttpResponseException(httpResponse);

            string body = httpResponse.ToBody();
            var response = JsonConvert.DeserializeObject<HabiticaResponse<TReturn>>(body);
            return response.Data;
        }

        public Task DeleteAsync(string requestUri) => DeleteAsync(requestUri.ToUri());

        public async Task DeleteAsync(Uri requestUri)
        {
            var httpResponse = await httpClient.DeleteAsync(requestUri).Safe();
            if (!httpResponse.IsSuccessStatusCode) throw new HttpResponseException(httpResponse);
        }
        
        public Task<IEnumerable<Data.Model.Task>> GetUserTasksAsync(GetUserTasksRequest request)
        {
            const string endpoint = "/api/v3/tasks/user";

            if (request == null) throw new ArgumentNullException(nameof(request));

            NameValueCollection queryParameters = new NameValueCollection();
            if (request.TaskType.HasValue) queryParameters.Add("type", request.TaskType.Value.ToString("G"));
            if (request.DueDate.HasValue) queryParameters.Add("dueDate", request.DueDate.Value.ToString("o", DateTimeFormatInfo.InvariantInfo));

            string path = endpoint + queryParameters.ToQueryString();
            return GetAsync<IEnumerable<Data.Model.Task>>(path.ToUri());
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
