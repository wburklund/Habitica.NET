﻿// Copyright (c) Will Burklund. Licensed under the MIT License.  See LICENSE in the project root for license information.

using Habitica.NET.Data.Response;
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

namespace Habitica.NET
{
    public sealed class HabiticaClient : IHabiticaClient, IDisposable
    {
        private readonly HttpClient httpClient;

        public HabiticaClient(
            HttpClient client,
            HabiticaCredentials credentials,
            string hostUrl = "https://habitica.com/")
        {
            if (credentials == null) throw new ArgumentNullException(nameof(credentials));
            if (client == null) throw new ArgumentNullException(nameof(client));
            ValidateCredentials(credentials);

            ConfigureHttpClient(client, credentials, hostUrl);
            this.httpClient = client;
        }

        private void ValidateCredentials(HabiticaCredentials credentials)
        {
            if (credentials.AppAuthorUserId == default) throw new ArgumentException(Resources.ExceptionAppAuthorUserIdEmpty);
            if (string.IsNullOrWhiteSpace(credentials.AppName)) throw new ArgumentException(Resources.ExceptionAppNameEmpty);
            if (credentials.ApiUserId == default) throw new ArgumentException(Resources.ExceptionApiUserIdEmpty);
            if (credentials.ApiToken == default) throw new ArgumentException(Resources.ExceptionApiTokenEmpty);
        }

        private void ConfigureHttpClient(
            HttpClient client,
            HabiticaCredentials credentials,
            string hostUrl)
        {
            string clientHeader = $"{credentials.AppAuthorUserId}-{credentials.AppName}";

            client.BaseAddress = new Uri(hostUrl);
            client.DefaultRequestHeaders.Add("x-client", clientHeader);
            client.DefaultRequestHeaders.Add("x-api-user", credentials.ApiUserId.ToString());
            client.DefaultRequestHeaders.Add("x-api-key", credentials.ApiToken.ToString());
        }

        public async Task<T> GetAsync<T>(string requestUri)
        {
            var httpResponse = await httpClient.GetAsync(requestUri);
            if (!httpResponse.IsSuccessStatusCode) throw new HttpResponseException(httpResponse);

            string content = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<HabiticaResponse<T>>(content);
            return response.Data;
        }

        public async Task<TResponseData> PostAsync<TRequestData, TResponseData>(string requestUri, TRequestData requestData)
        {
            string json = JsonConvert.SerializeObject(requestData);
            var requestBody = new StringContent(json, Encoding.UTF8, "application/json");

            var httpResponse = await httpClient.PostAsync(requestUri, requestBody);
            if (!httpResponse.IsSuccessStatusCode) throw new HttpResponseException(httpResponse);

            string body = httpResponse.ToBody();
            var response = JsonConvert.DeserializeObject<HabiticaResponse<TResponseData>>(body);
            return response.Data;
        }

        public async Task<TResponseData> PutAsync<TRequestData, TResponseData>(string requestUri, TRequestData requestData)
        {
            string json = JsonConvert.SerializeObject(requestData);
            var requestBody = new StringContent(json, Encoding.UTF8, "application/json");

            var httpResponse = await httpClient.PutAsync(requestUri, requestBody);
            if (!httpResponse.IsSuccessStatusCode) throw new HttpResponseException(httpResponse);

            string body = httpResponse.ToBody();
            var response = JsonConvert.DeserializeObject<HabiticaResponse<TResponseData>>(body);
            return response.Data;
        }

        public async Task DeleteAsync(string requestUri)
        {
            var httpResponse = await httpClient.DeleteAsync(requestUri);
            if (!httpResponse.IsSuccessStatusCode) throw new HttpResponseException(httpResponse);
        }
        
        public Task<IEnumerable<Data.Model.Task>> GetUserTasks(GetUserTasksRequest request)
        {
            const string endpoint = "/api/v3/tasks/user";

            if (request == null) throw new ArgumentNullException(nameof(request));

            NameValueCollection queryParameters = new NameValueCollection();
            if (request.TaskType.HasValue) queryParameters.Add("type", request.TaskType.Value.ToString("G"));
            if (request.DueDate.HasValue) queryParameters.Add("dueDate", request.DueDate.Value.ToString());

            string path = endpoint + queryParameters.ToQueryString();

            return GetAsync<IEnumerable<Data.Model.Task>>(path);
        }

        #region IDisposable Support
        private bool disposedValue = false;

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
